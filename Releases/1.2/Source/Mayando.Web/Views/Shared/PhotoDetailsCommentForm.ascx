<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Mayando.Web.ViewModels.PhotoDetailsViewModel>" %>
                <div id="comment-form">
                    <h3><%= Html.Encode(Resources.PhotoCommentsAddComment) %></h3>
                    <%= Html.ValidationSummary(Html.Encode(Resources.ValidationSummaryGenericTitle))%>
                    <%= Html.Hidden("photoId", Model.Photo.Id)%>
                    <table>
                        <tbody>
                            <tr>
                                <td><label for="AuthorName" class="required"><%= Html.Encode(Resources.PhotoCommentsAuthorName) %></label></td>
                                <td><%= Html.TextBox("AuthorName") %><%= Html.ValidationMessage("AuthorName", "*") %></td>
                            </tr>
                            <tr>
                                <td><label for="AuthorEmail" class="optional"><%= Html.Encode(Resources.PhotoCommentsAuthorEmail) %></label></td>
                                <td>
                                    <div><%= Html.TextBox("AuthorEmail")%></div>
                                    <div class="helptext"><%= Resources.PhotoCommentsAuthorEmailHelp %></div>
                                </td>
                            </tr>
                            <tr>
                                <td><label for="AuthorUrl" class="optional"><%= Html.Encode(Resources.PhotoCommentsAuthorUrl)%></label></td>
                                <td><%= Html.TextBox("AuthorUrl")%></td>
                            </tr>
                            <tr>
                                <td style="vertical-align: top"><label for="Text" class="required"><%= Html.Encode(Resources.PhotoCommentsAuthorComment)%></label></td>
                                <td><%= Html.TextArea("Text", string.Empty, 8, 80, null)%><%= Html.ValidationMessage("Text", "*") %></td>
                            </tr>
                            <tr>
                                <td colspan="1"><input id="submitComment" type="submit" value="<%= Html.AttributeEncode(Resources.PhotoCommentsSubmit) %>" /></td>
                                <td><%= Html.CheckBox("RememberMe") %> <label class="inline optional" for="RememberMe"><%= Html.Encode(Resources.PhotoCommentsAuthorRememberMe) %></label></td>
                            </tr>
                        </tbody>
                    </table>
                </div>