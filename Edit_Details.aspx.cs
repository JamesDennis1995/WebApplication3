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
    public partial class Edit_Details : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //If an admin login session is set, do nothing. If it is not, return to the admin login page.
            if (Session["Message"] != null)
            {

            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }
        protected void Address2_TextChanged(object sender, EventArgs e)
        {
            if (Address2.Text == "")
            {
                LeaveBlank.Checked = false;
                LeaveBlank.Attributes.Add("disabled", "disabled");
            }
            else
            {
                LeaveBlank.Enabled = true;
                Error.Text = Address2.Text;
            }
        }
        protected void Submit_Click(object sender, EventArgs e)
        {
            //If all fields are blank, generate an error message. Otherwise, declare SQL connection and command variables and update the relevant record in the database with all non-blank fields, update the session message to reflect this, and redirect to the User page.
            if (FirstName.Text == "" && Surname.Text == "" && Password.Text == "" && ContactNumber.Text == "" && Email.Text == "" && Address1.Text == "" && Address2.Text == "" && TownCity.Text == "" && County.Text == "" && Postcode.Text == "")
            {
                Error.Text = "You must enter at least one variable to edit.";
            }
            else
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                SqlCommand cmd = new SqlCommand();
                SqlCommand cmd2 = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "SELECT * FROM Customer WHERE Id = @id";
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = Session["Id"];
                cmd2.Connection = conn;
                cmd2.CommandText = "UPDATE Customer SET FirstName = @firstName, Surname = @surname, Password = @password, ContactNumber = @contactNumber, Email = @email, Address1 = @address1, Address2 = @address2, TownCity = @townCity, County = @county, Postcode = @postcode WHERE Id = @id";
                cmd2.Parameters.Add("@id", SqlDbType.Int).Value = Session["Id"];
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (FirstName.Text == "")
                    {
                        cmd2.Parameters.Add("@firstName", SqlDbType.VarChar).Value = reader["FirstName"].ToString();
                    }
                    else
                    {
                        cmd2.Parameters.Add("@firstName", SqlDbType.VarChar).Value = FirstName.Text;
                    }
                    if (Surname.Text == "")
                    {
                        cmd2.Parameters.Add("@surname", SqlDbType.VarChar).Value = reader["Surname"].ToString();
                    }
                    else
                    {
                        cmd2.Parameters.Add("@surname", SqlDbType.VarChar).Value = Surname.Text;
                    }
                    if (Password.Text == "")
                    {
                        cmd2.Parameters.Add("@password", SqlDbType.VarChar).Value = reader["Password"].ToString();
                    }
                    else
                    {
                        cmd2.Parameters.Add("@password", SqlDbType.VarChar).Value = Salt.Encode(Password.Text, null);
                    }
                    if (ContactNumber.Text == "")
                    {
                        cmd2.Parameters.Add("@contactNumber", SqlDbType.VarChar).Value = reader["ContactNumber"].ToString();
                    }
                    else
                    {
                        cmd2.Parameters.Add("@contactNumber", SqlDbType.VarChar).Value = ContactNumber.Text;
                    }
                    if (Email.Text == "")
                    {
                        cmd2.Parameters.Add("@email", SqlDbType.VarChar).Value = reader["Email"].ToString();
                    }
                    else
                    {
                        cmd2.Parameters.Add("@email", SqlDbType.VarChar).Value = Email.Text;
                    }
                    if (Address1.Text == "")
                    {
                        cmd2.Parameters.Add("@address1", SqlDbType.VarChar).Value = reader["Address1"].ToString();
                    }
                    else
                    {
                        cmd2.Parameters.Add("@address1", SqlDbType.VarChar).Value = Address1.Text;
                    }
                    if (Address2.Text == "" && LeaveBlank.Checked == false)
                    {
                        cmd2.Parameters.Add("@address2", SqlDbType.VarChar).Value = reader["Address2"].ToString();
                    }
                    else
                    {
                        cmd2.Parameters.Add("@address2", SqlDbType.VarChar).Value = Address2.Text;
                    }
                    if (TownCity.Text == "")
                    {
                        cmd2.Parameters.Add("@townCity", SqlDbType.VarChar).Value = reader["TownCity"].ToString();
                    }
                    else
                    {
                        cmd2.Parameters.Add("@townCity", SqlDbType.VarChar).Value = TownCity.Text;
                    }
                    if (County.Text == "")
                    {
                        cmd2.Parameters.Add("@county", SqlDbType.VarChar).Value = reader["County"].ToString();
                    }
                    else
                    {
                        cmd2.Parameters.Add("@county", SqlDbType.VarChar).Value = County.Text;
                    }
                    if (Postcode.Text == "")
                    {
                        cmd2.Parameters.Add("@postcode", SqlDbType.VarChar).Value = reader["Postcode"].ToString();
                    }
                    else
                    {
                        cmd2.Parameters.Add("@postcode", SqlDbType.VarChar).Value = Postcode.Text;
                    }
                }
                reader.Close();
                cmd2.ExecuteNonQuery();
                conn.Close();
                Session["Message"] = "Your details have been successfully updated.";
                Response.Redirect("Customer.aspx");
            }
        }
        protected void LeaveBlank_CheckedChanged(object sender, EventArgs e)
        {
            if (LeaveBlank.Checked == true)
            {
                Address2.Text = "";
                Address2.ReadOnly = true;
            }
            else
            {
                Address2.ReadOnly = false;
            }
        }
    }
}
