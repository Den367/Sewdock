<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Mayando.Web.ViewModels.PhotoDetailsViewModel>" %>
            <div id="photo-image">
                <% if(Model.ShouldLinkToExternalUrl) { %>
                <a href="<%= Model.Photo.ExternalUrl %>" target="_blank">
                <% } %>
                    <img id="photo-image-display" src="<%= Model.PreferredSizePhotoUrl %>" alt="<%= Html.AttributeEncode(Model.Photo.DisplayTitle) %>" />
                <% if(Model.ShouldLinkToExternalUrl) { %>
                </a>
                <% } %>
            </div>