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
    public partial class Add_Stock : System.Web.UI.Page
    {
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
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlCommand cmd2 = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "SELECT COUNT(*) FROM Stock WHERE Code = @code";
            cmd2.Connection = conn;
            cmd2.CommandText = "INSERT INTO Stock (Code, Description, PricePerUnit) VALUES (@code, @description, @priceperunit)";
            cmd.Parameters.Add("@code", SqlDbType.VarChar).Value = Code.Text;
            conn.Open();
            int rowNumber = (int)cmd.ExecuteScalar();
            if (rowNumber > 0)
            {
                Error.Text = "That stock code is already in use.";
            }
            else if (Code.Text == null || Description.Text == null || PriceUnit.Text == null || !Image.HasFile)
            {
                Error.Text = "You must enter something for all fields.";
            }
            else if (!Image.FileName.Contains(".jpg"))
            {
                Error.Text = "The uploaded image must be JPG.";
            }
            else
            {
                cmd2.Parameters.Add("@code", SqlDbType.VarChar).Value = Code.Text;
                cmd2.Parameters.Add("@description", SqlDbType.VarChar).Value = Description.Text;
                cmd2.Parameters.Add("@priceperunit", SqlDbType.Float).Value = PriceUnit.Text;
                cmd2.ExecuteNonQuery();
                Image.SaveAs(Server.MapPath("~/Images/") + Code.Text + ".jpg");
                Session["AdminMessage"] = "Stock item successfully added.";
                Response.Redirect("Admin.aspx");
            }
            conn.Close();
        }
    }
}
