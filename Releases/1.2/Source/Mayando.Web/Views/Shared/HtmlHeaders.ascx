<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Mayando.Web.ViewModels.MasterViewModel>" %>
    <meta http-equiv="Content-type" content="text/html;charset=UTF-8" />
    <meta name="description" content="<%= Html.AttributeEncode(Model.SiteData.Settings.Description) %>" />
    <meta name="keywords" content="<%= Html.AttributeEncode(Model.Keywords) %>" />
    <meta name="author" content="<%= Html.AttributeEncode(Model.SiteData.Settings.OwnerName) %>" />
    <meta name="generator" content="<%= Html.AttributeEncode(Model.SiteData.Generator) %>" />
    <link href="<%= Url.Content("~/content/Mayando.ico") %>" rel="icon" type="image/vnd.microsoft.icon" />
    <link href="<%= Url.Content("~/content/Mayando.ico") %>" rel="shortcut icon" type="image/vnd.microsoft.icon" />
    <link href="<%= Url.Content("~/content/Site.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%= Url.ThemedContent("~/content/Theme.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%= Url.FeedPhotos() %>" rel="alternate" type="application/atom+xml" title="<%= Html.AttributeEncode(Resources.SyndicationFeedPhotos) %>" />
    <link href="<%= Url.FeedComments() %>" rel="alternate" type="application/atom+xml" title="<%= Html.AttributeEncode(Resources.SyndicationFeedComments) %>" />
    <script src="<%= Url.Content("~/Scripts/Mayando.js") %>" type="text/javascript"></script>
    <% Html.RenderPartial(PartialViewName.HtmlHeadersForTheme); %>