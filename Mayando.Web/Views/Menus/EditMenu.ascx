<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Mayando.Web.Models.Menu>" %>
        <%= Html.ValidationSummary("The item could not be saved. Please correct any errors and try again.")%>
        <%= Html.Hidden("Sequence") %>
        <fieldset>
            <legend>Menu</legend>
            <p>
                <label for="Title" class="required">Title:</label>
                <%= Html.TextBox("Title", Model.Title) %>
                <%= Html.ValidationMessage("Title", "*") %>
            </p>
            <p>
                <label for="Url" class="required">URL:</label>
                <%= Html.TextBox("Url", Model.Url) %>
                <span title="Use a ~ in front of the URL to represent the site's root path; e.g. ~/photos will always be correct regardless of the path" class="help">?</span>
                <%= Html.ValidationMessage("Url", "*") %>
                <%= Html.ActionLinkAdminUrls("Show all available URL's") %>                
            </p>
            <p>
                <label for="ToolTip" class="optional">Tool Tip:</label>
                <%= Html.TextBox("ToolTip", Model.ToolTip)%>
                <%= Html.ValidationMessage("ToolTip", "*")%>
            </p>
            <p>
                <%= Html.CheckBox("OpenInNewWindow", Model.OpenInNewWindow) %>
                <label for="OpenInNewWindow" class="inline required">Open in new window</label>
                <%= Html.ValidationMessage("OpenInNewWindow", "*") %>
            </p>
            <p>
                <input type="submit" value="Save" />
            </p>
        </fieldset>