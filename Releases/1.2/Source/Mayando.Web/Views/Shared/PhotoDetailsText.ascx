<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Mayando.Web.ViewModels.PhotoDetailsViewModel>" %>
            <div id="photo-text">
                <%= Model.Photo.Text %>
            </div>
            <div id="photo-details">
                <div id="photo-dates">
                    <% if (Model.Photo.DateTaken.HasValue) { %>
                    <span><%= Html.DateTimeText(Model.Photo.DateTaken, DateTimeDisplayType.PhotoTaken)%></span> |
                    <% } %>
                    <span><%= Html.DateTimeText(Model.Photo.DatePublished, DateTimeDisplayType.PhotoPublished)%></span>
                </div>
                <% if (Model.CanChangeSize) { %>
                <div id="photo-sizes">
                    <%= Resources.PhotoSizes %> &raquo;
                    <% if (Model.CanChangeSizeToLarge) { %>
                    <span class="photo-size photo-size-l"><%= Html.ActionLinkPhotoSize(PhotoSize.Large) %></span>
                    <% } %>
                    <% if (Model.CanChangeSizeToNormal) { %>
                    <span class="photo-size photo-size-m"><%= Html.ActionLinkPhotoSize(PhotoSize.Normal) %></span>
                    <% } %>
                    <% if (Model.CanChangeSizeToSmall) { %>
                    <span class="photo-size photo-size-s"><%= Html.ActionLinkPhotoSize(PhotoSize.Small) %></span>
                    <% } %>
                    |
                </div>
                <% } %>
                <div id="photo-tags">
                    <%= Html.ActionLinkTagsIndex(Resources.PhotoTags) %> &raquo;
                    <% foreach(var tag in Model.Photo.Tags.OrderBy(t => t.Name)) { %>
                    <span class="photo-tag"><%= Html.ActionLinkTag(tag) %></span>
                    <% } %>
                </div>
            </div>