<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" MasterPageFile="~/Views/Shared/Site.Master" %>

<asp:Content ID="title" ContentPlaceHolderID="TitleContent" runat="server">
    <%= Html.Encode(Resources.AccountLogOn) %>
</asp:Content>

<asp:Content ID="content" ContentPlaceHolderID="MainContent" runat="server">
<div id="accountlogonpage">
    <h2><%= Html.Encode(Resources.AccountLogOn) %></h2>
    <p><%= Html.Encode(Resources.LogOnEnterCredentials) %></p>
    <%= Html.ValidationSummary(Resources.LogOnFailed) %>
    <% using (Html.BeginAccountLogOnForm()) { %>
        <%= Html.Hidden("returnUrl")%>
        <div>
            <fieldset>
                <legend><%= Html.Encode(Resources.LogOnAccountInformation) %></legend>
                <p>
                    <label for="username" class="required"><%= Html.Encode(Resources.LogOnUserName) %></label>
                    <%= Html.TextBox("username") %>
                    <%= Html.ValidationMessage("username") %>
                </p>
                <p>
                    <label for="password" class="required"><%= Resources.LogOnPassword %></label>
                    <%= Html.Password("password") %>
                    <%= Html.ValidationMessage("password") %>
                </p>
                <p>
                    <%= Html.CheckBox("rememberMe") %> <label class="inline optional" for="rememberMe"><%= Resources.LogOnRememberMe %></label>
                </p>
                <p>
                    <input type="submit" value="<%= Html.AttributeEncode(Resources.LogOnSubmit) %>" />
                </p>
            </fieldset>
        </div>
    <% } %>
</div>
</asp:Content>