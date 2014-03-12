<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Mayando.Web.Models.Page>" %>
        <%= Html.ValidationSummary("The item could not be saved. Please correct any errors and try again.") %>
        <fieldset>
            <legend>Page</legend>
            <p>
                <label for="Title" class="required">Title:</label>
                <%= Html.TextBox("Title", Model.Title) %>
                <%= Html.ValidationMessage("Title", "*") %>
                <% if(!string.IsNullOrEmpty(Model.Slug)) { %>
                <label for="Slug" class="inline">Slug:</label>
                <%= Html.Encode(Model.Slug) %>
                <span title="The URL-friendly version of the title, which can be used to browse to the page directly." class="help">?</span>
                <% } %>
            </p>
            <p>
                <%= Html.CheckBox("ShowContactForm", Model.ShowContactForm)%>
                <label for="ShowContactForm" class="inline optional">Show Contact Form</label>
            </p>
            <p>
                <label for="PhotoId" class="optional">Photo:</label>
                <%= Html.DropDownList("PhotoId", ViewData.GetPhotos()) %>
                <%= Html.CheckBox("HidePhotoText", Model.HidePhotoText)%>
                <label for="HidePhotoText" class="inline optional">Hide Photo Text</label>
                <%= Html.CheckBox("HidePhotoComments", Model.HidePhotoComments)%>
                <label for="HidePhotoComments" class="inline optional">Hide Photo Comments</label>
            </p>
            <p>
                <label for="Text" class="required">Text:</label>
                <%= Html.ValidationMessage("Text", "*") %>
            </p>
            <% Html.RenderPartialHtmlEditor("text", Model.Text); %>
            <p>
                <input type="submit" value="Save" />
            </p>
        </fieldset>
        <script type="text/javascript" src="<%= Url.Content("~/Scripts/tiny_mce/tiny_mce.js") %>"></script>
        <script type="text/javascript" src="<%= Url.Content("~/Scripts/tiny_mce_init.js") %>"></script>