<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<ICollection<Mayando.Web.Models.Menu>>" MasterPageFile="~/Views/Shared/Site.Master" %>

<asp:Content ID="title" ContentPlaceHolderID="TitleContent" runat="server">
    Menus
</asp:Content>

<asp:Content ID="content" ContentPlaceHolderID="MainContent" runat="server">
<div id="menuindexpage">
    <h2>Menus</h2>
    <% if(Model.Count > 0) { %>
    <% using(Html.BeginMenusMoveForm("moveForm")) { %>
        <%= Html.Hidden("id")%>
        <%= Html.Hidden("direction")%>
    <% } %>
    <table>
        <thead>
            <tr>
                <th></th>
                <th>Title</th>
                <th>Url</th>
                <th>Tool Tip</th>
                <th>New Window</th>
            </tr>
        </thead>
        <tbody>
            <% foreach (var item in Model) { %>
            <tr>
                <td>
                    <%= Html.ActionLinkMenuEdit("Edit", item.Id) %> |
                    <%= Html.ActionLinkMenuDelete("Delete", item.Id)%> |
                    <a href="#" onclick="onMoveFormDoPost('<%= item.Id %>','<%= Direction.Top.ToActionString() %>')">Top</a> |
                    <a href="#" onclick="onMoveFormDoPost('<%= item.Id %>','<%= Direction.Up.ToActionString() %>')">Up</a> |
                    <a href="#" onclick="onMoveFormDoPost('<%= item.Id %>','<%= Direction.Down.ToActionString() %>')">Down</a> |
                    <a href="#" onclick="onMoveFormDoPost('<%= item.Id %>','<%= Direction.Bottom.ToActionString() %>')">Bottom</a>
                </td>
                <td><%= Html.Encode(item.Title) %></td>
                <td><%= Html.Encode(item.Url) %></td>
                <td><%= Html.Encode(item.ToolTip) %></td>
                <td><%= Html.CheckBox("OpenInNewWindow-" + item.Id, item.OpenInNewWindow, new { disabled = "disabled" })%></td>
            </tr>
            <% } %>
        </tbody>
    </table>
    <% } %>
    <p><%= Html.ActionLinkMenuCreate("Create Menu") %></p>
</div>
</asp:Content>