<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Mayando.Web.Models.Comment>" %>
                            <div class="comment<%= (Model.AuthorIsOwner ? " comment-owner" : string.Empty) %>" id="comment-<%= Model.Id %>">
                                <div class="comment-title">
                                    <%= Html.Encode(Resources.PhotoCommentsBy) %>
                                    <span class="comment-author"><%= Html.Link(Model.AuthorUrl, Model.AuthorName, true)%></span>
                                    <span class="comment-date"><%= Html.DateTimeText(Model.DatePublished, DateTimeDisplayType.CommentPublished)%></span>
                                    <% if (Page.User.CanSeeAdministratorContent()) { %>
                                    <%= Html.ActionLinkCommentEdit("Edit", Model.Id, this.Context.Request.RawUrl, "admin")%>
                                    <%= Html.ActionLinkCommentDelete("Delete", Model.Id, this.Context.Request.RawUrl, "admin")%>
                                    <% } %>
                                </div>
                                <div class="comment-content">
                                    <div class="comment-gravatar">
                                        <img src="<%= Url.Gravatar(Model.AuthorEmail, 60, Url.ThemedContent("~/content/anonymous.png")) %>" height="60" width="60" alt="Gravatar" />
                                    </div>
                                    <div class="comment-text"><%= Html.EncodeWithLineBreaks(Model.Text)%></div>
                                </div>
                            </div>