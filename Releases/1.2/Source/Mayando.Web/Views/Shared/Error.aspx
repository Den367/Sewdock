<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<System.Web.Mvc.HandleErrorInfo>" MasterPageFile="~/Views/Shared/Basic.Master" %>

<asp:Content ID="title" ContentPlaceHolderID="TitleContent" runat="server">
    <%= Html.Encode(Resources.ErrorTitle) %>
</asp:Content>

<asp:Content ID="content" ContentPlaceHolderID="MainContent" runat="server">
<div id="errorpage">
    <h2><%= Html.Encode(Resources.ErrorMessage) %></h2>
    <p><%= Html.Encode(Resources.ErrorDescription) %></p>
    <% if (Model != null && Model.Exception != null) { %>
    <p class="error"><%= Html.Encode(Model.Exception.Message) %></p>
    <% } %>
</div>
</asp:Content>