<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<Mayando.Web.Models.EmbroideryItem>" MasterPageFile="~/Views/Shared/Site.Master" %>

<asp:Content ID="title" ContentPlaceHolderID="TitleContent" runat="server">
	Create EmbroideryItem
</asp:Content>

<asp:Content ID="content" ContentPlaceHolderID="MainContent" runat="server">
<div id="photocreatepage">
    <h2>Create EmbroideryItem</h2>
    <% using (Html.BeginPhotosCreateForm()) {%>
        <% Html.RenderPartial(PartialViewName.EditPhoto); %>
    <% } %>
</div>
</asp:Content>