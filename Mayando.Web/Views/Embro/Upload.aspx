<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<Mayando.Web.Models.EmbroideryItem>" MasterPageFile="~/Views/Shared/Site.Master" %>

<asp:Content ID="title" ContentPlaceHolderID="TitleContent" runat="server">
	Upload Embro
</asp:Content>
<asp:Content ID="content" ContentPlaceHolderID="MainContent" runat="server">
<div id="photouploadtpage">
    <h2>Upload Embro</h2>
        <% Html.RenderPartial(PartialViewName.UploadEmbro); %>
</div>
</asp:Content>