using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication3
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //If a login session is set, do nothing. If it is not, return to the user login page.
            if (Session["Message"] != null)
            {

            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }

        protected void Logout_Click(object sender, EventArgs e)
        {
            //End the user's session, and return to the user login page.
            Session.Contents.RemoveAll();
            Session.Abandon();
            Response.Redirect("Login.aspx");
        }
    }
}
