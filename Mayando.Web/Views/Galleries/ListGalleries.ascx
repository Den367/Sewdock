<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ICollection<Myembro.ViewModels.GalleryInfoViewModel>>" %>
    <% if(Model.Count > 0) { %>
    <% if (Page.User.CanSeeAdministratorContent()) { %>
    <% using(Html.BeginGalleriesMoveForm("moveForm")) { %>
        <%= Html.Hidden("id")%>
        <%= Html.Hidden("direction")%>
    <% } %>
    <% } %>
    <h3><%= Html.NumberOfGalleries(Model.Count) %></h3>
    <table class="list">
        <tbody>
        <% foreach(var galleryModel in Model.OrderBy(g => g.Gallery.Sequence)) { %>
            <tr>
                <td>
                    <div class="photo-thumbnail">
                        <a href="<%= Url.GalleryDetails(galleryModel.Gallery) %>" title="<%= Html.AttributeEncode(galleryModel.Gallery.Title) %>">
                            <img src="<%= galleryModel.GetCoverPhotoUrl(Url.ThemedContent("~/Content/gallery.png")) %>" alt="<%= Html.AttributeEncode(galleryModel.Gallery.Title) %>" />
                        </a>
                    </div>
                </td>
                <td>
                    <div class="listitem" id="gallery-<%= galleryModel.Gallery.Id %>">
                        <div class="listitem-title">
                            <a href="<%= Url.GalleryDetails(galleryModel.Gallery) %>"><%= Html.Encode(galleryModel.Gallery.Title) %></a>
                        <% if (Page.User.CanSeeAdministratorContent()) { %>
                         <%--  <%= Html.ActionLinkGalleryEdit("Edit", galleryModel.Gallery.Id, "admin") %>
                           <%= Html.ActionLinkGalleryDelete("Delete", galleryModel.Gallery.Id, "admin") %>--%>
                           <a href="#" class="admin" onclick="onMoveFormDoPost('<%= galleryModel.Gallery.Id %>','<%= Direction.Top.ToActionString() %>')">Top</a>
                           <a href="#" class="admin" onclick="onMoveFormDoPost('<%= galleryModel.Gallery.Id %>','<%= Direction.Up.ToActionString() %>')">Up</a>
                           <a href="#" class="admin" onclick="onMoveFormDoPost('<%= galleryModel.Gallery.Id %>','<%= Direction.Down.ToActionString() %>')">Down</a>
                           <a href="#" class="admin" onclick="onMoveFormDoPost('<%= galleryModel.Gallery.Id %>','<%= Direction.Bottom.ToActionString() %>')">Bottom</a>
                        <% } %>
                        </div>
                    <%--    <div class="listitem-content"><%= galleryModel.Gallery.Description %></div>--%>
                        <div class="listitem-footer">
                            <% if(galleryModel.PhotoCount > 0) { %>
                            <div><%= Html.NumberOfPhotos(galleryModel.PhotoCount) %></div>
                            <% } %>
                            <% if(galleryModel.ChildGalleryCount > 0) { %>
                            <div><%= Html.NumberOfGalleries(galleryModel.ChildGalleryCount) %></div>
                            <% } %>
                        </div>
                        <div class="listitem-footer"></div>
                    </div>
                </td>
            </tr>
        <% } %>
        </tbody>
    </table>
    <% } %>