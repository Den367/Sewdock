<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<Mayando.Web.Models.Comment>" MasterPageFile="~/Views/Shared/Site.Master" %>

<asp:Content ID="title" ContentPlaceHolderID="TitleContent" runat="server">
    Edit Comment
</asp:Content>

<asp:Content ID="content" ContentPlaceHolderID="MainContent" runat="server">
<div id="commenteditpage">
    <h2>Edit Comment</h2>
    <% using (Html.BeginCommentsEditForm()) { %>
        <% Html.RenderPartial(PartialViewName.EditComment); %>
    <% } %>
</div>
</asp:Content>