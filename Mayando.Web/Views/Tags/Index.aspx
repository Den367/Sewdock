<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<Mayando.Web.ViewModels.TagsViewModel>" MasterPageFile="~/Views/Shared/Site.Master" %>

<asp:Content ID="title" ContentPlaceHolderID="TitleContent" runat="server">
	<%= Html.Encode(Resources.TagsTitle) %>
</asp:Content>

<asp:Content ID="content" ContentPlaceHolderID="MainContent" runat="server">
<div id="tagindexpage">
    <h2><%=
        Html.Encode(Resources.TagsTitle) %></h2>
    <div class="linklist"><%= Html.LinkList(Model.Links) %></div>
    <div id="photo-tags">
    <%
        foreach (var tagInfo in Model.Tags) { %>
        <span class="photo-tag photo-tag-<%= tagInfo.RelativeSize %>"><%= Html.ActionLinkTag(tagInfo) %></span>
    <% } %>
    </div>
</div>
</asp:Content>