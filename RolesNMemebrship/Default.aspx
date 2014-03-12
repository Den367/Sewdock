<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        Welcome
        <asp:LoginName ID="LoginName1" runat="server" />
        <br />
        <br />
        <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="~/Default.aspx">Home</asp:HyperLink>
        |
        <asp:HyperLink ID="HyperLink5" runat="server" NavigateUrl="~/ViewDownloads.aspx">Free</asp:HyperLink>
        |
        <asp:HyperLink ID="HyperLink6" runat="server" NavigateUrl="~/Members/ViewDownloads.aspx">Regular</asp:HyperLink>
        |
        <asp:HyperLink ID="HyperLink7" runat="server" NavigateUrl="~/PremiumMembers/ViewDownloads.aspx">Premium</asp:HyperLink>
        |
        <asp:LoginStatus ID="LoginStatus1" runat="server" />
        &nbsp;<br />
        <br />
        This is a Dummy Download Website<br />
        <br />
        Please select your Account type to enter:<br />
        <br />
        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/ViewDownloads.aspx">Free Users</asp:HyperLink><br />
        <br />
        <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/Members/ViewDownloads.aspx">Regular Members</asp:HyperLink>
        <br />
        <br />
        <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="~/PremiumMembers/ViewDownloads.aspx">Premium Members</asp:HyperLink></div>
    </form>
</body>
</html>
