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
        //Declares a hashed password variable.
        static string hashedPassword;
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Login_Click(object sender, EventArgs e)
        {
            //Declares a connection and SQL command variable.
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand();   
            cmd.Connection = conn;
            cmd.CommandText = "SELECT AdminPassword FROM AdminPassword WHERE Id='1'";
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                //Retrieves the universal admin password.
                hashedPassword = reader["AdminPassword"].ToString();
            }
            //Checks if the hashed password is correct. If it is, grants access. If not, returns an error message.
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
            //Closes the database connection.
            conn.Close();
        }
    }
}
