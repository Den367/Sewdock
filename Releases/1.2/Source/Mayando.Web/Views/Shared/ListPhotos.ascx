<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Mayando.Web.ViewModels.PhotosViewModel>" %>
    <% if(Model.Photos.Count > 0) { %>
    <% if (Page.User.CanSeeAdministratorContent()) { Html.BeginPhotosBulkEditForm("bulkEditForm"); } %>
	<%= Html.Pager(Model.Photos.PageSize, Model.Photos.PageNumber, Model.Photos.TotalItemCount)%>
    <h3>
        <%= Html.NumberOfPhotos(Model.Photos.TotalItemCount) %>
        <% Html.RenderPartial(PartialViewName.NavigationContextSlideshow, Model.NavigationContext); %>
    </h3>
    <% Html.RenderPartial(PartialViewName.ListPhotoThumbnails, Model); %>
	<%= Html.Pager(Model.Photos.PageSize, Model.Photos.PageNumber, Model.Photos.TotalItemCount)%>
    <% Html.RenderPartial(PartialViewName.ListPhotosBulkEditForm); %>
    <% if (Page.User.CanSeeAdministratorContent()) { Html.EndForm(); } %>
    <% } %>