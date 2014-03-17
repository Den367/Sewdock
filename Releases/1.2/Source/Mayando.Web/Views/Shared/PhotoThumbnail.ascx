<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Mayando.Web.ViewModels.PhotoThumbnailViewModel>" %>
                    <span class="photo-thumbnail">
                        <a href="<%= Url.PhotoDetails(Model.Photo, Model.NavigationContextType, Model.NavigationContextCriteria, Model.NavigationContextSlideshowDelay) %>" title="<%= Html.AttributeEncode(Model.Photo.DisplayTitle) %>">
                            <img alt="<%= Html.AttributeEncode(Model.Photo.DisplayTitle) %>" src="<%= Model.Photo.UrlSmallestAvailable %>" />
                        </a>
                    </span>