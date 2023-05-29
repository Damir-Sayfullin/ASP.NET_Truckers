using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ASP.NET_Truckers
{
    class SqlResponses
    {
        static string connectionString = @"Provider=Microsoft.ACE.OLEDB.16.0;Data Source = C:/My Files/Универ/3 курс/Технологии программирования/ASP.NET_Truckers/data/TruckersDB.mdb";
        public static void SqlFromDB(string sql)
        {
            OleDbConnection connection = new OleDbConnection(connectionString);
            connection.Open();

            OleDbCommand cmd = new OleDbCommand(sql, connection);
            cmd.ExecuteNonQuery();
        }

        public static DataTable GetSqlFromDB(string sql)
        {
            OleDbConnection connection = new OleDbConnection(connectionString);
            connection.Open();

            DataTable dataTable = new DataTable();
            OleDbDataAdapter adapter = new OleDbDataAdapter(sql, connection);
            adapter.Fill(dataTable);

            return dataTable;
        }
    }

    public partial class Logist : System.Web.UI.Page
    {
        DataSet dst;
        OleDbConnection myCnt;
        OleDbDataAdapter dbAdpt1;

        static string connectionString = @"Provider=Microsoft.ACE.OLEDB.16.0;Data Source = C:/My Files/Универ/3 курс/Технологии программирования/ASP.NET_Truckers/data/TruckersDB.mdb";

        protected void Page_Load(object sender, EventArgs e)
        {
            dst = new DataSet();
            myCnt = new OleDbConnection();

            myCnt.ConnectionString = connectionString;

            string slct1 = "SELECT * From Cargo";
            dbAdpt1 = new OleDbDataAdapter(slct1, myCnt);
            dbAdpt1.Fill(dst, "Cargo");

            GridView1.DataSource = dst.Tables["Cargo"];
            Page.DataBind();

            if (Session["GridVisibility"] == null)
            {
                GridViewShow.Visible = false;
            }
            else
            {
                bool visible = (bool)Session["GridVisibility"];
                GridViewShow.Visible = visible;
            }

            if (Session["responseLabel"] != null)
            {
                responseLabel.InnerHtml = (string)Session["responseLabel"];
                CargoID_Reload();
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }
        }

        public void CargoID_Reload()
        {
            cargoID.Items.Clear();
            foreach (DataRow dataRow in dst.Tables["Cargo"].Rows)
            {
                cargoID.Items.Add(new ListItem { Text = dataRow["ID"].ToString(), Value = dataRow["ID"].ToString()});
            }
            if (Session["cargoDriverID"] != null)
            {
                cargoID.Value = Session["cargoID"].ToString();
                cargoDriverID.Value = Session["cargoDriverID"].ToString();
                cargoStatus.Value = Session["cargoStatus"].ToString();
                cargoName.Value = Session["cargoName"].ToString();
                cargoWeight.Value = Session["cargoWeight"].ToString();
                cargoFrom.Value = Session["cargoFrom"].ToString();
                cargoTo.Value = Session["cargoTo"].ToString();
            }
        }

        public void buttonChoose_Click(object sender, EventArgs e)
        {
            cargoID.Value = Request.Form["cargoID"];
            System.Diagnostics.Debug.WriteLine("ID:" + cargoID.Value.ToString());
            DataTable response = SqlResponses.GetSqlFromDB("SELECT * FROM Cargo WHERE ID=" + cargoID.Value.ToString());
            foreach (DataRow dataRow in response.Rows)
            {
                System.Diagnostics.Debug.WriteLine(dataRow["Cargo"].ToString());
                Session["cargoID"] = dataRow["ID"].ToString();
                Session["cargoDriverID"] = dataRow["DriverID"].ToString();
                Session["cargoStatus"] = dataRow["Status"].ToString();
                Session["cargoName"] = dataRow["Cargo"].ToString();
                Session["cargoWeight"] = dataRow["Weight"].ToString();
                Session["cargoFrom"] = dataRow["From"].ToString();
                Session["cargoTo"] = dataRow["To"].ToString();
            }
            Server.TransferRequest("Logist.aspx");
        }

        /// <summary>
        /// Функция поставки товара.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void NewStock(object sender, EventArgs e)
        {/*
            DataTable response;

            // Получение количества товара у поставщика (при необходимости) //
            int stockCount = 0;
            /*if (!admin)
            {
                response = SqlResponses.GetSqlFromDB("SELECT * FROM Products WHERE ProductId=" + productsForStock.Value + ";");
                if (response.Rows.Count == 0)
                {
                    SqlResponses.SqlFromDB("INSERT INTO Products ([ProductId], [Amount]) VALUES(" + productsForStock.Value + ", 0);");
                }
                else
                {
                    foreach (DataRow dataRow in response.Rows)
                    {
                        stockCount += (int)dataRow["Amount"];
                    }
                }
            }

            // Получение количества товара у авторизированного пользователя //
            int count = 0;
            response = SqlResponses.GetSqlFromDB("SELECT * FROM Products WHERE ProductId=" + productsForStock.Value + ";");
            if (response.Rows.Count == 0)
            {
                SqlResponses.SqlFromDB("INSERT INTO Products ([ProductId], [Amount]) VALUES (" + productsForStock.Value + ", 0);");
            }
            else
            {
                foreach (DataRow dataRow in response.Rows)
                {
                    count = (int)dataRow["Amount"];
                }
            }

           /* if (!admin)
            {
                if (stockCount > 0 && stockCount - Convert.ToInt32(productCount.Value) >= 0)
                {
                    // Прибавление количества товара указанного в форме поставки если достаточно количества на складе //
                    SqlResponses.SqlFromDB("UPDATE Products SET [Amount]=" + (stockCount - Convert.ToInt32(productCount.Value)) + " WHERE ProductId=" + productsForStock.Value + ";");
                    SqlResponses.SqlFromDB("INSERT INTO Orders ([ProductID], [DeliveryDate], [OrderedAmount]) VALUES (" + productsForStock.Value + ", '" + dateTimePicker.Value.ToString() + "', " + Convert.ToInt32(productCount.Value) + ");");
                    Session["orderResponse"] = "Товар: " + productsForStock.Items.FindByValue(productsForStock.Value).ToString() + " в размере " + productCount.Value + " шт. будет доставлен " + dateTimePicker.Value;

                    Session["productCount"] = 0.ToString();
                    //DefaultThirdFrame(false);
                }
                else
                {
                    // Прибавление количества товара указанного в форме поставки если не достаточно количества на складе //
                    Session["orderResponse"] = "Необходимое количество товара отсутствует. Нажмите кнопку 'Уведомить магазин' для оповещения.";
                    button1.Visible = true;
                    Session["notification"] = ("INSERT INTO Notifications ([Product], [OrderedAmount]) VALUES " + "('" + productsForStock.Items.FindByValue(productsForStock.Value).ToString() + "', " + productCount.Value + ");");
                    Session["visibility"] = true;
                }
            }
            else
            {
                // Прибавление количества товара указанного в форме поставки //

                string rr = ("UPDATE Products SET [Amount]=" + (count + Convert.ToInt32(productCount.Value)) + " WHERE ProductId=" + productsForStock.Value + ";");
                SqlResponses.SqlFromDB("UPDATE Products SET [Amount]=" + (count + Convert.ToInt32(productCount.Value)) + " WHERE ProductId=" + productsForStock.Value + ";");
                Session["productCount"] = 0.ToString();
                Session["orderResponse"] = "\nТовар: " + productsForStock.Items.FindByValue(productsForStock.Value).ToString() + " в количестве " + productCount.Value + " шт. будет увеличен на складе";
            }

            Server.TransferRequest("Database.aspx");

            //Session["orderResponse"] = null;
            Session["productCount"] = null;
            Session["productsForStock"] = null;

            productsForStock.Value = Request.Form["productsForStock"];
            Session["productsForStock"] = Request.Form["productsForStock"];
            productCount.Value = Request.Form["productCount"];
            //Session["orderResponse"] = null;*/
        }

        protected void SendNotification(object sender, EventArgs e)
        {
            /*
            ScriptManager.RegisterStartupScript(this, GetType(), "ShowMessage", "showMessage();", true);
            //button1.Visible = false;
            string notification = Session["notification"].ToString();
            SqlResponses.SqlFromDB(notification);
            Session["visibility"] = false;
            Session["orderResponse"] = "";*/
        }
        protected void OpenAllNotifications(object sender, EventArgs e)
        {/*
            DataTable notificatons = SqlResponses.GetSqlFromDB("SELECT * FROM Notifications");
            StringBuilder sb = new StringBuilder();

            foreach (DataRow dataRow in notificatons.Rows)
            {
                string Product = dataRow["Product"].ToString();
                string OrderedAmount = dataRow["OrderedAmount"].ToString();
                string notif = ("Товар: " + Product + " был заказан в количестве - " + OrderedAmount + " шт. Указанный объем на складе отсутсвует. <br>");
                sb.AppendLine(notif);
            }
            notifi.InnerHtml = sb.ToString();*/
        }
        /*
        protected void DeleteAllNotifications(object sender, EventArgs e)
        {
            SqlResponses.SqlFromDB("DELETE FROM Notifications");
            ScriptManager.RegisterStartupScript(this, GetType(), "ShowMessage", "notificationsDeleted();", true);
            Session["visible_notifations"] = false;
        }*/

        protected void gridshow_Click(object sender, EventArgs e)
        {
            Session["GridVisibility"] = true;
            Server.TransferRequest("Logist.aspx");
        }

        protected void gridhide_Click(object sender, EventArgs e)
        {
            Session["GridVisibility"] = false;
            Server.TransferRequest("Logist.aspx");
        }

        protected void buttonExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Login.aspx");
        }
    }
}