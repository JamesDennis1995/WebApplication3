using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication3
{
    public partial class Admin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["AdminUsername"] != null)
            {
                Welcome.Text = Session["AdminMessage"].ToString();
            }
            else
            {
                Response.Redirect("Admin_Login.aspx");
            }
        }

        protected void Logout_Click(object sender, EventArgs e)
        {
            Session.Contents.RemoveAll();
            Session.Abandon();
            Response.Redirect("Admin_Login.aspx");
        }
    }
}