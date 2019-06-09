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
    public partial class Check_Orders : System.Web.UI.Page
    {
        //Declare table and SQL connection variables
        static DataTable table = new DataTable();
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            //If a login session is set, check if the page has been posted back. If it has, fill the Orders dropdown menu from the database. If a login session is not set, return to the user login page.
            if (Session["Message"] != null)
            {
                if (!IsPostBack)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "Select ID FROM Orders WHERE Customer = @customer";
                    cmd.Parameters.Add("@customer", SqlDbType.Int).Value = Session["Id"];
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
                Response.Redirect("Login.aspx");
            }
        }

        protected void OrderNumbers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (OrderNumbers.SelectedIndex > 0)
            {
                //Declare variables.
                var itemToAdd = new Item();
                SqlCommand cmd = new SqlCommand();
                //Clear all rows from the table, refill it with all relevant information from the database, and display it.
                table.Rows.Clear();
                cmd.Connection = conn;
                cmd.CommandText = "SELECT Orders.ID, OrderStock.StockCode, Stock.Description, OrderStock.Quantity, Orders.OrderTotal FROM Stock INNER JOIN ((Customer INNER JOIN Orders ON Customer.Id = Orders.Customer) INNER JOIN OrderStock ON Orders.Id = OrderStock.OrderID) ON Stock.Code = OrderStock.StockCode WHERE Orders.ID = @order";
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
                    Total.Text = "£" + reader["OrderTotal"].ToString();
                }
                Items.DataSource = table;
                Items.DataBind();
            } 
        }
    }
}
