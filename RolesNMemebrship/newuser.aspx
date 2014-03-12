<%@ Page Language="C#" AutoEventWireup="true" CodeFile="newuser.aspx.cs" Inherits="newuser" %>

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
        <asp:CreateUserWizard ID="CreateUserWizard1" runat="server" OnCreatedUser="CreateUserWizard1_CreatedUser">
            <WizardSteps>
                <asp:CreateUserWizardStep runat="server">
                </asp:CreateUserWizardStep>
                <asp:CompleteWizardStep runat="server">
                </asp:CompleteWizardStep>
            </WizardSteps>
        </asp:CreateUserWizard>
        <br />
        <asp:RadioButtonList ID="RadioButtonList1" runat="server">
            <asp:ListItem Value="0">Regular</asp:ListItem>
            <asp:ListItem Value="1">Premium</asp:ListItem>
        </asp:RadioButtonList></div>
    </form>
</body>
</html>
