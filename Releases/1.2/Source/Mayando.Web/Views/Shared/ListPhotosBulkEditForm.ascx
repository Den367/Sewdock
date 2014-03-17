<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<% if (Page.User.CanSeeAdministratorContent()) { %>
	<fieldset>
	    <legend>Actions</legend>
	    <%= Html.Hidden("operation")%>
	    <%= Html.Hidden("returnUrl", this.Context.Request.RawUrl)%>
	    <p>
	        <a href="#" onclick="return onBulkEditFormSetSelected(true);">Select All</a>
	        <a href="#" onclick="return onBulkEditFormSetSelected(false);">Select None</a>
	    </p>
	    <table>
	        <tr>
	            <td colspan="2">
                    <input type="button" onclick="onBulkEditFormDoPost('Delete')" value="Delete Selected" />
                    <input type="button" onclick="onBulkEditFormDoPost('Hide')" value="Hide Selected" />
                    <input type="button" onclick="onBulkEditFormDoPost('Unhide')" value="Unhide Selected" />
	            </td>
	        </tr>
	        <tr>
	            <td>
                    <input type="button" onclick="onBulkEditFormDoPost('AddTags')" value="Add Tags" />
                    <input type="button" onclick="onBulkEditFormDoPost('RemoveTags')" value="Remove Tags" />
	            </td>
	            <td>
                    <% Html.RenderPartial(PartialViewName.InputTags); %>
	            </td>
	        </tr>
	    </table>
	</fieldset>
<% } %>