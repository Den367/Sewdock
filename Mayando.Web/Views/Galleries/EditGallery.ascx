<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Mayando.Web.Models.Gallery>" %>
        <%= Html.ValidationSummary("The item could not be saved. Please correct any errors and try again.") %>
        <%= Html.Hidden("Sequence") %>    
        <fieldset>
            <legend>Gallery</legend>
            <p>
                <label for="Title" class="required">Title:</label>
                <%= Html.TextBox("Title", Model.Title) %>
                <%= Html.ValidationMessage("Title", "*") %>
                <% if(!string.IsNullOrEmpty(Model.Slug)) { %>
                <label for="Slug" class="inline">Slug:</label>
                <%= Html.Encode(Model.Slug) %>
                <span title="The URL-friendly version of the title, which can be used to browse to the gallery directly." class="help">?</span>
                <% } %>
            </p>
            <p>
                <label for="Description" class="optional">Description:</label>
                <% Html.RenderPartialHtmlEditor("Description", Model.Description); %>
                <%= Html.ValidationMessage("Description", "*")%>
            </p>
            <p>
                <label for="CoverPhotoId" class="optional">Cover EmbroideryItem:</label>
                <%= Html.DropDownList("CoverPhotoId", ViewData.GetPhotos()) %>
            </p>
            <p>
                <label for="ParentGalleryId" class="optional">Parent Gallery:</label>
                <%= Html.DropDownList("ParentGalleryId", ViewData.GetGalleries())%>
            </p>
            <p>
                <label for="TagList" class="optional">Include photos with all the following tags (comma separated):</label>
                <% Html.RenderPartial(PartialViewName.InputTags); %>
            </p>
            <p>
                <input type="submit" value="Save" />
            </p>
        </fieldset>
        <script type="text/javascript" src="<%= Url.Content("~/Scripts/tiny_mce/tiny_mce.js") %>"></script>
        <script type="text/javascript" src="<%= Url.Content("~/Scripts/tiny_mce_init.js") %>"></script>