using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication3
{
    public partial class Change_Admin_Password : System.Web.UI.Page
    {
        //Declares a hashed password variable.
        static string hashedPassword;
        protected void Page_Load(object sender, EventArgs e)
        {
            //If an admin login session is set, do nothing. If it is not, return to the admin login page.
            if (Session["AdminUsername"] != null)
            {
                
            }
            else
            {
                Response.Redirect("Admin_Login.aspx");
            }
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            //If the New Password and Confirm New Password text boxes do not match, generate an error message. If they do, proceed.
            if (NewPassword.Text != ConfirmNewPassword.Text)
            {
                Error.Text = "New Password and Confirm New Password do not match.";
            }
            else
            {
                //Declare connection and SQL query variables.
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "SELECT AdminPassword FROM AdminPassword WHERE Id = '1'";
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    //Retrieve the universal admin password.
                    hashedPassword = reader["AdminPassword"].ToString();
                }
                reader.Close();
                //Checks if the input existing password is correct. If it is, proceeds. If not, generates an error message.
                bool correct = Salt.Verify(OldPassword.Text, hashedPassword);
                if (correct == false)
                {
                    Error.Text = "Incorrect old password.";
                }
                else
                {
                    SqlCommand cmd2 = new SqlCommand();
                    cmd2.Connection = conn;
                    //Updates the universal admin password, changes the message to reflect this, and redirects to the Admin page.
                    cmd2.CommandText = "UPDATE AdminPassword SET AdminPassword = @newPassword WHERE Id = '1'";
                    cmd2.Parameters.Add("@newPassword", SqlDbType.VarChar).Value = Salt.Encode(NewPassword.Text, null);
                    cmd2.ExecuteNonQuery();
                    Session["AdminMessage"] = "Admin password successfully changed.";
                    Response.Redirect("Admin.aspx");
                }
                //Closes the database connection.
                conn.Close();
            }
        }
    }
}
