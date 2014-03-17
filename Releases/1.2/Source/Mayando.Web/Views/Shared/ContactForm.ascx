<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
    <% using (Html.BeginContactSendMessageForm()) { %>
    <div id="contact-form">
        <h3><%= Html.Encode(Resources.ContactContactMe) %></h3>
        <%= Html.ValidationSummary(Html.Encode(Resources.ValidationSummaryGenericTitle)) %>
        <table>
            <tbody>
                <tr>
                    <td><label for="AuthorName" class="required"><%= Html.Encode(Resources.ContactAuthorName) %></label></td>
                    <td><%= Html.TextBox("AuthorName") %><%= Html.ValidationMessage("AuthorName", "*") %></td>
                </tr>
                <tr>
                    <td><label for="AuthorEmail" class="required"><%= Html.Encode(Resources.ContactAuthorEmail) %></label></td>
                    <td><%= Html.TextBox("AuthorEmail")%><%= Html.ValidationMessage("AuthorEmail", "*") %></td>
                </tr>
                <tr>
                    <td style="vertical-align: top"><label for="Text" class="required"><%= Html.Encode(Resources.ContactAuthorMessage) %></label></td>
                    <td><%= Html.TextArea("Text", string.Empty, 8, 80, null)%><%= Html.ValidationMessage("Text", "*") %></td>
                </tr>
                <tr>
                    <td colspan="1"><input type="submit" value="<%= Html.AttributeEncode(Resources.ContactSubmit) %>" /></td>
                    <td><%= Html.CheckBox("RememberMe") %> <label class="inline optional" for="RememberMe"><%= Html.Encode(Resources.ContactAuthorRememberMe) %></label></td>
                </tr>
            </tbody>
        </table>
    </div>
    <% } %>