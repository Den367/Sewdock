<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" MasterPageFile="~/Views/Shared/Site.Master" %>

<asp:Content ID="title" ContentPlaceHolderID="TitleContent" runat="server">
	<%= Html.Encode(Resources.ContactTitle) %>
</asp:Content>

<asp:Content ID="content" ContentPlaceHolderID="MainContent" runat="server">
<div id="contactindexpage">
    <h2><%= Html.Encode(Resources.ContactTitle) %></h2>
    <% Html.RenderPartial(PartialViewName.ContactForm); %>
</div>
</asp:Content>