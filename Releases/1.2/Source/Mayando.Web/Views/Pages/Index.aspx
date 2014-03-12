<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<ICollection<Mayando.Web.Models.Page>>" MasterPageFile="~/Views/Shared/Site.Master" %>

<asp:Content ID="title" ContentPlaceHolderID="TitleContent" runat="server">
    Pages
</asp:Content>

<asp:Content ID="content" ContentPlaceHolderID="MainContent" runat="server">
<div id="pageindexpage">
    <h2>Pages</h2>
    <% if(Model.Count > 0) { %>
    <table>
        <thead>
            <tr>
                <th></th>
                <th>Title</th>
            </tr>
        </thead>
        <tbody>
            <% foreach (var item in Model) { %>
            <tr>
                <td>
                    <%= Html.ActionLinkPageDetails("View", item.Id) %> |
                    <%= Html.ActionLinkPageEdit("Edit", item.Id) %> |
                    <%= Html.ActionLinkPageDelete("Delete", item.Id) %>
                </td>
                <td><%= Html.Encode(item.Title) %></td>
            </tr>
            <% } %>
        </tbody>
    </table>
    <% } %>
    <p><%= Html.ActionLinkPageCreate("Create Page")%></p>
</div>
</asp:Content>