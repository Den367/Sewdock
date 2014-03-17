using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.EntityClient;
using System.Data.Mapping;
using System.Data.Metadata.Edm;
using System.Data.Objects;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Xml;

namespace Mayando.Web.Infrastructure
{
    /// <summary>
    /// Creates <see cref="ObjectContext"/> instances that can have modified mappings, e.g.
    /// to automatically apply a table prefix to each table in the database.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The <see cref="ObjectContextFactory"/> caches the modifications that were performed for
    /// subsequent instances. In fact, because certain data is always cached, the overall performance
    /// will increase with and without performing any modifications, compared to directly instantiating
    /// the <see cref="ObjectContext"/> through its default constructor. So in general, using this
    /// factory will be faster than manually creating instances with a default constructor.
    /// </para>
    /// <para>
    /// Note: the <see cref="ObjectContextFactory"/> has a slightly simplified way of looking up the
    /// metadata mapping files if they are embedded resources and if the assembly is specified by wildcard
    /// (i.e. URI's that start with "res://*"). By default, the Entity Framework will look them up in the
    /// following order (see http://msdn.microsoft.com/en-us/library/cc716756.aspx):
    /// (1) The calling assembly, (2) The referenced assemblies and (3) The assemblies in the bin directory
    /// of an application. The <see cref="ObjectContextFactory"/> will simply look through all loaded
    /// assemblies in the order returned by "System.AppDomain.CurrentDomain.GetAssemblies()".
    /// </para>
    /// </remarks>
    public static class ObjectContextFactory
    {
        #region Fields

        private static Dictionary<string, MetadataWorkspace> metadataWorkspaceCache = new Dictionary<string, MetadataWorkspace>();
        private static object metadataWorkspaceCacheLock = new object();

        #endregion

        #region Overloads

        /// <summary>
        /// Creates a new <see cref="ObjectContext"/> instance.
        /// </summary>
        /// <typeparam name="TObjectContext">The concrete type of <see cref="ObjectContext"/> to create.</typeparam>
        /// <param name="entityConnectionStringName">The name of the connection string that contains the configuration for the <see cref="ObjectContext"/> to create.</param>
        /// <param name="instantiator">The method to call to create an instance of the <see cref="ObjectContext"/> with an <see cref="EntityConnection"/>.</param>
        /// <returns>A new instance of the requested <see cref="ObjectContext"/> type.</returns>
        public static TObjectContext Create<TObjectContext>(string entityConnectionStringName, Func<EntityConnection, TObjectContext> instantiator) where TObjectContext : ObjectContext
        {
            return Create<TObjectContext>(entityConnectionStringName, instantiator, (string)null);
        }

        /// <summary>
        /// Creates a new <see cref="ObjectContext"/> instance and automatically applies a table prefix to each table in the database.
        /// </summary>
        /// <typeparam name="TObjectContext">The concrete type of <see cref="ObjectContext"/> to create.</typeparam>
        /// <param name="entityConnectionStringName">The name of the connection string that contains the configuration for the <see cref="ObjectContext"/> to create.</param>
        /// <param name="instantiator">The method to call to create an instance of the <see cref="ObjectContext"/> with an <see cref="EntityConnection"/>.</param>
        /// <param name="tablePrefix">The table prefix to apply to each table in the database.</param>
        /// <returns>A new instance of the requested <see cref="ObjectContext"/> type.</returns>
        public static TObjectContext Create<TObjectContext>(string entityConnectionStringName, Func<EntityConnection, TObjectContext> instantiator, string tablePrefix) where TObjectContext : ObjectContext
        {
            Func<string, string> tableNameModifier = null;
            if (!string.IsNullOrEmpty(tablePrefix))
            {
                tableNameModifier = (t => tablePrefix + t);
            }
            return Create<TObjectContext>(entityConnectionStringName, instantiator, tableNameModifier, "prefix:" + tablePrefix);
        }

        #endregion

        #region Create

        /// <summary>
        /// Creates a new <see cref="ObjectContext"/> instance and automatically applies a table name modification to each table in the database.
        /// </summary>
        /// <typeparam name="TObjectContext">The concrete type of <see cref="ObjectContext"/> to create.</typeparam>
        /// <param name="entityConnectionStringName">The name of the connection string that contains the configuration for the <see cref="ObjectContext"/> to create.</param>
        /// <param name="instantiator">The method to call to create an instance of the <see cref="ObjectContext"/> with an <see cref="EntityConnection"/>.</param>
        /// <param name="tableNameModifier">The method to call to modify the name of each table in the database.</param>
        /// <param name="cacheKeyDifferentiator">A unique value for the type of <paramref name="tableNameModifier"/> so that the modification that was performed can be cached.</param>
        /// <returns>A new instance of the requested <see cref="ObjectContext"/> type.</returns>
        private static TObjectContext Create<TObjectContext>(string entityConnectionStringName, Func<EntityConnection, TObjectContext> instantiator, Func<string, string> tableNameModifier, string cacheKeyDifferentiator) where TObjectContext : ObjectContext
        {
            #region Funny Story

            // At first, I had a new() constraint on the type argument TObjectContext so I could new up the
            // instance directly if no table prefix was given. Now it seems that the effect of caching the
            // MetadataWorkspace in this factory speeds up the creation of the ObjectContext *by 10 times*
            // compared to newing up the ObjectContext instance directly. So we now always go through the
            // cached MetadataWorkspace.
            //if (string.IsNullOrEmpty(tablePrefix))
            //{
            //    return new TObjectContext();
            //}

            #endregion

            // Check arguments.
            if (string.IsNullOrEmpty(entityConnectionStringName))
            {
                throw new ArgumentNullException("entityConnectionStringName");
            }
            if (instantiator == null)
            {
                throw new ArgumentNullException("instantiator");
            }

            // Look up the connection string.
            var connectionString = ConfigurationManager.ConnectionStrings[entityConnectionStringName];
            if (connectionString == null)
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, "The connection string with the name \"{0}\" is not configured for this application.", entityConnectionStringName));
            }
            var entityConnectionString = new EntityConnectionStringBuilder(connectionString.ConnectionString);

            // Look up a cached MetadataWorkspace.
            var cacheKey = entityConnectionStringName + "*" + cacheKeyDifferentiator;
            if (!metadataWorkspaceCache.ContainsKey(cacheKey))
            {
                lock (metadataWorkspaceCacheLock)
                {
                    if (!metadataWorkspaceCache.ContainsKey(cacheKey))
                    {
                        // Create the metadata readers.
                        var metadataLocations = entityConnectionString.Metadata.Split('|');
                        var csdlReader = GetXmlReaderForMetadata(metadataLocations[0]);
                        var ssdlReader = GetXmlReaderForMetadata(metadataLocations[1]);
                        var mslReader = GetXmlReaderForMetadata(metadataLocations[2]);

                        // Apply the table modifier if needed.
                        if (tableNameModifier != null)
                        {
                            ssdlReader = ApplyTableNameModifierToSsdl(ssdlReader, tableNameModifier);
                        }

                        // Create the MetadataWorkspace from the metadata readers.
                        var edmCollection = new EdmItemCollection(new XmlReader[] { csdlReader });
                        var storeCollection = new StoreItemCollection(new XmlReader[] { ssdlReader });
                        var storeMappings = new StorageMappingItemCollection(edmCollection, storeCollection, new XmlReader[] { mslReader });
                        var workspace = new MetadataWorkspace();
                        workspace.RegisterItemCollection(edmCollection);
                        workspace.RegisterItemCollection(storeCollection);
                        workspace.RegisterItemCollection(storeMappings);

                        // Cache the MetadataWorkspace.
                        metadataWorkspaceCache[cacheKey] = workspace;
                    }
                }
            }

            // Create an EntityConnection based on the MetadataWorkspace and a database connection.
            var cachedWorkspace = metadataWorkspaceCache[cacheKey];
            var dbProviderFactory = DbProviderFactories.GetFactory(entityConnectionString.Provider);
            var dbConnection = dbProviderFactory.CreateConnection();
            dbConnection.ConnectionString = entityConnectionString.ProviderConnectionString;
            var entityConnection = new EntityConnection(cachedWorkspace, dbConnection);

            // Instantiate the ObjectContext based on the EntityConnection.
            return instantiator(entityConnection);
        }

        #endregion

        #region Helper Methods

        private static XmlReader GetXmlReaderForMetadata(string metadataLocation)
        {
            if (!metadataLocation.StartsWith("res://", StringComparison.OrdinalIgnoreCase))
            {
                // The metadata is a file path, create a reader directly.
                return XmlReader.Create(metadataLocation);
            }
            else
            {
                // The metadata is an embedded resource, extract it out of an assembly.
                var resourceName = metadataLocation.Substring(metadataLocation.LastIndexOf('/') + 1);
                var assemblySpec = metadataLocation.Substring("res://".Length, metadataLocation.Length - resourceName.Length - "res://".Length - 1);
                IEnumerable<Assembly> assemblies;
                if (string.Equals("*", assemblySpec, StringComparison.Ordinal))
                {
                    assemblies = AppDomain.CurrentDomain.GetAssemblies();
                }
                else
                {
                    assemblies = new Assembly[] { Assembly.Load(assemblySpec) };
                }
                foreach (var assembly in assemblies)
                {
                    try
                    {
                        foreach (var foundResourceName in assembly.GetManifestResourceNames())
                        {
                            if (string.Equals(foundResourceName, resourceName, StringComparison.OrdinalIgnoreCase))
                            {
                                return new XmlTextReader(assembly.GetManifestResourceStream(foundResourceName));
                            }
                        }
                    }
                    catch (NotSupportedException)
                    {
                        // Ignore this exception, it can occur on dynamic assemblies.
                    }
                }
                throw new ArgumentException("The given metadata resource could not be found in any of the loaded assemblies.");
            }
        }

        private static XmlReader ApplyTableNameModifierToSsdl(XmlReader ssdlReader, Func<string, string> tableNameModifier)
        {
            // Load the SSDL into memory.
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(ssdlReader);

            // Find all tables, which are in the XML path /Schema/EntityContainer/EntitySet.
            var nsmgr = new XmlNamespaceManager(new NameTable());
            nsmgr.AddNamespace("ssdl", "http://schemas.microsoft.com/ado/2006/04/edm/ssdl");
            foreach (XmlElement node in xmlDoc.SelectNodes("/ssdl:Schema/ssdl:EntityContainer/ssdl:EntitySet", nsmgr))
            {
                var tableAttribute = node.Attributes["Table"];
                if (tableAttribute == null)
                {
                    // There is no Table attribute, create one.
                    tableAttribute = node.OwnerDocument.CreateAttribute("Table");
                    node.Attributes.Append(tableAttribute);
                }
                if (string.IsNullOrEmpty(tableAttribute.Value))
                {
                    // The Table attribute has no value, use the value of the required Name attribute.
                    tableAttribute.Value = node.Attributes["Name"].Value;
                }

                // Rewrite the Table attribute to be the modifed version.
                tableAttribute.Value = tableNameModifier(tableAttribute.Value);
            }
            return XmlReader.Create(new StringReader(xmlDoc.OuterXml));
        }

        #endregion
    }
}