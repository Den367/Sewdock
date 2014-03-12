<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Mayando.Web.Models.Comment>" %>
        <%= Html.Hidden("returnUrl")%>
        <%= Html.ValidationSummary("The item could not be saved. Please correct any errors and try again.")%>
        <fieldset>
            <legend>Comment</legend>
            <p>
                <label for="AuthorName" class="required">Author's Name:</label>
                <%= Html.TextBox("AuthorName", Model.AuthorName)%>
                <%= Html.ValidationMessage("AuthorName", "*")%>
            </p>
            <p>
                <label for="AuthorEmail" class="optional">Author's Email:</label>
                <%= Html.TextBox("AuthorEmail", Model.AuthorEmail)%>
                <%= Html.ValidationMessage("AuthorEmail", "*")%>
            </p>
            <p>
                <label for="AuthorUrl" class="optional">Author's URL:</label>
                <%= Html.TextBox("AuthorUrl", Model.AuthorUrl)%>
                <%= Html.ValidationMessage("AuthorUrl", "*")%>
            </p>
            <p>
                <label for="DatePublished" class="required">Date Published:</label>
                <%= Html.TextBox("DatePublished", Model.DatePublished.ToEditString())%>
                <%= Html.ValidationMessage("DatePublished", "*")%>
            </p>
            <p>
                <%= Html.CheckBox("AuthorIsOwner", Model.AuthorIsOwner)%>
                <label for="AuthorIsOwner" class="inline required">Is the author the owner of this website?</label>
                <%= Html.ValidationMessage("AuthorIsOwner", "*")%>
            </p>
            <p>
                <label for="ExternalId" class="optional">External Id:</label>
                <%= Html.TextBox("ExternalId", Model.ExternalId)%>
                <span title="This is the unique identifier of the comment on an external website or coming from an external photo provider" class="help">?</span>
                <%= Html.ValidationMessage("ExternalId", "*")%>
            </p>
            <p>
                <label for="Text" class="required">Text:</label>
                <%= Html.TextArea("Text", Model.Text, 8, 80, null)%>
                <%= Html.ValidationMessage("Text", "*")%>
            </p>
            <p>
                <input type="submit" value="Save" />
            </p>
        </fieldset>