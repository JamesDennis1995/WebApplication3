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
    public partial class Order : System.Web.UI.Page
    {
        static List<String> codes = new List<String>();
        static List<String> descriptions = new List<String>();
        static List<decimal> prices = new List<decimal>();
        static List<Item> basket = new List<Item>();
        static DataTable table = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Message"] != null)
            {
                if (!IsPostBack)
                {
                    Image.Visible = false;
                    Image.Attributes.Add("style", "height:125px;width:auto");
                    codes.Clear();
                    descriptions.Clear();
                    prices.Clear();
                    basket.Clear();
                    codes.Add("");
                    descriptions.Add("");
                    prices.Add(decimal.Parse("0"));
                    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT * FROM Stock";
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        codes.Add(reader["Code"].ToString());
                        descriptions.Add(reader["Description"].ToString());
                        prices.Add(decimal.Parse(reader["PricePerUnit"].ToString()));
                    }
                    Stock.DataSource = codes;
                    Stock.DataBind();
                    if (table.Columns.Count == 0)
                    {
                        table.Columns.Add(new DataColumn("Image", typeof(String)));
                        table.Columns.Add(new DataColumn("Stock Code", typeof(String)));
                        table.Columns.Add(new DataColumn("Quantity", typeof(String)));
                        table.Columns.Add(new DataColumn("Total Price", typeof(String)));
                        OrderItems.DataSource = table;
                        OrderItems.DataBind();
                    }
                    ItemToRemove.Items.Add("");
                }

            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }
        protected void Stock_SelectedIndexChanged(object sender, EventArgs e)
        {
            int number = Stock.SelectedIndex;
            if (number > 0)
            {
                Image.Visible = true;
                Image.ImageUrl = "~/Images/" + Stock.SelectedItem.ToString() + ".jpg";
                Description.Text = descriptions[number];
                Price.Text = "£" + prices[number].ToString();
            }
            else
            {
                Image.Visible = false;
                Description.Text = "";
                Price.Text = "";
            }
        }

        protected void AddToBasket_Click(object sender, EventArgs e)
        {
            if (Stock.SelectedIndex == 0)
            {
                Error.Text = "You must first select an item.";
            }
            else if (Quantity.Text == "")
            {
                Error.Text = "You must first select a quantity.";
            }
            else
            {
                bool found = false;
                var itemToAdd = new Item();
                if (basket.Count > 0)
                {
                    for (int i = 0; i < basket.Count; i++)
                    {
                        if (basket[i].code == Stock.SelectedItem.ToString())
                        {
                            found = true;
                            basket[i].quantity += Int32.Parse(Quantity.Text);
                            basket[i].total = Math.Round(basket[i].quantity * prices[basket[i].number], 2);
                            table.Rows[i][2] = basket[i].quantity;
                            table.Rows[i][3] = basket[i].total;
                            break;
                        }
                    }
                    if (found == false)
                    {
                        
                        itemToAdd.number = Stock.SelectedIndex;
                        itemToAdd.code = Stock.SelectedItem.ToString();
                        itemToAdd.quantity = Int32.Parse(Quantity.Text);
                        itemToAdd.total = Math.Round(Int32.Parse(Quantity.Text) * prices[Stock.SelectedIndex], 2);
                        basket.Add(itemToAdd);
                        ItemToRemove.Items.Add(itemToAdd.code);
                        DataRow newRow = table.NewRow();
                        newRow["Image"] = ResolveUrl("/Images/" + itemToAdd.code.ToString() + ".jpg");
                        newRow["Stock Code"] = itemToAdd.code.ToString();
                        newRow["Quantity"] = itemToAdd.quantity.ToString();
                        newRow["Total Price"] = itemToAdd.total.ToString();
                        table.Rows.Add(newRow);
                    }
                }
                else
                {
                    itemToAdd.number = Stock.SelectedIndex;
                    itemToAdd.code = Stock.SelectedItem.ToString();
                    itemToAdd.quantity = Int32.Parse(Quantity.Text);
                    itemToAdd.total = Math.Round(Int32.Parse(Quantity.Text) * prices[Stock.SelectedIndex], 2);
                    basket.Add(itemToAdd);
                    ItemToRemove.Items.Add(itemToAdd.code);
                    DataRow newRow = table.NewRow();
                    newRow["Image"] = ResolveUrl("/Images/" + itemToAdd.code.ToString() + ".jpg");
                    newRow["Stock Code"] = itemToAdd.code.ToString();
                    newRow["Quantity"] = itemToAdd.quantity.ToString();
                    newRow["Total Price"] = itemToAdd.total.ToString();
                    table.Rows.Add(newRow);
                }
                OrderItems.DataSource = table;
                OrderItems.DataBind();
                Quantity.Text = "";
            }
        }

        protected void ItemToRemove_SelectedIndexChanged(object sender, EventArgs e)
        {
            NumberToRemove.Items.Clear();
            if (ItemToRemove.SelectedIndex > 0)
            {
                if (basket[ItemToRemove.SelectedIndex - 1].quantity > 1)
                {
                    for (int i = 1; i < basket[ItemToRemove.SelectedIndex - 1].quantity; i++)
                    {
                        NumberToRemove.Items.Add(i.ToString());
                    }
                }
                NumberToRemove.Items.Add("All");
            }
        }
        protected void Remove_Click(object sender, EventArgs e)
        {
            if (basket.Count == 0)
            {
                Error.Text = "You have nothing in your basket.";
            }
            else if (ItemToRemove.SelectedIndex == 0)
            {
                Error.Text = "Please select an item to remove.";
            }
            else
            {
                if (NumberToRemove.SelectedItem.ToString() == "All")
                {
                    basket.RemoveAt(ItemToRemove.SelectedIndex - 1);
                    table.Rows[0].Delete();
                    ItemToRemove.Items.Remove(ItemToRemove.SelectedItem);
                }
                else
                {
                    basket[ItemToRemove.SelectedIndex - 1].quantity -= Int32.Parse(NumberToRemove.SelectedItem.ToString());
                    basket[ItemToRemove.SelectedIndex - 1].total = Math.Round(basket[ItemToRemove.SelectedIndex - 1].quantity * prices[basket[ItemToRemove.SelectedIndex - 1].number], 2);
                    table.Rows[ItemToRemove.SelectedIndex - 1][1] = basket[ItemToRemove.SelectedIndex - 1].quantity;
                    table.Rows[ItemToRemove.SelectedIndex - 1][2] = basket[ItemToRemove.SelectedIndex - 1].total;
                    NumberToRemove.Items.Clear();
                    if (basket[ItemToRemove.SelectedIndex - 1].quantity > 1)
                    {
                        for (int i = 1; i < basket[ItemToRemove.SelectedIndex - 1].quantity; i++)
                        {
                            NumberToRemove.Items.Add(i.ToString());
                        }
                    }
                    NumberToRemove.Items.Add("All");
                }
                OrderItems.DataSource = table;
                OrderItems.DataBind();
            }
        }

        protected void PlaceOrder_Click(object sender, EventArgs e)
        {
            if (basket.Count == 0)
            {
                Error.Text = "You must first have something in your basket.";
            }
            else
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                SqlCommand cmd = new SqlCommand();
                SqlCommand cmd2 = new SqlCommand();
                SqlCommand cmd3 = new SqlCommand();
                decimal orderTotal = 0;
                for (int i = 0; i < basket.Count; i++)
                {
                    orderTotal += decimal.Parse(basket[i].total.ToString());
                }
                orderTotal = Math.Round(orderTotal, 2);
                cmd.Connection = cmd2.Connection = cmd3.Connection = conn;
                cmd.CommandText = "INSERT INTO Orders (Customer, OrderTotal) VALUES (@customer, @total)";
                cmd2.CommandText = "SELECT COUNT(*) FROM Orders";
                cmd3.CommandText = "INSERT INTO OrderStock (OrderID, StockCode, Quantity) VALUES (@orderID, @stockCode, @quantity)";
                cmd.Parameters.Add("@customer", SqlDbType.Int).Value = Session["Id"];
                cmd.Parameters.Add("@total", SqlDbType.Float).Value = orderTotal;
                conn.Open();
                cmd.ExecuteNonQuery();
                int orderNumber = (int)cmd2.ExecuteScalar();
                
                for (int j = 0; j < basket.Count; j++)
                {
                    cmd3.Parameters.Clear();
                    cmd3.Parameters.Add("@orderID", SqlDbType.Int).Value = orderNumber;
                    cmd3.Parameters.Add("@stockCode", SqlDbType.VarChar).Value = basket[j].code;
                    cmd3.Parameters.Add("@quantity", SqlDbType.Int).Value = basket[j].quantity;
                    cmd3.ExecuteNonQuery();
                }
                Session["Message"] = "Order successfully placed.";
                Response.Redirect("Customer.aspx");
            }
        }
    }
}