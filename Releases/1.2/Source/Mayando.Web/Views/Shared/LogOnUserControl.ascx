<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<% if (Request.IsAuthenticated) { %>
                <%= Html.Encode(Resources.LogOnWelcome) %> <b><%= Html.Encode(Page.User.Identity.Name) %></b>!
                <%= Html.ActionLinkAccountLogOff(Resources.AccountLogOff) %>
                <%= Html.ActionLinkAccountChangePassword(Resources.AccountChangePassword) %>
<% } else { %>
                <%= Html.ActionLinkAccountLogOn(Resources.AccountLogOn) %>
<% } %>