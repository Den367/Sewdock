<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
                <%= Html.TextBox("TagList") %>
                <span title="Separate multiple tags with commas" class="help">?</span>
                <%= Html.ValidationMessage("TagList", "*") %>