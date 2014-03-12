<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<Mayando.Web.ViewModels.DeleteViewModel>" MasterPageFile="~/Views/Shared/Site.Master" %>

<asp:Content ID="title" ContentPlaceHolderID="TitleContent" runat="server">
    Delete <%= Html.Encode(Model.ItemName) %>
</asp:Content>

<asp:Content ID="content" ContentPlaceHolderID="MainContent" runat="server">
<div id="deletepage">
    <h2>Delete <%= Html.Encode(Model.ItemName) %></h2>
    <p>Are you sure you want to delete &quot;<%= Html.Encode(Model.ItemDescription) %>&quot;?</p>
    <% using (Html.BeginForm()) { %>
        <%= Html.Hidden("returnUrl")%>
        <input name="submitButton" type="submit" value="Delete" />
    <% } %>
</div>
</asp:Content>