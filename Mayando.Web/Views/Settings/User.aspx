<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<Myembro.ViewModels.SettingsViewModel>" MasterPageFile="~/Views/Shared/Site.Master" %>

<asp:Content ID="title" ContentPlaceHolderID="TitleContent" runat="server">
    User-Defined Settings
</asp:Content>

<asp:Content ID="content" ContentPlaceHolderID="MainContent" runat="server">
<div id="settinguserpage">
    <h2>User-Defined Settings</h2>
    <% if (Model.Settings.Count > 0) { %>    
    <h3>Edit Settings</h3>
    <% using (Html.BeginSettingsEditUserSettingsForm())
       {
            Html.RenderPartial(PartialViewName.ListSettings, Model);
       } %>
    <% } %>
    <% using (Html.BeginSettingsAddUserSettingForm()) { %>
    <h3>Add New Setting</h3>
    <fieldset>
        <legend>New Setting Definition</legend>
        <p>
            <label for="Name" class="required">Name:</label>
            <%= Html.TextBox("Name")%>
            <%= Html.ValidationMessage("Name", "*")%>
        </p>
        <p>
            <%= Html.CheckBox("IsHtml")%>
            <label for="IsHtml" class="inline required">Use HTML editor?</label>
            <%= Html.ValidationMessage("IsHtml", "*")%>
        </p>
        <p>
            <input type="submit" value="Add Setting" />
        </p>
    </fieldset>
    <% } %>
    <h3>Usage</h3>
    <p>User-defined settings can be used in custom or modified visual themes to show additional information on pages that can be easily modified.</p>
    <p>To display the value of such a setting in a page, use the following code (in an .aspx, .ascx or .master file):</p>
    <pre>&lt;%= ViewData.GetMasterViewModel().SiteData.GetUserSetting("MySetting") %&gt;</pre>
    <p>Note that the name of the user-defined setting (<code>"MySetting"</code> in the example) is case sensitive, so it must be exactly the same everywhere.</p>
    <p>In case the setting has not been defined (yet), you can also specify a default value which will then be used:</p>
    <pre>&lt;%= ViewData.GetMasterViewModel().SiteData.GetUserSetting("MySetting", "This will be shown when the setting is not defined.") %&gt;</pre>
    <p>For master pages (.master files), you can also use the following short-hand notation:</p>
    <pre>&lt;%= MasterModel.SiteData.GetUserSetting("MySetting", "This will be shown when the setting is not defined.") %&gt;</pre>
</div>
</asp:Content>