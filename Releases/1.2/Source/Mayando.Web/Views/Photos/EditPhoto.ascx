<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Mayando.Web.Models.Photo>" %>
        <%= Html.ValidationSummary("The item could not be saved. Please correct any errors and try again.") %>
        <fieldset>
            <legend>Photo</legend>
            <p>
                <label for="Title" class="optional">Title:</label>
                <%= Html.TextBox("Title", Model.Title) %>
                <%= Html.ValidationMessage("Title", "*") %>
            </p>
            <p>
                <%= Html.CheckBox("Hidden", Model.Hidden)%>
                <label for="Hidden" class="inline required">Is the photo hidden for visitors?</label>
                <%= Html.ValidationMessage("Hidden", "*")%>
            </p>
            <p>
                <label for="UrlLarge" class="optional">URL for the large version of the photo:</label>
                <%= Html.TextBox("UrlLarge", Model.UrlLarge) %>
                <%= Html.ValidationMessage("UrlLarge", "*") %>
            </p>
            <p>
                <label for="UrlNormal" class="required">URL for the normal version of the photo:</label>
                <%= Html.TextBox("UrlNormal", Model.UrlNormal) %>
                <%= Html.ValidationMessage("UrlNormal", "*") %>
            </p>
            <p>
                <label for="UrlSmall" class="optional">URL for the small version of the photo:</label>
                <%= Html.TextBox("UrlSmall", Model.UrlSmall) %>
                <%= Html.ValidationMessage("UrlSmall", "*") %>
            </p>
            <p>
                <label for="UrlThumbnail" class="optional">URL for the thumbnail version of the photo:</label>
                <%= Html.TextBox("UrlThumbnail", Model.UrlThumbnail) %>
                <%= Html.ValidationMessage("UrlThumbnail", "*") %>
            </p>
            <p>
                <label for="UrlThumbnailSquare" class="optional">URL for the square thumbnail version of the photo:</label>
                <%= Html.TextBox("UrlThumbnailSquare", Model.UrlThumbnailSquare)%>
                <%= Html.ValidationMessage("UrlThumbnailSquare", "*")%>
            </p>
            <p>
                <label for="TagList" class="optional">Tags (comma separated):</label>
                <% Html.RenderPartial(PartialViewName.InputTags); %>
            </p>
            <p>
                <label for="DateTaken" class="optional">Date Taken:</label>
                <%= Html.TextBox("DateTaken", Model.DateTaken.ToEditString()) %>
                <%= Html.ValidationMessage("DateTaken", "*") %>
            </p>
            <p>
                <label for="DatePublished" class="required">Date Published:</label>
                <%= Html.TextBox("DatePublished", Model.DatePublished.ToEditString()) %>
                <%= Html.ValidationMessage("DatePublished", "*") %>
            </p>
            <p>
                <label for="ExternalId" class="optional">External Id:</label>
                <%= Html.TextBox("ExternalId", Model.ExternalId) %>
                <span title="This is the unique identifier of the photo on an external website or coming from an external photo provider" class="help">?</span>
                <%= Html.ValidationMessage("ExternalId", "*") %>
            </p>
            <p>
                <label for="ExternalUrl" class="optional">External URL:</label>
                <%= Html.TextBox("ExternalUrl", Model.ExternalUrl)%>
                <span title="This is the URL of the photo on an external website" class="help">?</span>
                <%= Html.ValidationMessage("ExternalUrl", "*")%>
            </p>
            <p>
                <label for="Text" class="optional">Text:</label>
                <%= Html.ValidationMessage("Text", "*") %>
            </p>
            <% Html.RenderPartialHtmlEditor("text", Model.Text); %>
            <p>
                <input type="submit" value="Save" />
            </p>
        </fieldset>
        <script type="text/javascript" src="<%= Url.Content("~/Scripts/tiny_mce/tiny_mce.js") %>"></script>
        <script type="text/javascript" src="<%= Url.Content("~/Scripts/tiny_mce_init.js") %>"></script>