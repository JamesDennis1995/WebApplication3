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
        //Declare a hashed password variable.
        static string hashedPassword;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void CustomerLogin_Click(object sender, EventArgs e)
        {
            //Declare SQL connection and command variables.
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
            //Check if any email address in the database matches the one entered. If no rows are returned, generate an error message. Otherwise, proceed.
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows == false)
            {
                Error.Text = "Incorrect email address.";
            }
            else
            {
                while (reader.Read())
                {
                    //Sets the hashed password variable.
                    hashedPassword = reader["Password"].ToString();
                }
                reader.Close();
                //Checks the hashed password. If it isn't correct, generate an error message. If it is, set session ID and message and redirect to the User page.
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
