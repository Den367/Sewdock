<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<Mayando.Web.ViewModels.AntiSpamProviderViewModel>" MasterPageFile="~/Views/Shared/Site.Master" %>

<asp:content id="title" contentplaceholderid="TitleContent" runat="server">
	Anti-Spam Provider
</asp:content>

<asp:content id="content" contentplaceholderid="MainContent" runat="server">
<div id="antispamproviderindexpage">
    <h2>Anti-Spam Provider</h2>
    <fieldset>
        <legend>Provider</legend>
        <% using (Html.BeginAntiSpamProviderSelectProviderForm()) { %>
        <p>
            <label for="SelectedProvider" class="inline required">Anti-Spam Provider:</label>
            <%= Html.DropDownList("SelectedProvider", Model.AvailableProviders) %>
            <input name="submitButton" type="submit" value="Change" />
            <span title="Attention: changing the provider will reset it and clear all its settings!" class="help">!</span>
        </p>
        <% } %>
        <% if (Model.Provider != null) { %>
        <fieldset class="inline">
            <legend>Provider Description</legend>
            <p><%= Html.Encode(Model.Provider.Description) %></p>
            <p><%= Html.Link(Model.Provider.Url, Model.Provider.Url, true) %></p>
        </fieldset>
            <% using (Html.BeginAntiSpamProviderResetForm()) { %>
        <p>
            <input name="submitButton" type="submit" value="Reset Provider" />
            <span title="Attention: resetting the provider will clear all its settings and synchronization information!" class="help">!</span>
        </p>
            <% } %>
        <% } %>
    </fieldset>
    <% if(Model.Provider != null) { %>
        <% if (Model.Settings != null && Model.Settings.UserVisibleSettings.Count > 0)
           {
               using (Html.BeginAntiSpamProviderEditForm())
               { %>
    <fieldset>
        <legend>Provider Settings</legend>
        <% Html.RenderPartial(PartialViewName.ListSettings, Model.Settings); %>
    </fieldset>
        <%     }
           } %>
    <fieldset>
        <legend>Provider Status</legend>
        <%= Model.ProviderStatus.StatusDescriptionHtml %>
    </fieldset>
    <% } %>
</div>
</asp:content>