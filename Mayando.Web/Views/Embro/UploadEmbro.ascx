<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Mayando.Web.Models.EmbroideryItem>" %>



        <%= Html.ValidationSummary("The item could not be saved. Please correct any errors and try again.") %>
        <fieldset>
            <legend>EmbroideryItem</legend>
             <div id="dialog" title="Upload files">
        <% using (Html.BeginForm("Upload", "Embro", FormMethod.Post, new { enctype = "multipart/form-data" }))
           {%>


           <div class="attach"> 
	<input id="file_fake" type="text" readonly="readonly" value="<%=Resources.EmbroFileToBeUploadName %>" /> 
	<span class="file" title="Выбрать файл">
		<input type="file" id="fileUpload" name="fileUpload" class="file_select" onchange="document.getElementById('file_fake').value = this.value"/>
		<input type="button" class="file_select_btn" value="<%=Resources.SelectFileButtonTitle %>" title="Вместо этого элемента вы можете подставить любой другой" />
	</span>
</div>


<%--            <p><input type="file" id="fileUpload" name="fileUpload" class="file"/> </p> --%>
       
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
                <label for="TagList" class="optional">Tags (semicolon separated):</label>
                <% Html.RenderPartial(PartialViewName.InputTags); %>
            </p>
          <%--  <p>
                <label for="DateTaken" class="optional">Date Taken:</label>
                <%= Html.TextBox("DateTaken", Model.DateTaken.ToEditString()) %>
                <%= Html.ValidationMessage("DateTaken", "*") %>
            </p>--%>
            <p>
                <label for="DatePublished" class="required">Date Published:</label>
                <%= Html.TextBox("DatePublished", Model.Published.ToEditString()) %>
                <%= Html.ValidationMessage("DatePublished", "*") %>
            </p>
           
           
            <p>
                <label for="Article" class="optional">Article:</label>
                <%= Html.ValidationMessage("Text", "*") %>
            </p>
            <% Html.RenderPartialHtmlEditor("Article", Model.Article); %>
            <p>
                <input type="submit" value="Save" class="submit"/>
            </p>
             <% } %>
    </div>
        </fieldset>
        <script type="text/javascript" src="<%= Url.Content("~/Scripts/tiny_mce/tiny_mce.js") %>"></script>
        <script type="text/javascript" src="<%= Url.Content("~/Scripts/tiny_mce_init.js") %>"></script>