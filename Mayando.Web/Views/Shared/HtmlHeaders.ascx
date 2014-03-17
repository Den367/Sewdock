<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Mayando.Web.ViewModels.MasterViewModel>" %>
    <meta http-equiv="Content-type" content="text/html;charset=UTF-8" />
    <meta name="description" content="<%= Html.AttributeEncode(Model.SiteData.Settings.Description) %>" />
    <meta name="keywords" content="<%= Html.AttributeEncode(Model.Keywords) %>" />
    <meta name="author" content="<%= Html.AttributeEncode(Model.SiteData.Settings.OwnerName) %>" />
    <meta name="generator" content="<%= Html.AttributeEncode(Model.SiteData.Generator) %>" />
    <link href="<%= Url.Content("~/content/ThreadSmiling.png") %>" rel="icon" type="image/vnd.microsoft.icon" />
    <link href="<%= Url.Content("~/content/ThreadSmiling.png") %>" rel="shortcut icon" type="image/vnd.microsoft.icon" />
     <link href="<%= Url.ThemedContent("~/content/Theme.css") %>" rel="stylesheet" type="text/css" />
     <link href="<%= Url.ThemedContent("~/content/jquery-ui-1.8.9.custom.css") %>" rel="stylesheet" type="text/css" />
 <link href="<%= Url.Content("~/content/bootstrap.css") %>" rel="stylesheet" type="text/css" />
<link href="<%= Url.Content("~/content/polyglot-language-switcher.css") %>" type="text/css" rel="stylesheet">
   <%-- <link href="<%= Url.Content("~/content/screen_preview.css") %>" rel="stylesheet" type="text/css" />   --%>
    <link href="<%= Url.FeedPhotos() %>" rel="alternate" type="application/atom+xml" title="<%= Html.AttributeEncode(Resources.SyndicationFeedPhotos) %>" />
    <link href="<%= Url.FeedComments() %>" rel="alternate" type="application/atom+xml" title="<%= Html.AttributeEncode(Resources.SyndicationFeedComments) %>" />
    <script src="<%= Url.Content("~/Scripts/jquery-1.7.2.min.js") %>" type="text/javascript"></script>
<script src="<%= Url.Content("~/Scripts/jquery.unobtrusive-ajax.min.js") %>" type="text/javascript"></script>        
<script src="<%= Url.Content("~/Scripts/jquery-ui-1.8.11.min.js") %>" type="text/javascript" defer></script>
<script src="<%= Url.Content("~/Scripts/jquery.validate.min.js") %>" type="text/javascript"></script>
<script src="<%= Url.Content("~/Scripts/jquery.validate.unobtrusive.min.js") %>" type="text/javascript"></script>
    <script src="<%= Url.Content("~/Scripts/globalize.js") %>" type="text/javascript" defer></script>
    <script src="<%= Url.Content("~/Scripts/jquery.cookie.js") %>" type="text/javascript" defer></script>
<%--<script src="<%= Url.Content("~/Scripts/MicrosoftAjax.js") %>" type="text/javascript"></script>
	<script src="@<%= Url.Content("~/Scripts/MicrosoftMvcAjax.js") %>" type="text/javascript"></script>--%>
    <script src="<%= Url.Content("~/Scripts/Mayando.js") %>" type="text/javascript"></script>
    <script src="<%= Url.Content("~/Scripts/jquery.polyglot.language.switcher.js") %>" type="text/javascript"></script>      
    <% Html.RenderPartial(PartialViewName.HtmlHeadersForTheme); %>
