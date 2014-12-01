<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<Myembro.ViewModels.PageViewModel>" MasterPageFile="~/Views/Shared/Site.Master" %>

<asp:Content ID="title" ContentPlaceHolderID="TitleContent" runat="server">
    <%= Html.Encode(Model.Page.Title) %>
</asp:Content>

<asp:Content ID="admin" ContentPlaceHolderID="AdminLinks" runat="server">
                <%= Html.ActionLinkPageEdit("Edit Page", Model.Page.Id) %>
                <%= Html.ActionLinkPageDelete("Delete Page", Model.Page.Id) %>
                <%= Html.ActionLinkMenuCreateForPage("Create Menu For Page", Model.Page.Slug) %>
</asp:Content>

<asp:Content ID="content" ContentPlaceHolderID="MainContent" runat="server">
<div id="pagedetailspage">
    <h2><%= Html.Encode(Model.Page.Title)%></h2>
    <% if (!string.IsNullOrEmpty(Model.Page.Text)) { %>
    <div id="page-text"><%= Model.Page.Text %></div>
    <% } %>
    <% if (Model.Embro != null)
       {
           using (Html.BeginPagesAddCommentForm(Model.Page.Id, "submitComment"))
           {
               Html.RenderPartial(PartialViewName.EmbroDetails, Model.Embro);
           }
       }
       if (Model.Page.ShowContactForm)
       {
           Html.RenderPartial(PartialViewName.ContactForm);
       }
    %>
</div>
</asp:Content>