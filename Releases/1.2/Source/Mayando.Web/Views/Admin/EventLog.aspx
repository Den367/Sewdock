<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<JelleDruyts.Web.Mvc.Paging.IPagedList<Mayando.Web.Models.Log>>" MasterPageFile="~/Views/Shared/Site.Master" %>

<asp:Content ID="title" ContentPlaceHolderID="TitleContent" runat="server">
    Event Log
</asp:Content>

<asp:Content ID="content" ContentPlaceHolderID="MainContent" runat="server">
<div id="admineventlogpage">
    <h2>Event Log</h2>
    <fieldset>
        <legend>Filter</legend>
        <% using (Html.BeginForm()) { %>
            <p>
                <label for="SearchText" class="inline optional">Search:</label>
                <%= Html.TextBox("LogSearchText")%>
                <label for="MinLevel" class="inline optional">Minimum Log Level:</label>
                <%= Html.DropDownList("MinLevel", ViewData.GetLogLevels())%>
                <input name="submitButton" type="submit" value="Search" />
            </p>
        <% } %>
    </fieldset>
    <% if (Model.Count == 0) { %>
    <p>There are currently no logs available.</p>
    <% } else { %>
	<%= Html.Pager(Model.PageSize, Model.PageNumber, Model.TotalItemCount)%>
    <table>
        <thead>
            <tr>
                <th>Level</th>
                <th>Time</th>
                <th>Message</th>
            </tr>
        </thead>
        <tbody>
            <% foreach (var item in Model) { %>
            <tr class="log-<%= item.LogLevel.ToString().ToLowerInvariant() %>">
                <td>
                    <%= Html.Encode(item.LogLevel.ToString()) %>
                </td>
                <td>
                    <%= Html.Encode(item.Time.ToAdjustedDisplayString()) %>
                </td>
                <td>
                    <div class="log-message"><%= Html.Encode(item.Message) %></div>
                    <% if(!string.IsNullOrEmpty(item.Detail)) { %>
                    <pre class="log-detail"><%= Html.Encode(item.Detail) %></pre>
                    <% } %>
                </td>
            </tr>
            <% } %>
        </tbody>
    </table>
	<%= Html.Pager(Model.PageSize, Model.PageNumber, Model.TotalItemCount)%>
    <% using (Html.BeginAdminClearEventLogForm()) { %>
    <fieldset>
        <legend>Actions</legend>
        <input type="submit" value="Delete" /> event log entries older than <%= Html.TextBox("minAge", 30, new { @class = "short" })%> days and with a maximum log level of <%= Html.DropDownList("MaxLevel", ViewData.GetLogLevels())%>.
    </fieldset>
    <% } %>
    <% } %>
</div>
</asp:Content>