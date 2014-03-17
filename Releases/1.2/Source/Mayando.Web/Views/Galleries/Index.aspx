<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<ICollection<Mayando.Web.ViewModels.GalleryInfoViewModel>>" MasterPageFile="~/Views/Shared/Site.Master" %>

<asp:Content ID="title" ContentPlaceHolderID="TitleContent" runat="server">
    <%= Html.Encode(Resources.GalleriesTitle) %>
</asp:Content>

<asp:Content ID="content" ContentPlaceHolderID="MainContent" runat="server">
<div id="galleryindexpage">
    <h2><%= Html.Encode(Resources.GalleriesTitle) %></h2>
    <% Html.RenderPartial(PartialViewName.ListGalleries, Model); %>
</div>
</asp:Content>