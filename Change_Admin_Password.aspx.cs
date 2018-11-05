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
        static string hashedPassword;
        protected void Page_Load(object sender, EventArgs e)
        {
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
            if (NewPassword.Text != ConfirmNewPassword.Text)
            {
                Error.Text = "New Password and Confirm New Password do not match.";
            }
            else
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "SELECT AdminPassword FROM AdminPassword WHERE Id = '1'";
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    hashedPassword = reader["AdminPassword"].ToString();
                }
                reader.Close();
                bool correct = Salt.Verify(OldPassword.Text, hashedPassword);
                if (correct == false)
                {
                    Error.Text = "Incorrect old password.";
                }
                else
                {
                    SqlCommand cmd2 = new SqlCommand();
                    cmd2.Connection = conn;
                    cmd2.CommandText = "UPDATE AdminPassword SET AdminPassword = @newPassword WHERE Id = '1'";
                    cmd2.Parameters.Add("@newPassword", SqlDbType.VarChar).Value = Salt.Encode(NewPassword.Text, null);
                    cmd2.ExecuteNonQuery();
                    Session["AdminMessage"] = "Admin password successfully changed.";
                    Response.Redirect("Admin.aspx");
                }
                conn.Close();
            }
        }
    }
}