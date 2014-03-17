<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Mayando.Web.ViewModels.PhotoDetailsViewModel>" %>
                <div id="comment-list">
                    <h3><%= Html.Encode(Resources.PhotoComments) %></h3>
                    <% if (Model.Photo.Comments.Count == 0) { %>
                    <div><%= Html.Encode(Resources.PhotoCommentsEmpty) %></div>
                    <% } else { %>
                    <ul>
                        <% foreach (var comment in Model.Photo.Comments.OrderBy(c => c.DatePublished)) { %>
                        <li>
                            <% Html.RenderPartial(PartialViewName.CommentDetails, comment); %>
                        </li>
                        <% } %>
                    </ul>
                    <% } %>
                </div>