<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<Myembro.Models.Menu>" MasterPageFile="~/Views/Shared/Site.Master" %>

<asp:Content ID="title" ContentPlaceHolderID="TitleContent" runat="server">
    Create Menu
</asp:Content>

<asp:Content ID="content" ContentPlaceHolderID="MainContent" runat="server">
<div id="menucreatepage">
    <h2>Create Menu</h2>
    <% using (Html.BeginMenusCreateForm()) { %>
        <% Html.RenderPartial(PartialViewName.EditMenu); %>
    <% } %>
</div>
</asp:Content>
