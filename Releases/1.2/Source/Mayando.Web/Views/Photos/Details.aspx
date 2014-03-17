<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<Mayando.Web.ViewModels.PhotoViewModel>" MasterPageFile="~/Views/Shared/Site.Master" %>

<asp:Content ID="title" ContentPlaceHolderID="TitleContent" runat="server">
    <%= Html.Encode(Model.PhotoDetails.Photo.DisplayTitle)%>
</asp:Content>

<asp:Content ID="admin" ContentPlaceHolderID="AdminLinks" runat="server">
                <%= Html.ActionLinkPhotoEdit("Edit Photo", Model.PhotoDetails.Photo.Id) %>
                <%= Html.ActionLinkPhotoDelete("Delete Photo", Model.PhotoDetails.Photo.Id) %>
                <%= Html.ActionLinkPageCreate("Create Page With Photo", Model.PhotoDetails.Photo.Id) %>
</asp:Content>

<asp:Content ID="content" ContentPlaceHolderID="MainContent" runat="server">
<div id="photodetailspage">
    <% if (Model.PhotoDetails.Photo.Hidden) { %>
    <div class="pageflash">This photo is hidden.</div>
    <% } %>
    <% Html.RenderPartial(PartialViewName.NavigationContext, Model.NavigationContext); %>
    <h2><%= Html.Encode(Model.PhotoDetails.Photo.DisplayTitle)%></h2>
    <% using (Html.BeginPhotosAddCommentForm("submitComment")) { %>
        <% Html.RenderPartial(PartialViewName.PhotoDetails, Model.PhotoDetails); %>
    <% } %>
</div>
</asp:Content>