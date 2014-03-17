<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<Mayando.Web.ViewModels.CalendarViewModel>" MasterPageFile="~/Views/Shared/Site.Master" %>

<asp:Content ID="title" ContentPlaceHolderID="TitleContent" runat="server">
	<%= Html.Encode(Resources.PhotosCalendar) %>
</asp:Content>

<asp:Content ID="content" ContentPlaceHolderID="MainContent" runat="server">
<div id="photocalendarpage">
    <h2><%= Html.Encode(Resources.PhotosCalendar) %></h2>
    <div class="linklist"><%= Html.LinkList(Model.Links) %></div>
    <ul class="calendar-list-year">
        <% foreach (var yearItem in Model.Years) { %>
        <li class="calendar-item-year" id="calendar-<%= yearItem.Year.Year %>">
            <%= Html.ActionLinkPhotoYear(yearItem.Type, yearItem.Year) %> <span class="calendar-item-description">(<%= Html.NumberOfPhotos(yearItem.TotalPhotos) %>)</span>
            <% if (yearItem.DisplayMonths) { %>
            <ul class="calendar-list-month">
                <% foreach (var monthItem in yearItem.Months) { %>
                <li class="calendar-item-month" id="calendar-<%= monthItem.Month.Year %>-<%= monthItem.Month.Month %>">
                    <%= Html.ActionLinkPhotoMonth(monthItem.Type, monthItem.Month) %> <span class="calendar-item-description">(<%= Html.NumberOfPhotos(monthItem.TotalPhotos) %>)</span>
                    <% if(monthItem.DisplayDays) { %>
                    <ul class="calendar-list-day">
                        <% foreach (var dayItem in monthItem.Days) { %>
                        <li class="calendar-item-day" id="calendar-<%= dayItem.Day.Year %>-<%= dayItem.Day.Month %>-<%= dayItem.Day.Day %>">
                            <%= Html.ActionLinkPhotoDay(dayItem.Type, dayItem.Day) %> <span class="calendar-item-description">(<%= Html.NumberOfPhotos(dayItem.TotalEmbros)%>)</span>
                        </li>
                        <% } %>
                    </ul>
                    <% } %>
                </li>
                <% } %>
            </ul>
            <% } %>
        </li>
        <% } %>
    </ul>
</div>
</asp:Content>