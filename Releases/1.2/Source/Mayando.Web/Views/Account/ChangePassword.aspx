<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<Mayando.Web.ViewModels.AccountViewModel>" MasterPageFile="~/Views/Shared/Site.Master" %>

<asp:Content ID="title" ContentPlaceHolderID="TitleContent" runat="server">
    Change Password
</asp:Content>

<asp:Content ID="content" ContentPlaceHolderID="MainContent" runat="server">
<div id="accountchangepasswordpage">
    <h2>Change Password</h2>
    <p>Use the form below to change your password.</p>
    <p>New passwords are required to be a minimum of <%=Html.Encode(Model.MinPasswordLength)%> characters in length.</p>
    <%= Html.ValidationSummary("Password change was unsuccessful. Please correct the errors and try again.")%>
    <% using (Html.BeginAccountChangePasswordForm()) { %>
        <div>
            <fieldset>
                <legend>Account Information</legend>
                <p>
                    <label for="currentPassword" class="required">Current password:</label>
                    <%= Html.Password("currentPassword") %>
                    <%= Html.ValidationMessage("currentPassword") %>
                </p>
                <p>
                    <label for="newPassword" class="required">New password:</label>
                    <%= Html.Password("newPassword") %>
                    <%= Html.ValidationMessage("newPassword") %>
                </p>
                <p>
                    <label for="confirmPassword" class="required">Confirm new password:</label>
                    <%= Html.Password("confirmPassword") %>
                    <%= Html.ValidationMessage("confirmPassword") %>
                </p>
                <p>
                    <input type="submit" value="Change Password" />
                </p>
            </fieldset>
        </div>
    <% } %>
</div>
</asp:Content>