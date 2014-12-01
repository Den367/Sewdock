using System;
using System.Web.Mvc;

namespace Myembro.Infrastructure
{
    /// <summary>
    /// Logs unhandled errors when they occur.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public sealed class LogErrorAttribute : FilterAttribute, IExceptionFilter
    {
        /// <summary>
        /// Called when an exception occurs.
        /// </summary>
        /// <param name="filterContext">The filter context.</param>
        public void OnException(ExceptionContext filterContext)
        {
            Logger.LogException(filterContext.Exception);
        }
    }
}