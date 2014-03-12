<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<JelleDruyts.Web.Mvc.Paging.IPagedList<Mayando.Web.Models.Comment>>" MasterPageFile="~/Views/Shared/Site.Master" %>

<asp:Content ID="title" ContentPlaceHolderID="TitleContent" runat="server">
	<%= Html.Encode(Resources.CommentsTitle) %>
</asp:Content>

<asp:Content ID="contents" ContentPlaceHolderID="MainContent" runat="server">
<div id="commentindexpage">
    <h2><%= Html.Encode(Resources.CommentsTitle) %></h2>
	<%= Html.Pager(Model.PageSize, Model.PageNumber, Model.TotalItemCount)%>
    <table class="list">
        <tbody>
            <% foreach (var comment in Model) { %>
            <tr>
                <td>
                    <% Html.RenderPartialPhotoThumbnail(comment.Photo); %>
                </td>
                <td>
                    <div class="listitem">
                        <% Html.RenderPartial(PartialViewName.CommentDetails, comment); %>
                    </div>
                </td>
            </tr>       
            <% } %>
        </tbody>
    </table>
	<%= Html.Pager(Model.PageSize, Model.PageNumber, Model.TotalItemCount)%>
</div>
</asp:Content>