<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<Mayando.Web.ViewModels.PhotosPageViewModel>" MasterPageFile="~/Views/Shared/Site.Master" %>

<asp:content id="title" contentplaceholderid="TitleContent" runat="server">
	<%= Html.Encode(Model.PageTitle) %>
</asp:content>

<asp:content id="content" contentplaceholderid="MainContent" runat="server">
<div id="photoindexpage">
    <h2><%= Html.Encode(Model.PageTitle) %></h2>
    <% Html.RenderPartial(PartialViewName.ListPhotos, Model.Photos); %>
</div>
</asp:content>