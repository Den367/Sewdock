<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Mayando.Web.ViewModels.PhotoDetailsViewModel>" %>
        <% if (!Model.HidePhotoComments) { %>
        <div id="controlcontainer">
            <a href="#" onclick="toggleVisible('comment-list',true); setVisible('comment-form',false); return false;"><%= Html.NumberOfComments(Model.Photo.Comments.Count) %></a> |
            <a href="#" onclick="setVisible('comment-list',false); toggleVisible('comment-form',true); return false;"><%= Resources.PhotoCommentsAddComment %></a>
        </div>
        <% } %>
        <div id="photocontainer">
            <table id="photocontainertable">
                <tbody>
                    <tr>
                        <td>
                            <% Html.RenderPartial(PartialViewName.PhotoDetailsImage, Model); %>
                        </td>
                        <td>
                            <% if (!Model.HidePhotoComments) { %>
                            <div id="commentscontainer">
                                <% Html.RenderPartial(PartialViewName.PhotoDetailsCommentList, Model); %>
                                <% Html.RenderPartial(PartialViewName.PhotoDetailsCommentForm, Model); %>
                            </div>
                            <% } %>
                        </td>
                    </tr>
                </tbody>
            </table>
            <% if (!Model.HidePhotoText) { %>
                <% Html.RenderPartial(PartialViewName.PhotoDetailsText, Model); %>
            <% } %>
        </div>