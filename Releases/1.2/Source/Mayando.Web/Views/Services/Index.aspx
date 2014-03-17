<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ServicesViewModel>" %>

<asp:Content ID="title" ContentPlaceHolderID="TitleContent" runat="server">
	Service API
</asp:Content>

<asp:Content ID="content" ContentPlaceHolderID="MainContent" runat="server">
<div id="servicesindexpage">
    <h2>Service API</h2>
    <p>You can use the Service API to interact with Mayando through client applications, e.g. to automate certain administrative tasks.</p>
    <fieldset>
        <legend>Status</legend>
        <% if (Model.ServiceApiEnabled) { %>
        <p>The Service API is currently enabled with the following API Key:</p>
        <pre><%= Model.ApiKey %></pre>
        <% using (Html.BeginServicesDisableServiceApi()) { %>
            <p><input type="submit" value="Disable Service API" /> if you no longer want to allow service requests.</p>
        <% } %>
        <% using (Html.BeginServicesRequestNewApiKey()) { %>
            <p><input type="submit" value="Request New API Key" /> in case your API key was compromised.</p>
        <% } %>
        <% } else { %>
        <p>The Service API is currently disabled.</p>
        <% using (Html.BeginServicesEnableServiceApi()) { %>
            <p><input type="submit" value="Enable Service API" /></p>
        <% } %>
        <% } %>
    </fieldset>
</div>
</asp:Content>