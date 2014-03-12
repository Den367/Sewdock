<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<Mayando.Web.Models.SiteData>" MasterPageFile="~/Views/Shared/Site.Master" %>

<asp:Content ID="title" ContentPlaceHolderID="TitleContent" runat="server">
    Welcome to <%= Html.Encode(Model.ApplicationName) %>!
</asp:Content>

<asp:Content ID="content" ContentPlaceHolderID="MainContent" runat="server">
<div id="welcomepage">
    <h2>Welcome to <%= Html.Encode(Model.ApplicationName) %>!</h2>
    <p>You have just installed <%= Html.Encode(Model.ApplicationName) %> v<%= Html.Encode(Model.ApplicationDisplayVersion)%>.</p>
    <p>For the latest information, please visit the <%= Html.Link(Model.ApplicationUrl, Model.ApplicationName, true) %> homepage.</p>
    <p>Now that everything is up and running, you will probably want to do the following:</p>
    <ul>
        <li>
            <b><%= Html.ActionLinkAccountLogOn(Resources.AccountLogOn) %>.</b>
            To change the settings for this application, you must be logged on. The user name of the one and only user that can change the settings is "<b><%= Model.AdministratorUserName %></b>".
            If you have not yet changed it yet, the password is "<b><%= Model.AdministratorDefaultPassword %></b>".
        </li>
        <li>
            <b><%= Html.ActionLinkAccountChangePassword(Resources.AccountChangePassword) %>.</b>
            Once you have logged on, you should <%= Html.ActionLinkAccountChangePassword("change your password") %> as soon as possible.
        </li>
        <li>
            <b><%= Html.ActionLinkAdminIndex("Change Settings")%>.</b>
            You can customize your website on the <%= Html.ActionLinkAdminIndex("Site Administration") %> page.
            Here, you can change the title and subtitle, set up email settings, choose a theme and much more.
        </li>
        <li>
            <b><%= Html.ActionLinkPhotoProviderIndex("Select Photo Provider") %>.</b>
            Your photo blog is currently empty. You can either add photos manually, or import them from a photo provider.
        </li>
        <li>
            <b><%= Html.ActionLinkAntiSpamProviderIndex("Select Anti-Spam Provider") %>.</b>
            If you want to keep your site free from comment spam, configure a provider that removes unwanted messages for you.
        </li>
    </ul>
    <p>If you have any questions or issues with this application, please report them on the <%= Html.Link(Model.ApplicationUrl, Model.ApplicationName, true) %> homepage.</p>
    <p>Enjoy your photo blog!</p>
    <p><i>--The <%= Html.Encode(Model.ApplicationName) %> Team.</i></p>
</div>
</asp:Content>