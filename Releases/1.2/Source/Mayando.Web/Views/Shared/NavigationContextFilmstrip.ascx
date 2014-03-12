<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Mayando.Web.Infrastructure.NavigationContext>" %>
                    <div class="navigation-filmstrip">
                        <table>
                            <tr>
                                <td class="navigation-filmstrip-previous">
                                <% foreach (var photo in Model.PreviousPhotos) { %>
                                    <% Html.RenderPartialPhotoThumbnail(photo, Model); %>
                                <% } %>
                                </td>
                                <td class="navigation-filmstrip-current">
                                    <% Html.RenderPartialPhotoThumbnail(Model.Current, Model); %>
                                </td>
                                <td class="navigation-filmstrip-next">
                                <% foreach (var photo in Model.NextPhotos) { %>
                                    <% Html.RenderPartialPhotoThumbnail(photo, Model); %>
                                <% } %>
                                </td>
                            </tr>
                        </table>
                    </div>