<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<Mayando.Web.ViewModels.AboutViewModel>" MasterPageFile="~/Views/Shared/Site.Master" %>

<asp:Content ID="title" ContentPlaceHolderID="TitleContent" runat="server">
	About <%= Html.Encode(Model.SiteData.ApplicationName) %>
</asp:Content>

<asp:Content ID="content" ContentPlaceHolderID="MainContent" runat="server">
<div id="aboutpage">
    <h2>About <%= Html.Encode(Model.SiteData.ApplicationName)%></h2>
    <p>You are running <b><%= Html.Encode(Model.SiteData.ApplicationName)%> <%= Html.Encode(Model.SiteData.ApplicationDisplayVersion) %></b>.</p>
    <p>For the latest information, please visit the <%= Html.Link(Model.SiteData.ApplicationUrl, Model.SiteData.ApplicationName, true) %> homepage.</p>
    <p>Here are some other interesting facts:</p>
    <table>
        <tbody>
            <tr>
                <td>Number Of <%= Html.ActionLinkPhotosIndex("Photos") %></td>
                <td>
                    <%= Html.Encode(Model.Statistics.NumberOfPhotos) %>
                    <% if(Model.Statistics.NumberOfHiddenPhotos > 0) { %>
                    (including <%= Html.ActionLinkPhotosHidden(Model.Statistics.NumberOfHiddenPhotos + " hidden") %>)
                    <% } %>
                </td>
            </tr>
            <tr>
                <td>Number Of <%= Html.ActionLinkGalleriesIndex("Galleries") %></td>
                <td><%= Html.Encode(Model.Statistics.NumberOfGalleries) %></td>
            </tr>
            <tr>
                <td>Number Of <%= Html.ActionLinkPagesIndex("Pages") %></td>
                <td><%= Html.Encode(Model.Statistics.NumberOfPages) %></td>
            </tr>
            <tr>
                <td>Number Of <%= Html.ActionLinkCommentsIndex("Comments") %></td>
                <td><%= Html.Encode(Model.Statistics.NumberOfComments) %></td>
            </tr>
            <tr>
                <td>Number Of <%= Html.ActionLinkTagsIndex("Tags") %></td>
                <td><%= Html.Encode(Model.Statistics.NumberOfTags) %></td>
            </tr>
            <tr>
                <td>Version Number</td>
                <td><%= Html.Encode(Model.SiteData.ApplicationVersion)%></td>
            </tr>
        </tbody>
    </table>
    <p>Enjoy your photo blog!</p>
    <p><i>--The <%= Html.Encode(Model.SiteData.ApplicationName)%> Team.</i></p>
</div>
</asp:Content>