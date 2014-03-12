<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<Mayando.Web.Models.Page>" MasterPageFile="~/Views/Shared/Site.Master" %>

<asp:Content ID="title" ContentPlaceHolderID="TitleContent" runat="server">
    Edit Page
</asp:Content>

<asp:Content ID="content" ContentPlaceHolderID="MainContent" runat="server">
<div id="pageeditpage">
    <h2>Edit Page</h2>
    <% using (Html.BeginPagesEditForm()) {%>
        <% Html.RenderPartial(PartialViewName.EditPage); %>
    <% } %>
</div>
</asp:Content>