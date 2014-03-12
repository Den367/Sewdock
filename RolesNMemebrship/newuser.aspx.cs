using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class newuser : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void CreateUserWizard1_CreatedUser(object sender, EventArgs e)
    {
        if (RadioButtonList1.SelectedValue == "0")
        {
            string username = CreateUserWizard1.UserName;
            Roles.AddUserToRole(username, "Regular");
        }
        else if (RadioButtonList1.SelectedValue == "1")
        {
            string username = CreateUserWizard1.UserName;
            Roles.AddUserToRole(username, "Premium");
        }
    }
}
