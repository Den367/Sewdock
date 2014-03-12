<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

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
        Please sign in using your credentials<br />
        <br />
        <asp:Login ID="Login1" runat="server" BackColor="#F7F6F3" BorderColor="#E6E2D8" BorderPadding="4"
            BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="0.8em"
            ForeColor="#333333" CreateUserText="Create Account Now" CreateUserUrl="newuser.aspx">
            <TitleTextStyle BackColor="#5D7B9D" Font-Bold="True" Font-Size="0.9em" ForeColor="White" />
            <InstructionTextStyle Font-Italic="True" ForeColor="Black" />
            <TextBoxStyle Font-Size="0.8em" />
            <LoginButtonStyle BackColor="#FFFBFF" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px"
                Font-Names="Verdana" Font-Size="0.8em" ForeColor="#284775" />
        </asp:Login>
    
    </div>
    </form>
</body>
</html>
