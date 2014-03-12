<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Mayando.Web.ViewModels.EmbroDetailsViewModel>" %>
        <% if (!Model.HidePhotoComments) { %>
        <div id="controlcontainer">
            <a href="#" onclick="toggleVisible('comment-list',true); setVisible('comment-form',false); return false;"><% Html.NumberOfComments(Model.Comments.Count()); %></a> |
            <a href="#" onclick="setVisible('comment-list',false); toggleVisible('comment-form',true); return false;"><%= Resources.PhotoCommentsAddComment %></a>
        </div>
        <% } %>
        <div id="photocontainer">
            <table id="photocontainertable">
                <tbody>
                    <tr>
                        <td>
                            <% Html.RenderPartial(PartialViewName.EmbroDetailsImage, Model); %>
                        </td>
                        <td>
                            <% if (!Model.HidePhotoComments) { %>
                            <div id="commentscontainer">
                                <% Html.RenderPartial(PartialViewName.EmbroDetailsCommentList, Model); %>
                                <% Html.RenderPartial(PartialViewName.EmbroDetailsCommentForm, Model); %>
                            </div>
                            <% } %>
                        </td>
                    </tr>
                </tbody>
            </table>
            <% if (!Model.HidePhotoText) { %>
                <% Html.RenderPartial(PartialViewName.EmbroDetailsText, Model); %>
            <% } %>
        </div>