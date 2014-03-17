<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Mayando.Web.Infrastructure.NavigationContext>" %>
    <div class="navigationcontainer">
        <table>
            <tr>
                <td class="navigation-previous"><%= Html.ActionLinkPhotoPrevious(Model) %></td>
                <td class="navigation-context">
                    <span class="navigation-overview">
                        <%= Html.Link(Model.OverviewUrl, Model.Description)%>
                    </span>
                    <% Html.RenderPartial(PartialViewName.NavigationContextSlideshow, Model); %>
                    <% if (Model.ShowFilmstrip) { %>
                    <% Html.RenderPartial(PartialViewName.NavigationContextFilmstrip, Model); %>
                    <% } %>
                </td>
                <td class="navigation-next"><%= Html.ActionLinkPhotoNext(Model)%></td>
            </tr>
        </table>
    </div>