<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<Mayando.Web.Models.Photo>" MasterPageFile="~/Views/Shared/Site.Master" %>

<asp:Content ID="title" ContentPlaceHolderID="TitleContent" runat="server">
	Edit Photo
</asp:Content>

<asp:Content ID="content" ContentPlaceHolderID="MainContent" runat="server">
<div id="photoeditpage">
    <h2>Edit Photo</h2>
    <% using (Html.BeginPhotosEditForm()) {%>
        <% Html.RenderPartial(PartialViewName.EditPhoto); %>
    <% } %>
</div>
</asp:Content>