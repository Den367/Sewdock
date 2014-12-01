<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<Myembro.Models.Page>" MasterPageFile="~/Views/Shared/Site.Master" %>

<asp:Content ID="title" ContentPlaceHolderID="TitleContent" runat="server">
    Create Page
</asp:Content>

<asp:Content ID="content" ContentPlaceHolderID="MainContent" runat="server">
<div id="pagecreatepage">
    <h2>Create Page</h2>
    <% using (Html.BeginPagesCreateForm()) {%>
        <% Html.RenderPartial(PartialViewName.EditPage); %>
    <% } %>
</div>
</asp:Content>