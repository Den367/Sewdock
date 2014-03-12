<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Mayando.Web.ViewModels.SettingsViewModel>" %>
<% if(Model.Settings.Count > 0) { %>
<table>
    <thead>
        <tr>
            <th>Setting</th>
            <th>Value</th>
            <th>Help</th>
            <% if (Model.AllowDelete) { %>
            <th>Actions</th>
            <% } %>
        </tr>
    </thead>
    <tfoot>
        <tr>
            <td colspan="4"><input type="submit" value="Update Settings" /></td>
        </tr>
    </tfoot>
    <tbody>
        <% foreach (var item in Model.UserVisibleSettings) { %>
        <tr>
            <td>
                <%= Html.Hidden(string.Format("settings[{0}].Key", Model.UserVisibleSettings.IndexOf(item)), item.Setting.Name, new { id = "settings-key-" + Model.UserVisibleSettings.IndexOf(item) })%>
                <%= Html.Encode(item.Setting.DisplayTitle)%>
            </td>
            <td>
                <% if (item.AllowedValues != null) { %>
                <%= Html.DropDownList(string.Format("settings[{0}].Value", Model.UserVisibleSettings.IndexOf(item)), item.AllowedValues, new { id = "settings-value-" + Model.UserVisibleSettings.IndexOf(item) })%>
                <% } else if(item.Setting.SettingType == SettingType.Boolean) { %>
                <%= Html.CheckBox(string.Format("settings[{0}].Value", Model.UserVisibleSettings.IndexOf(item)), string.Equals(bool.TrueString, item.Setting.Value, StringComparison.OrdinalIgnoreCase), new { id = "settings-value-" + Model.UserVisibleSettings.IndexOf(item) })%>
                <% } else if(item.Setting.SettingType == SettingType.Html) { %>
                <% Html.RenderPartialHtmlEditor(string.Format("settings[{0}].Value", Model.UserVisibleSettings.IndexOf(item)), item.Setting.Value, 3);  %>
                <% } else { %>
                <%= Html.TextBox(string.Format("settings[{0}].Value", Model.UserVisibleSettings.IndexOf(item)), item.Setting.Value, new { id = "settings-value-" + Model.UserVisibleSettings.IndexOf(item) })%>
                <% } %>
            </td>
            <td>
                <% if (!string.IsNullOrEmpty(item.Setting.Description)) { %>
                <span title="<%= Html.AttributeEncode(item.Setting.Description) %>" class="help">?</span>
                <% } %>
            </td>
            <% if (Model.AllowDelete) { %>
            <td>
                <% if (!string.IsNullOrEmpty(item.DeleteUrl)) { %>
                <%= Html.Link(item.DeleteUrl, "Delete") %>
                <% } %>
            </td>
            <% } %>
        </tr>
        <% } %>
    </tbody>
</table>
<script type="text/javascript" src="<%= Url.Content("~/Scripts/tiny_mce/tiny_mce.js") %>"></script>
<script type="text/javascript" src="<%= Url.Content("~/Scripts/tiny_mce_init.js") %>"></script>
<% } %>