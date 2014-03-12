<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<JelleDruyts.Web.Mvc.Discovery.ActionInfo>>" %>
    <table>
        <thead>
            <tr>
                <th>URL</th>
                <th>Parameters <span title="Hover over a parameter for more information" class="help">?</span></th>
                <th>Description</th>
            </tr>
        </thead>
        <tbody>
        <% foreach(var action in Model) { %>
            <tr>
                <td><%= Html.ActionLinkAction(Url.DiscoveryAction(action), action) %></td>
                <td><%= Html.LinkList(action.Parameters.ToLinkList(), ", ") %></td>
                <td><%= Html.Encode(action.Description) %></td>
            </tr>
        <% } %>
        </tbody>
    </table>