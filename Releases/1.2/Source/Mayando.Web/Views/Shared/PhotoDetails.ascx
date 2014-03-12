<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Mayando.Web.ViewModels.PhotoDetailsViewModel>" %>
        <div id="photocontainer">
            <% Html.RenderPartial(PartialViewName.PhotoDetailsImage, Model); %>
            <% if (!Model.HidePhotoText) { %>
            <% Html.RenderPartial(PartialViewName.PhotoDetailsText, Model); %>
            <% } %>
            <% if (!Model.HidePhotoComments) { %>
            <div id="commentscontainer">
                <% Html.RenderPartial(PartialViewName.PhotoDetailsCommentList, Model); %>
                <% Html.RenderPartial(PartialViewName.PhotoDetailsCommentForm, Model); %>
            </div>
            <% } %>
        </div>