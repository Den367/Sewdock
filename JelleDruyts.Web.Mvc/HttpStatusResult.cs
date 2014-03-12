using System;
using System.Net;
using System.Web.Mvc;

namespace JelleDruyts.Web.Mvc
{
    /// <summary>
    /// Represents an <see cref="ActionResult"/> that returns a specified HTTP status code.
    /// </summary>
    public class HttpStatusResult : ActionResult
    {
        #region Properties

        /// <summary>
        /// Gets or sets the HTTP status code for the response.
        /// </summary>
        public HttpStatusCode StatusCode { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpStatusResult"/> class.
        /// </summary>
        /// <remarks>The <see cref="StatusCode"/> will be set to <see cref="HttpStatusCode.OK"/></remarks>
        public HttpStatusResult()
            : this(HttpStatusCode.OK)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpStatusResult"/> class.
        /// </summary>
        /// <param name="statusCode">The status code.</param>
        public HttpStatusResult(HttpStatusCode statusCode)
        {
            this.StatusCode = statusCode;
        }

        #endregion

        #region ExecuteResult

        /// <summary>
        /// Sets the status code on the HTTP response.
        /// </summary>
        /// <param name="context">The context within which the result is executed.</param>
        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            context.HttpContext.Response.StatusCode = (int)this.StatusCode;
        }

        #endregion
    }
}