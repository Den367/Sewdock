using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

namespace Mayando.Web.Extensions
{
    public static class AjaxHelperExtension
    {
        public static IHtmlString ImageActionLink(this AjaxHelper helper, string imageBase64Data, string actionName, string controllerName,object routeValues, AjaxOptions ajaxOptions)
        {
            var builder = new TagBuilder("img");
            builder.MergeAttribute("src", string.Format("data:image;base64,{0}",imageBase64Data));
            builder.MergeAttribute("width", "100");
            builder.MergeAttribute("height", "auto");
            var link = helper.ActionLink("[replaceme]", actionName, controllerName,routeValues, ajaxOptions).ToString();

            return MvcHtmlString.Create(link.Replace("[replaceme]", builder.ToString(TagRenderMode.SelfClosing)));
        }
    }
}