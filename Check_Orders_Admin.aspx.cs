using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication3
{
    public partial class Check_Orders_Admin : System.Web.UI.Page
    {
        //Declare table and SQL connection variables.
        static DataTable table = new DataTable();
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            //If an admin login session is not set, return to the admin login page. If it is, check if the page has been posted back. If it has, fill the Orders dropdown menu from the database.
            if (Session["AdminUsername"] != null)
            {
                if (!IsPostBack)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT Id FROM Orders";
                    OrderNumbers.Items.Add("");
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        OrderNumbers.Items.Add(reader["Id"].ToString());
                    }
                    conn.Close();
                    if (table.Columns.Count == 0)
                    {
                        table.Columns.Add("Image", typeof(String));
                        table.Columns.Add("Stock Code", typeof(String));
                        table.Columns.Add("Description", typeof(String));
                        table.Columns.Add("Quantity", typeof(String));
                    }
                }
            }
            else
            {
                Response.Redirect("Admin_Login.aspx");
            }
        }
        protected void OrderNumbers_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Declare variables.
            var itemToAdd = new Item();
            //Clear all rows from the table, refill it with all relevant information from the database, and display it.
            table.Rows.Clear();
            if (OrderNumbers.SelectedValue != "")
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "SELECT Orders.Id, Customer.FirstName, Customer.Surname, Customer.ContactNumber, Customer.Address1, Customer.Address2, Customer.TownCity, Customer.County, Customer.Postcode, OrderStock.StockCode, Stock.Description, OrderStock.Quantity, Orders.OrderTotal FROM Stock INNER JOIN ((Customer INNER JOIN Orders ON Customer.Id = Orders.Customer) INNER JOIN OrderStock ON Orders.Id = OrderStock.OrderID) ON Stock.Code = OrderStock.StockCode WHERE Orders.ID = @order";
                cmd.Parameters.Add("@order", SqlDbType.Int).Value = Int32.Parse(OrderNumbers.SelectedItem.ToString());
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    itemToAdd.code = reader["StockCode"].ToString();
                    itemToAdd.description = reader["Description"].ToString();
                    itemToAdd.quantity = Int32.Parse(reader["Quantity"].ToString());
                    DataRow newRow = table.NewRow();
                    newRow["Image"] = ResolveUrl("/Images/" + itemToAdd.code.ToString() + ".jpg");
                    newRow["Stock Code"] = itemToAdd.code.ToString();
                    newRow["Description"] = itemToAdd.description.ToString();
                    newRow["Quantity"] = itemToAdd.quantity.ToString();
                    table.Rows.Add(newRow);
                    Name.Text = reader["FirstName"].ToString() + " " + reader["Surname"].ToString();
                    if (reader["Address2"].ToString() != "")
                    {
                        Address.Text = reader["Address1"].ToString() + ", " + reader["Address2"].ToString() + ", " + reader["TownCity"].ToString() + ", " + reader["County"].ToString() + ", " + reader["Postcode"].ToString();
                    }
                    else
                    {
                        Address.Text = reader["Address1"].ToString() + ", " + reader["TownCity"].ToString() + ", " + reader["County"].ToString() + ", " + reader["Postcode"].ToString();
                    }
                    Number.Text = reader["ContactNumber"].ToString();
                    Total.Text = "£" + reader["OrderTotal"].ToString();
                }
            }
            else
            {
                Name.Text = "";
                Address.Text = "";
                Number.Text = "";
                Total.Text = "";
            }
            Items.DataSource = table;
            Items.DataBind();
        }
    }
}
