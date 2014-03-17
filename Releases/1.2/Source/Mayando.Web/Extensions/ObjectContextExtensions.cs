using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.EntityClient;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Reflection;
using System.Text;
using Mayando.Web.Models;

namespace Mayando.Web.Extensions
{
    /// <summary>
    /// Provides extension methods for <see cref="ObjectContext"/> instances.
    /// </summary>
    public static class ObjectContextExtensions
    {
        #region AttachTo

        /// <summary>
        /// Attaches an entity to the context.
        /// </summary>
        /// <param name="context">The context to add the entity to.</param>
        /// <param name="entitySetName">The entity set name to which the entity should be added.</param>
        /// <param name="entity">The entity to add.</param>
        public static void AttachTo(this ObjectContext context, EntitySetName entitySetName, object entity)
        {
            context.AttachTo(entitySetName.ToString(), entity);
        }

        #endregion

        #region AttachAsModified

        /// <summary>
        /// Attaches an entity to the context and marks all properties as modified.
        /// </summary>
        /// <param name="context">The context to add the entity to.</param>
        /// <param name="entitySetName">The entity set name to which the entity should be added.</param>
        /// <param name="entity">The entity to add.</param>
        public static void AttachAsModified(this ObjectContext context, EntitySetName entitySetName, EntityObject entity)
        {
            context.AttachAsModified(entitySetName.ToString(), entity);
        }

        /// <summary>
        /// Attaches an entity to the context and marks all properties as modified.
        /// </summary>
        /// <param name="context">The context to add the entity to.</param>
        /// <param name="entitySetName">The entity set name to which the entity should be added.</param>
        /// <param name="entity">The entity to add.</param>
        /// <remarks>
        /// Adapted from: http://blogs.msdn.com/dsimmons/archive/2008/10/31/attachasmodified-a-small-step-toward-simplifying-ef-n-tier-patterns.aspx.
        /// </remarks>
        public static void AttachAsModified(this ObjectContext context, string entitySetName, EntityObject entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            if (entity.EntityState != EntityState.Detached)
            {
                throw new ArgumentException("The entity must be detached for it to be attached.");
            }

            // Attach the updated entity.
            context.AttachTo(entitySetName, entity);

            // Mark all properties as modified to make sure all changes are persisted.
            var stateEntry = context.ObjectStateManager.GetObjectStateEntry(entity);
            foreach (var propertyName in from fm in stateEntry.CurrentValues.DataRecordInfo.FieldMetadata
                                         select fm.FieldType.Name)
            {
                stateEntry.SetModifiedProperty(propertyName);
            }
        }

        #endregion

        #region Tracing

        /// <summary>
        /// Returns a trace string for the given query.
        /// </summary>
        /// <param name="query">The query to trace.</param>
        /// <returns>A trace string that shows what is executed when the given query is enumerated.</returns>
        /// <remarks>
        /// Adapted from: http://social.msdn.microsoft.com/Forums/en-US/adodotnetentityframework/thread/2a50ffd2-ed73-411d-82bc-c9c564623cb4/.
        /// </remarks>
        public static string ToTraceString(this IQueryable query)
        {
            MethodInfo toTraceStringMethod = query.GetType().GetMethod("ToTraceString");

            if (toTraceStringMethod != null)
            {
                return toTraceStringMethod.Invoke(query, null).ToString();
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Returns a trace string for the given object context.
        /// </summary>
        /// <param name="context">The context to trace.</param>
        /// <returns>A trace string that shows what is executed when the given object context is saved.</returns>
        /// <remarks>
        /// Adapted from: http://social.msdn.microsoft.com/Forums/en-US/adodotnetentityframework/thread/2a50ffd2-ed73-411d-82bc-c9c564623cb4/.
        /// </remarks>
        public static string ToTraceString(this ObjectContext context)
        {
            Assembly entityAssemly = typeof(EntityConnection).Assembly;
            Type updateTranslatorType = entityAssemly.GetType("System.Data.Mapping.Update.Internal.UpdateTranslator");
            Type functionUpdateCommandType = entityAssemly.GetType("System.Data.Mapping.Update.Internal.FunctionUpdateCommand");
            Type dynamicUpdateCommandType = entityAssemly.GetType("System.Data.Mapping.Update.Internal.DynamicUpdateCommand");

            object[] ctorParams = new object[]
                        {
                            context.ObjectStateManager,
                            ((EntityConnection)context.Connection).GetMetadataWorkspace(),
                            (EntityConnection)context.Connection,
                            context.CommandTimeout
                        };

            object updateTranslator = Activator.CreateInstance(updateTranslatorType, BindingFlags.NonPublic | BindingFlags.Instance, null, ctorParams, null);

            MethodInfo produceCommandsMethod = updateTranslatorType.GetMethod("ProduceCommands", BindingFlags.Instance | BindingFlags.NonPublic);
            object updateCommands = produceCommandsMethod.Invoke(updateTranslator, null);

            List<DbCommand> dbCommands = new List<DbCommand>();

            foreach (object o in (IEnumerable)updateCommands)
            {
                if (functionUpdateCommandType.IsInstanceOfType(o))
                {
                    FieldInfo m_dbCommandField = functionUpdateCommandType.GetField("m_dbCommand", BindingFlags.Instance | BindingFlags.NonPublic);

                    dbCommands.Add((DbCommand)m_dbCommandField.GetValue(o));
                }
                else if (dynamicUpdateCommandType.IsInstanceOfType(o))
                {
                    MethodInfo createCommandMethod = dynamicUpdateCommandType.GetMethod("CreateCommand", BindingFlags.Instance | BindingFlags.NonPublic);

                    object[] methodParams = new object[]
                    {
                        updateTranslator,
                        new Dictionary<long, object>()
                    };

                    dbCommands.Add((DbCommand)createCommandMethod.Invoke(o, methodParams));
                }
                else
                {
                    throw new NotSupportedException("Unknown UpdateCommand Kind");
                }
            }

            StringBuilder traceString = new StringBuilder();
            foreach (DbCommand command in dbCommands)
            {
                traceString.AppendLine("=============== BEGIN COMMAND ===============");
                traceString.AppendLine();

                traceString.AppendLine(command.CommandText);
                foreach (DbParameter param in command.Parameters)
                {
                    traceString.AppendFormat("{0} = {1}", param.ParameterName, param.Value);
                    traceString.AppendLine();
                }

                traceString.AppendLine();
                traceString.AppendLine("=============== END COMMAND ===============");
            }

            return traceString.ToString();
        }

        #endregion
    }
}