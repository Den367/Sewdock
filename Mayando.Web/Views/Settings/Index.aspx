<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<Myembro.ViewModels.SettingsViewModel>" MasterPageFile="~/Views/Shared/Site.Master" %>

<asp:Content ID="title" ContentPlaceHolderID="TitleContent" runat="server">
    Settings
</asp:Content>

<asp:Content ID="content" ContentPlaceHolderID="MainContent" runat="server">
<div id="settingindexpage">
    <h2>Application Settings</h2>
    <% using (Html.BeginSettingsEditForm())
       {
            Html.RenderPartial(PartialViewName.ListSettings, Model);
       } %>
</div>
</asp:Content>