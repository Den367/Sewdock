<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<Mayando.Web.ViewModels.PhotoProviderViewModel>" MasterPageFile="~/Views/Shared/Site.Master" %>

<asp:content id="title" contentplaceholderid="TitleContent" runat="server">
	Photo Provider
</asp:content>

<asp:content id="content" contentplaceholderid="MainContent" runat="server">
<div id="photoproviderindexpage">
    <h2>Photo Provider</h2>
    <fieldset>
        <legend>Provider</legend>
        <% using (Html.BeginPhotoProviderSelectProviderForm()) { %>
        <p>
            <label for="SelectedProvider" class="inline required">Photo Provider:</label>
            <%= Html.DropDownList("SelectedProvider", Model.AvailableProviders)%>
            <input name="submitButton" type="submit" value="Change" />
            <span title="Attention: changing the provider will reset it and clear all its settings and synchronization information!" class="help">!</span>
        </p>
        <% } %>
        <% if(Model.Provider != null) { %>
        <fieldset class="inline">
            <legend>Provider Description</legend>
            <p><%= Html.Encode(Model.Provider.Description) %></p>
            <p><%= Html.Link(Model.Provider.Url, Model.Provider.Url, true) %></p>
        </fieldset>
            <% using (Html.BeginPhotoProviderResetForm()) { %>
        <p>
            <input name="submitButton" type="submit" value="Reset Provider" <%= (Model.ProviderStatus.CanReset ? string.Empty : "disabled=\"disabled\" ") %>/>
            <span title="Attention: resetting the provider will clear all its settings and synchronization information!" class="help">!</span>
        </p>
            <% } %>
        <% } %>
    </fieldset>
    <% if(Model.Provider != null) { %>
        <% if (Model.Settings != null && Model.Settings.UserVisibleSettings.Count > 0)
           {
               using (Html.BeginPhotoProviderEditForm())
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
    <fieldset>
        <legend>Synchronization</legend>
        <fieldset class="inline">
            <legend>Manual Synchronization</legend>
            <% using (Html.BeginPhotoProviderSynchronizeForm("submitSynchronize")) { %>
                <p>
                    <label for="SyncStartTime" class="required">Include all changes since:</label>
                    <%= Html.TextBox("SyncStartTime", Model.ProposedSyncStartTime.ToEditString()) %>
                </p>
                <p>
                    <label for="TagList" class="optional">Include only photos with all of the following tags (comma separated):</label>
                    <% Html.RenderPartial(PartialViewName.InputTags); %>
                </p>
                <p>
                    <%= Html.CheckBox("Simulate", false) %>
                    <label for="Simulate" class="inline optional">Simulate (no changes will actually be saved)</label>                
                </p>
                <p>
                    <input name="submitButton" id="submitSynchronize" type="submit" value="Synchronize Now" <%= (Model.ProviderStatus.CanSynchronize && !Model.PhotoProviderIsSynchronizing ? string.Empty : "disabled=\"disabled\"") %>/>
                    <% if (Model.PhotoProviderIsSynchronizing) { %>
                    <b>The photo provider is already synchronizing in the background.</b>
                    <% } %>
                </p>
            <% } %>
        </fieldset>
        <fieldset class="inline">
            <legend>Automatic Synchronization</legend>
            <% using (Html.BeginPhotoProviderSaveAutoSyncForm()) { %>
                <p>
                    <%= Html.CheckBox("AutoSyncEnabled", Model.AutoSyncEnabled) %> <label class="inline" for="AutoSyncEnabled">Enable automatic synchronization</label>
                </p>
                <p>
                    <label for="AutoSyncIntervalMinutes" class="optional">Time to wait (in minutes) between automatic synchronizations:</label>
                    <%= Html.TextBox("AutoSyncIntervalMinutes", Model.AutoSyncIntervalMinutes) %>
                </p>
                <p>
                    <label for="TagList" class="optional">Include only photos with all of the following tags (comma separated):</label>
                    <% Html.RenderPartial(PartialViewName.InputTags); %>
                </p>
                <p>
                    <input name="submitButton" type="submit" value="Save" />
                </p>
            <% } %>
        </fieldset>
        <fieldset class="inline">
            <legend>Synchronization Status</legend>
            <% if (Model.SyncStatus.LastSyncWasSimulation == true) { %>
            <p><b>Note:</b> the last synchronization was a simulation, no changes were actually saved.</p>
            <% } %>
            <p>
                <label class="inline">Last synchronization result:</label>
                <% if (Model.SyncStatus.LastSyncSucceeded.HasValue) { %>
                <%= Html.Encode(Model.SyncStatus.LastSyncSucceeded.Value ? "Succeeded" : "Failed")%>.
                <% } else { %>
                   Not synchronized yet.
                <% } %>
            </p>
            <p>
                <label class="inline">Last successfull synchronization:</label>
                <% if (Model.SyncStatus.LastSyncTime.HasValue) { %>
                <%= Html.Encode(Model.SyncStatus.LastSyncTime.Value.ToAdjustedDisplayString())%>.
                <% } else { %>
                   Not synchronized yet.
                <% } %>
            </p>
            <p>
                <label class="inline">Number of new photos:</label>
                <% if (Model.SyncStatus.LastSyncNewPhotos.HasValue) { %>
                <%= Html.Encode(Model.SyncStatus.LastSyncNewPhotos.Value.ToString())%>.
                <% } else { %>
                   No photos were retrieved.
                <% } %>
            </p>
            <p>
                <label class="inline">Number of new comments:</label>
                <% if (Model.SyncStatus.LastSyncNewComments.HasValue) { %>
                <%= Html.Encode(Model.SyncStatus.LastSyncNewComments.Value.ToString())%>.
                <% } else { %>
                   No comments were retrieved.
                <% } %>
            </p>
        </fieldset>
        <% if (!string.IsNullOrEmpty(Model.SyncStatus.LastSyncStatus)) { %>
        <fieldset class="inline">
            <legend>Status Description</legend>
            <%= Model.SyncStatus.LastSyncStatus %>
        </fieldset>
        <% } %>
    </fieldset>
    <% } %>
</div>
</asp:content>