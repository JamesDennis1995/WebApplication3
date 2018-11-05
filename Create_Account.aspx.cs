using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication3
{
    public partial class Create_Account : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void CreateAccount_Click(object sender, EventArgs e)
        {
            if (FirstName.Text == null || Surname.Text == null || Password.Text == null || ContactNumber.Text == null || Email.Text == null || Address1.Text == null || TownCity.Text == null || County.Text == null || Postcode.Text == null)
            {
                Error.Text = "You must enter something for all fields.";
            }
            else
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                SqlCommand cmd = new SqlCommand();
                SqlCommand cmd2 = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "SELECT COUNT(*) FROM Customer WHERE Email = @email";
                cmd2.Connection = conn;
                cmd2.CommandText = "INSERT INTO Customer(FirstName, Surname, Password, ContactNumber, Email, Address1, Address2, TownCity, County, Postcode) VALUES (@firstname, @surname, @password, @contactnumber, @email, @address1, @address2, @towncity, @county, @postcode)";
                cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = Email.Text;
                cmd2.Parameters.Add("@firstname", SqlDbType.VarChar).Value = FirstName.Text;
                cmd2.Parameters.Add("@surname", SqlDbType.VarChar).Value = Surname.Text;
                cmd2.Parameters.Add("@password", SqlDbType.VarChar).Value = Salt.Encode(Password.Text, null);
                cmd2.Parameters.Add("@contactnumber", SqlDbType.VarChar).Value = ContactNumber.Text;
                cmd2.Parameters.Add("@email", SqlDbType.VarChar).Value = Email.Text;
                cmd2.Parameters.Add("@address1", SqlDbType.VarChar).Value = Address1.Text;
                cmd2.Parameters.Add("@address2", SqlDbType.VarChar).Value = Address2.Text;
                cmd2.Parameters.Add("@towncity", SqlDbType.VarChar).Value = TownCity.Text;
                cmd2.Parameters.Add("@county", SqlDbType.VarChar).Value = County.Text;
                cmd2.Parameters.Add("@postcode", SqlDbType.VarChar).Value = Postcode.Text;
                conn.Open();
                int rowNumber = (int)cmd.ExecuteScalar();
                if (rowNumber > 0)
                {
                    Error.Text = "That email address is already in use.";
                }
                else
                {
                    cmd2.ExecuteNonQuery();
                    conn.Close();
                    Response.Redirect("Start.aspx");
                }
            }
        }
    }
}