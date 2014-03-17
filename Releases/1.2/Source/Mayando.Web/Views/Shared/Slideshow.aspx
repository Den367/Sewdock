<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<Mayando.Web.ViewModels.PhotoViewModel>" MasterPageFile="~/Views/Shared/Slideshow.Master" %>

<asp:Content ID="title" ContentPlaceHolderID="TitleContent" runat="server">
    <%= Html.Encode(Model.PhotoDetails.Photo.DisplayTitle)%>
</asp:Content>

<asp:Content ID="headerSubtitle" ContentPlaceHolderID="HeaderSubtitleContent" runat="server">
    <%= Html.Encode(Model.PhotoDetails.Photo.DisplayTitle)%>
</asp:Content>

<asp:Content ID="content" ContentPlaceHolderID="MainContent" runat="server">
<div id="photodetailspage">
    <% if (Model.PhotoDetails.Photo.Hidden) { %>
    <div class="pageflash">This photo is hidden.</div>
    <% } %>
    <% Html.RenderPartial(PartialViewName.NavigationContext, Model.NavigationContext); %>
    <div id="photocontainer">
        <% Html.RenderPartial(PartialViewName.PhotoDetailsImage, Model.PhotoDetails); %>
        <% if (!Model.PhotoDetails.HidePhotoText) { %>
        <div id="photo-text">
            <%= Model.PhotoDetails.Photo.Text%>
        </div>
        <% } %>
    </div>
</div>
</asp:Content>