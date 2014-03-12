<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<Mayando.Web.Models.Menu>" MasterPageFile="~/Views/Shared/Site.Master" %>

<asp:Content ID="title" ContentPlaceHolderID="TitleContent" runat="server">
    Edit Menu
</asp:Content>

<asp:Content ID="content" ContentPlaceHolderID="MainContent" runat="server">
<div id="menueditpage">
    <h2>Edit Menu</h2>
    <% using (Html.BeginMenusEditForm()) { %>
        <% Html.RenderPartial(PartialViewName.EditMenu); %>
    <% } %>
</div>
</asp:Content>