<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<Mayando.Web.Models.Gallery>" MasterPageFile="~/Views/Shared/Site.Master" %>

<asp:Content ID="title" ContentPlaceHolderID="TitleContent" runat="server">
    Create Gallery
</asp:Content>

<asp:Content ID="content" ContentPlaceHolderID="MainContent" runat="server">
<div id="gallerycreatepage">
    <h2>Create Gallery</h2>
    <% using (Html.BeginGalleriesCreateForm()) { %>
        <% Html.RenderPartial(PartialViewName.EditGallery); %>
    <% } %>
</div>
</asp:Content>