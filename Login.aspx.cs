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
    public partial class Login : System.Web.UI.Page
    {
        static string hashedPassword;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void CustomerLogin_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlCommand cmd2 = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "SELECT Password FROM Customer WHERE Email = @email";
            cmd.Parameters.Add("@email", SqlDbType.NChar).Value = Email.Text;
            cmd2.Connection = conn;
            cmd2.CommandText = "SELECT ID, FirstName FROM Customer WHERE Email = @email";
            cmd2.Parameters.Add("@email", SqlDbType.NChar).Value = Email.Text;
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows == false)
            {
                Error.Text = "Incorrect email address.";
            }
            else
            {
                while (reader.Read())
                {
                    hashedPassword = reader["Password"].ToString();
                }
                reader.Close();
                bool correct = Salt.Verify(Password.Text, hashedPassword);
                if (correct == false)
                {
                    Error.Text = "Incorrect password.";
                }
                else
                {
                    SqlDataReader reader2 = cmd2.ExecuteReader();
                    while (reader2.Read())
                    {
                        Session["Id"] = Int32.Parse(reader2["Id"].ToString());
                        Session["Message"] = "Welcome, " + reader2["FirstName"].ToString() + ".";
                    }
                    Response.Redirect("Customer.aspx");
                }
            }
        }
    }
}