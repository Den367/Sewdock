<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Mayando.Web.ViewModels.ConfirmBulkEditViewModel>" %>

<asp:Content ID="title" ContentPlaceHolderID="TitleContent" runat="server">
	Confirm <%= Html.Encode(Model.Operation.ToString()) %>
</asp:Content>

<asp:Content ID="content" ContentPlaceHolderID="MainContent" runat="server">
<div id="photoconfirmpage">
    <h2>Confirm <%= Html.Encode(Model.OperationDescription) %></h2>
    <p><%= Html.Encode(Model.Description) %></p>
    <% using (Html.BeginPhotosBulkEditExecuteForm()) { %>
        <input name="submitButton" type="submit" value="Confirm" />
    <% } %>
</div>
</asp:Content>