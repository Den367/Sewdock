<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<Mayando.Web.ViewModels.AdminViewModel>" MasterPageFile="~/Views/Shared/Site.Master" %>

<asp:content id="title" contentplaceholderid="TitleContent" runat="server">
	Site Administration
</asp:content>

<asp:content id="content" contentplaceholderid="MainContent" runat="server">
<div id="adminindexpage">
    <h2>Site Administration</h2>
    <table id="admindashboard">
        <tr>
            <td>
                <h3>Site Content</h3>
                <p><%= Html.ActionLinkMenusManage("Manage Menus")%></p>
                <p><%= Html.ActionLinkGalleriesManage("Manage Galleries")%></p>
                <p><%= Html.ActionLinkPagesManage("Manage Pages")%></p>
                <p><%= Html.ActionLinkSettingsUser("Manage User-Defined Settings")%></p>
            </td>
            <td>
                <h3>Providers &amp; Services</h3>
                <p><%= Html.ActionLinkPhotoProviderIndex("Manage Photo Provider")%></p>
                <p><%= Html.ActionLinkAntiSpamProviderIndex("Manage Anti-Spam Provider")%></p>
                <p><%= Html.ActionLinkServicesIndex("Manage Service API")%></p>
            </td>
            <td>
                <h3>Information</h3>
                <p><%= Html.ActionLinkAdminAbout("About...")%></p>
                <p><%= Html.ActionLinkAdminEventLog("Show the event log")%></p>
                <p><%= Html.ActionLinkAdminUrls("Show all available URL's")%></p>
            </td>
        </tr>
    </table>
    
    <h3>Application Settings</h3>
    <% using (Html.BeginSettingsEditForm()) {
        Html.RenderPartial(PartialViewName.ListSettings, Model.Settings);  
    } %>

    <h3>Test Settings</h3>
    <% using (Html.BeginAdminEmailTestForm()) { %>
        <fieldset>
            <legend>Email Test</legend>
            <p>Test your email settings by sending a test email to your notification address.</p>
            <p><input type="submit" value="Send Test Email" /></p>
        </fieldset>
    <% } %>
</div>
</asp:content>