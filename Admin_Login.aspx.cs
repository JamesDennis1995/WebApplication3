using System;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication3
{
    public partial class Admin_Login : System.Web.UI.Page
    {
        static string hashedPassword;
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Login_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand();   
            cmd.Connection = conn;
            cmd.CommandText = "SELECT AdminPassword FROM AdminPassword WHERE Id='1'";
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                hashedPassword = reader["AdminPassword"].ToString();
            }
            bool correct = Salt.Verify(AdminPassword.Text, hashedPassword);
            if (correct == false)
            {
                Error.Text = "Incorrect password.";
            }
            else
            {
                Session["AdminUsername"] = "Admin";
                Session["AdminMessage"] = "Welcome.";
                Response.Redirect("Admin.aspx");
            }
            conn.Close();
        }
    }
}