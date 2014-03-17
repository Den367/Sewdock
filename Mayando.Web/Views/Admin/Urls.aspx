<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<JelleDruyts.Web.Mvc.Discovery.WebsiteInfo>" MasterPageFile="~/Views/Shared/Site.Master" %>

<asp:Content ID="title" ContentPlaceHolderID="TitleContent" runat="server">
	Available URL's
</asp:Content>

<asp:Content ID="content" ContentPlaceHolderID="MainContent" runat="server">
<div id="adminurlspage">
    <h2>Available URL's</h2>
    <h3>Public URL's</h3>
    <% Html.RenderPartialListControllerActionsForPublic(Model.Controllers); %>
    <h3>Administrator URL's</h3>
    <% Html.RenderPartialListControllerActionsForAdministrator(Model.Controllers); %>
</div>
</asp:Content>