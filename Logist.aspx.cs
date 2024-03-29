﻿using System;
using System.Data;
using System.Data.OleDb;
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
            connection.Close();
        }

        public static DataTable GetSqlFromDB(string sql)
        {
            OleDbConnection connection = new OleDbConnection(connectionString);
            connection.Open();
            DataTable dataTable = new DataTable();
            OleDbDataAdapter adapter = new OleDbDataAdapter(sql, connection);
            adapter.Fill(dataTable);
            connection.Close();
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

            // проверка на авторизацию
            if (Session["userPost"] != null && Session["userPost"].ToString() == "Логист")
            {
                responseLabel.InnerHtml = (string)Session["responseLabel"];
                if (!IsPostBack)
                    CargoID_Reload();
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }

            // отображение таблицы
            if (Session["GridVisibility"] == null)
            {
                GridViewShow.Visible = false;
            }
            else
            {
                bool visible = (bool)Session["GridVisibility"];
                GridViewShow.Visible = visible;
            }

            // отображение сообщения об ошибке
            if (Session["ErrorMessage"] == null)
            {
                errorMessage.Visible = false;
            }
            else
            {
                errorMessage.InnerText = Session["ErrorMessage"].ToString();
                errorMessage.Visible = true;
            }
        }

        public void CargoID_Reload()
        {
            cargoID.Items.Clear();
            foreach (DataRow dataRow in dst.Tables["Cargo"].Rows)
            {
                cargoID.Items.Add(new ListItem { Text = dataRow["ID"].ToString(), Value = dataRow["ID"].ToString() });
            }
            if (Session["cargoDriverID"] == null) // при первой загрузке страницы
            {
                DataTable response = SqlResponses.GetSqlFromDB("SELECT * FROM Cargo WHERE ID=" + cargoID.SelectedValue.ToString());
                foreach (DataRow dataRow in response.Rows)
                {
                    Session["cargoID"] = cargoID.SelectedValue.ToString();
                    Session["cargoDriverID"] = dataRow["DriverID"].ToString();
                    Session["cargoStatus"] = dataRow["Status"].ToString();
                    Session["cargoName"] = dataRow["Cargo"].ToString();
                    Session["cargoWeight"] = dataRow["Weight"].ToString();
                    Session["cargoFrom"] = dataRow["From"].ToString();
                    Session["cargoTo"] = dataRow["To"].ToString();
                }
            }
            cargoID.SelectedValue = Session["cargoID"].ToString();
            cargoDriverID.Value = Session["cargoDriverID"].ToString();
            cargoStatus.Value = Session["cargoStatus"].ToString();
            cargoName.Value = Session["cargoName"].ToString();
            cargoWeight.Value = Session["cargoWeight"].ToString();
            cargoFrom.Value = Session["cargoFrom"].ToString();
            cargoTo.Value = Session["cargoTo"].ToString();
        }

        public void cargoID_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable response = SqlResponses.GetSqlFromDB("SELECT * FROM Cargo WHERE ID=" + cargoID.SelectedValue.ToString());
            foreach (DataRow dataRow in response.Rows)
            {
                Session["cargoID"] = cargoID.SelectedValue.ToString();
                Session["cargoDriverID"] = dataRow["DriverID"].ToString();
                Session["cargoStatus"] = dataRow["Status"].ToString();
                Session["cargoName"] = dataRow["Cargo"].ToString();
                Session["cargoWeight"] = dataRow["Weight"].ToString();
                Session["cargoFrom"] = dataRow["From"].ToString();
                Session["cargoTo"] = dataRow["To"].ToString();
            }
            Server.TransferRequest("Logist.aspx");
        }

        protected void buttonReload_Click(object sender, EventArgs e)
        {
            Session["ErrorMessage"] = null;
            Server.TransferRequest("Logist.aspx");
        }

        protected void buttonSave_Click(object sender, EventArgs e)
        {
            if (cargoID.SelectedValue != "" && cargoDriverID.Value != "" && cargoStatus.Value != "" && cargoName.Value != "" && cargoWeight.Value != "" && cargoFrom.Value != "" && cargoTo.Value != "")
            {
                DataTable response = SqlResponses.GetSqlFromDB(string.Format("SELECT * FROM Cargo WHERE DriverID = {0}", cargoDriverID.Value.ToString()));
                if (cargoDriverID.Value == "0" || response.Rows.Count == 0 || cargoDriverID.Value == (string)Session["cargoDriverID"])
                {
                    SqlResponses.SqlFromDB(string.Format("UPDATE Cargo SET DriverID={1}, Status='{2}', Cargo='{3}', Weight={4}, [From]='{5}', [To]='{6}' WHERE ID={0}",
                        cargoID.SelectedValue.ToString(), cargoDriverID.Value.ToString(), cargoStatus.Value.ToString(),
                        cargoName.Value.ToString(), cargoWeight.Value.ToString(), cargoFrom.Value.ToString(), cargoTo.Value.ToString()));
                    Session["ErrorMessage"] = "УСПЕХ: Данные у груза с ID=" + cargoID.SelectedValue.ToString() + " обновлены";

                    DataTable response2 = SqlResponses.GetSqlFromDB("SELECT * FROM Cargo WHERE ID=" + cargoID.SelectedValue.ToString());
                    foreach (DataRow dataRow in response2.Rows)
                    {
                        Session["cargoID"] = cargoID.SelectedValue.ToString();
                        Session["cargoDriverID"] = dataRow["DriverID"].ToString();
                        Session["cargoStatus"] = dataRow["Status"].ToString();
                        Session["cargoName"] = dataRow["Cargo"].ToString();
                        Session["cargoWeight"] = dataRow["Weight"].ToString();
                        Session["cargoFrom"] = dataRow["From"].ToString();
                        Session["cargoTo"] = dataRow["To"].ToString();
                    }
                }
                else
                    Session["ErrorMessage"] = "ОШИБКА: У водителя с ID=" + cargoDriverID.Value.ToString() + " уже есть груз!";
                Server.TransferRequest("Logist.aspx");
            }
            else
            {
                Session["ErrorMessage"] = "ОШИБКА: Не все поля были заполнены!";
                Server.TransferRequest("Logist.aspx");
            }
        }

        protected void buttonAdd_Click(object sender, EventArgs e)
        {
            if (cargoID.SelectedValue != "" && cargoDriverID.Value != "" && cargoStatus.Value != "" && cargoName.Value != "" && cargoWeight.Value != "" && cargoFrom.Value != "" && cargoTo.Value != "")
            {
                DataTable response = SqlResponses.GetSqlFromDB(string.Format("SELECT * FROM Cargo WHERE DriverID = {0}", cargoDriverID.Value.ToString()));
                if (cargoDriverID.Value == "0" || response.Rows.Count == 0)
                {

                    SqlResponses.SqlFromDB(string.Format("INSERT INTO Cargo (DriverID, Status, Cargo, Weight, [From], [To]) VALUES ({0}, '{1}', '{2}', {3}, '{4}', '{5}')", 
                        cargoDriverID.Value, cargoStatus.Value, cargoName.Value, cargoWeight.Value, cargoFrom.Value, cargoTo.Value));
                    Session["ErrorMessage"] = "УСПЕХ: Груз \"" + cargoName.Value.ToString() + "\" успешно добавлен";

                    Session["cargoID"] = null;
                    Session["cargoDriverID"] = null;
                    Session["cargoStatus"] = null;
                    Session["cargoName"] = null;
                    Session["cargoWeight"] = null;
                    Session["cargoFrom"] = null;
                    Session["cargoTo"] = null;
                }
                else
                    Session["ErrorMessage"] = "ОШИБКА: У водителя с ID=" + cargoDriverID.Value.ToString() + " уже есть груз!";
                Server.TransferRequest("Logist.aspx");
            }
            else
            {
                Session["ErrorMessage"] = "ОШИБКА: Не все поля были заполнены!";
                Server.TransferRequest("Logist.aspx");
            }
        }

        protected void buttonDelete_Click(object sender, EventArgs e)
        {
            if (cargoID.SelectedValue != null)
            {
                DataTable response = SqlResponses.GetSqlFromDB(string.Format("SELECT * FROM Cargo WHERE ID = {0}", cargoID.SelectedValue.ToString()));
                foreach (DataRow dataRow in response.Rows)
                {
                    if (dataRow["DriverID"].ToString() == "0")
                    {
                        SqlResponses.SqlFromDB(string.Format("DELETE FROM Cargo WHERE ID = {0}", cargoID.SelectedValue.ToString()));
                        Session["ErrorMessage"] = "УСПЕХ: Груз с ID=" + cargoID.SelectedValue.ToString() + " успешно удален";

                        Session["cargoID"] = null;
                        Session["cargoDriverID"] = null;
                        Session["cargoStatus"] = null;
                        Session["cargoName"] = null;
                        Session["cargoWeight"] = null;
                        Session["cargoFrom"] = null;
                        Session["cargoTo"] = null;
                    }
                    else
                    {
                       Session["ErrorMessage"] = "ОШИБКА: Невозможно удалить груз, так как он занят водителем с ID=" + dataRow["DriverID"].ToString() + "!";
                    }
                }
                Server.TransferRequest("Logist.aspx");
            }
            else
            {
                Session["ErrorMessage"] = "ОШИБКА: Не выбран ID!";
                Server.TransferRequest("Logist.aspx");
            }
        }

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
            Session["ErrorMessage"] = null;
            Response.Redirect("~/Login.aspx");
        }
    }
}