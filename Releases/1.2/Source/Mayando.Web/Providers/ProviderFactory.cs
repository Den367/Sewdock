using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using JelleDruyts.Web.Mvc;
using Mayando.ProviderModel;
using Mayando.Web.Models;

namespace Mayando.Web.Providers
{
    /// <summary>
    /// Discovers providers and creates instances from them.
    /// </summary>
    /// <typeparam name="TProviderType">The type of the provider.</typeparam>
    /// <typeparam name="TProviderInfoType">The type of the information class for the provider type.</typeparam>
    internal class ProviderFactory<TProviderType, TProviderInfoType>
        where TProviderType : IProvider
        where TProviderInfoType : ProviderInfo
    {
        #region Fields

        private TProviderInfoType[] availableProviders;
        private object availableProvidersLock = new object();
        private Func<Type, TProviderInfoType> providerInfoInstantiator;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ProviderFactory&lt;TProviderType, TProviderInfoType&gt;"/> class.
        /// </summary>
        /// <param name="providerInfoInstantiator">The provider info instantiator.</param>
        public ProviderFactory(Func<Type, TProviderInfoType> providerInfoInstantiator)
        {
            this.providerInfoInstantiator = providerInfoInstantiator;
        }

        #endregion

        #region GetAvailableProviders

        /// <summary>
        /// Gets the available providers.
        /// </summary>
        /// <returns>A list of available providers.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2001:AvoidCallingProblematicMethods", MessageId = "System.Reflection.Assembly.LoadFile")]
        public TProviderInfoType[] GetAvailableProviders()
        {
            if (availableProviders == null)
            {
                lock (availableProvidersLock)
                {
                    if (availableProviders == null)
                    {
                        var assemblies = new List<Assembly>();
                        assemblies.AddRange(AppDomain.CurrentDomain.GetAssemblies());
                        foreach (var file in Directory.GetFiles(AppDomain.CurrentDomain.RelativeSearchPath, "*.dll"))
                        {
                            try
                            {
                                assemblies.Add(Assembly.LoadFile(file));
                            }
                            catch (BadImageFormatException)
                            {
                                // The file is not an assembly.
                                // Continue with the next file.
                            }
                        }
                        var assemblyProviders = from t in assemblies.SelectMany(s => s.GetTypes())
                                                where !t.IsAbstract && typeof(TProviderType).IsAssignableFrom(t) && t.FindAttribute<ProviderAttribute>() != null
                                                select providerInfoInstantiator(t);
                        availableProviders = assemblyProviders.ToArray();
                    }
                }
            }
            return availableProviders;
        }

        #endregion

        #region GetProviderInfo

        /// <summary>
        /// Gets the provider info.
        /// </summary>
        /// <param name="providerGuid">The provider GUID.</param>
        /// <returns>Information about the provider.</returns>
        public TProviderInfoType GetProviderInfo(string providerGuid)
        {
            if (string.IsNullOrEmpty(providerGuid))
            {
                return null;
            }
            else
            {
                return (from p in GetAvailableProviders()
                        where p.Guid == providerGuid
                        select p).FirstOrDefault();
            }
        }

        #endregion

        #region CreateProvider

        /// <summary>
        /// Creates the provider.
        /// </summary>
        /// <param name="providerGuid">The provider GUID.</param>
        /// <param name="providerInitializer">The provider initializer.</param>
        /// <param name="settingsScope">The settings scope.</param>
        /// <returns>The instantiated and initialized provider.</returns>
        public TProviderType CreateProvider(string providerGuid, Action<TProviderType> providerInitializer, SettingsScope settingsScope)
        {
            var providerType = (from p in GetAvailableProviders()
                                where p.Guid == providerGuid
                                select p.Type).FirstOrDefault();
            TProviderType instance = default(TProviderType);
            SettingDefinition[] settings = null;
            if (providerType != null)
            {
                try
                {
                    instance = (TProviderType)Activator.CreateInstance(providerType);
                }
                catch (MissingMethodException exc)
                {
                    throw new InvalidOperationException("The selected provider does not provide a public parameterless constructor.", exc);
                }
                providerInitializer(instance);
                settings = instance.GetSettingDefinitions();
            }
            using (var repository = MayandoRepositoryFactory.CreateRepository(true))
            {
                if (settings == null)
                {
                    settings = new SettingDefinition[0];
                }
                repository.EnsureSettings(settingsScope, settings);
                repository.CommitChanges();
            }
            return instance;
        }

        #endregion
    }
}