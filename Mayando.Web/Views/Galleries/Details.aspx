<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<Mayando.Web.ViewModels.GalleryViewModel>" MasterPageFile="~/Views/Shared/Site.Master" %>

<asp:Content ID="title" ContentPlaceHolderID="TitleContent" runat="server">
    <%= Html.Encode(Model.Gallery.Title) %>
</asp:Content>

<asp:Content ID="admin" ContentPlaceHolderID="AdminLinks" runat="server">
              <%--  <%= Html.ActionLinkGalleryCreateChildGallery("Create Child Gallery", Model.Gallery.Id)%>
                <%= Html.ActionLinkGalleryEdit("Edit Gallery", Model.Gallery.Id) %>
                <%= Html.ActionLinkGalleryDelete("Delete Gallery", Model.Gallery.Id)%>--%>
</asp:Content>

<asp:Content ID="content" ContentPlaceHolderID="MainContent" runat="server">
<div id="gallerydetailspage">
    <h2>
        <% if (Model.ParentGalleries.Count == 0) { %>
        <%= Html.Encode(Model.Gallery.Title)%>
        <% } else { %>
        <span class="breadcrumb">
            <% foreach(var parent in Model.ParentGalleries) { %>
         <%--   <span class="breadcrumb-parent"><%= Html.ActionLinkGalleryDetails(parent.Title, parent.Slug) %></span>--%> &raquo;
            <% } %>
            <span class="breadcrumb-current"><%= Html.Encode(Model.Gallery.Title)%></span>
        </span>
        <% } %>
    </h2>
   <%-- <div id="gallery-text"><%= Model.Gallery.Description %></div>--%>
    <% Html.RenderPartial(PartialViewName.ListGalleries, Model.ChildGalleries); %>
<%--    <% Html.RenderPartial(PartialViewName.ListEmbros, Model.Embros); %>--%>
</div>
</asp:Content>