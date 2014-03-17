<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Mayando.Web.ViewModels.PhotosViewModel>" %>
    <table class="list">
        <tbody>
        <% foreach (var photo in Model.Photos) { %>
            <tr>
                <% if (Page.User.CanSeeAdministratorContent()) { %>
                <td>
                    <%= Html.Hidden(string.Format("photos[{0}].Key", Model.Photos.IndexOf(photo)), photo.Id, new { id = "photos-key-" + Model.Photos.IndexOf(photo) })%>
                    <%= Html.CheckBox(string.Format("photos[{0}].Value", Model.Photos.IndexOf(photo)), true, new { id = "photos-value-" + Model.Photos.IndexOf(photo) })%>
                </td>
                <% } %>
                <td>
                    <% Html.RenderPartialPhotoThumbnail(photo, Model.NavigationContext); %>
                </td>
                <td>
                    <div class="listitem" id="photo-<%= photo.Id %>">
                        <div class="listitem-title"><a href="<%= Url.PhotoDetails(photo, Model.NavigationContext.Type, Model.NavigationContext.Criteria) %>"><%= Html.Encode(photo.DisplayTitle) %></a></div>
                        <div class="listitem-content"><%= photo.Text %></div>
                        <div class="listitem-footer">
                            <% if (photo.Comments.Count > 0) { %>
                            <span><%= Html.NumberOfComments(photo.Comments.Count) %></span> |
                            <% } %>
                            <% if (photo.DateTaken.HasValue) { %>
                            <span><%= Html.DateTimeText(photo.DateTaken, DateTimeDisplayType.PhotoTaken)%></span> |
                            <% } %>
                            <span><%= Html.DateTimeText(photo.DatePublished, DateTimeDisplayType.PhotoPublished)%></span>
                        </div>
                    </div>
                </td>
            </tr>
        <% } %>
        </tbody>
    </table>