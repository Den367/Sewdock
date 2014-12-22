<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<Myembro.Models.Gallery>" MasterPageFile="~/Views/Shared/Site.Master" %>

<asp:Content ID="title" ContentPlaceHolderID="TitleContent" runat="server">
    Edit Gallery
</asp:Content>

<asp:Content ID="content" ContentPlaceHolderID="MainContent" runat="server">
<div id="galleryeditpage">
    <h2>Edit Gallery</h2>
    <% using (Html.BeginGalleriesEditForm()) { %>
        <% Html.RenderPartial(PartialViewName.EditGallery); %>
    <% } %>
</div>
</asp:Content>