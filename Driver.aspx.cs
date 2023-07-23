using System;
using System.Data;
using System.Data.OleDb;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ASP.NET_Truckers
{
    public partial class Driver : System.Web.UI.Page
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
            if (Session["userPost"] != null && Session["userPost"].ToString() == "Водитель")
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
            Server.TransferRequest("Driver.aspx");
        }

        protected void buttonCurrent_Click(object sender, EventArgs e)
        {
            DataTable response = SqlResponses.GetSqlFromDB(string.Format("SELECT * FROM Cargo WHERE DriverID = {0}", Session["userID"].ToString()));
            if (response.Rows.Count == 1)
            {
                foreach (DataRow dataRow in response.Rows)
                {
                    Session["cargoID"] = dataRow["ID"].ToString();
                    Session["cargoDriverID"] = dataRow["DriverID"].ToString();
                    Session["cargoStatus"] = dataRow["Status"].ToString();
                    Session["cargoName"] = dataRow["Cargo"].ToString();
                    Session["cargoWeight"] = dataRow["Weight"].ToString();
                    Session["cargoFrom"] = dataRow["From"].ToString();
                    Session["cargoTo"] = dataRow["To"].ToString();
                    Session["ErrorMessage"] = "УСПЕХ: У вас есть активный груз с ID=" + dataRow["ID"].ToString(); ;
                }
            }
            else
                Session["ErrorMessage"] = "ОШИБКА: У вас нет активного груза!";
            Server.TransferRequest("Driver.aspx");
        }

        protected void buttonAccept_Click(object sender, EventArgs e)
        {
            // проверка на наличие груза у водителя
            DataTable response = SqlResponses.GetSqlFromDB(string.Format("SELECT * FROM Cargo WHERE DriverID = {0}", Session["userID"].ToString()));
            if (response.Rows.Count == 0)
            {
                // проверка на наличие водителей у выбранного груза
                DataTable response2 = SqlResponses.GetSqlFromDB(string.Format("SELECT * FROM Cargo WHERE ID = {0}", cargoID.SelectedValue.ToString()));
                if (response2.Rows[0]["DriverID"].ToString() == "0")
                {
                    // проверка статуса груза
                    DataTable response3 = SqlResponses.GetSqlFromDB(string.Format("SELECT * FROM Cargo WHERE ID = {0}", cargoID.SelectedValue.ToString()));
                    if (response3.Rows[0]["Status"].ToString() == "ready for unloading")
                    {
                        SqlResponses.SqlFromDB(string.Format("UPDATE Cargo SET DriverID={0}, Status='on the way' WHERE ID={1}", Session["userID"].ToString(), cargoID.SelectedValue.ToString()));
                        Session["ErrorMessage"] = string.Format("УСПЕХ: Груз принят! ID={0} Груз: {1} из {2} в {3}", cargoID.SelectedValue.ToString(), response3.Rows[0]["Cargo"].ToString(), response3.Rows[0]["From"].ToString(), response3.Rows[0]["To"].ToString());
                    }
                    else
                        Session["ErrorMessage"] = "ОШИБКА: Этот груз уже был доставлен!";
                }
                else
                    Session["ErrorMessage"] = "ОШИБКА: У этого груза уже есть водитель!";
            }
            else
                Session["ErrorMessage"] = "ОШИБКА: У вас уже есть активный груз!";
            Server.TransferRequest("Driver.aspx");
        }

        protected void buttonCancel_Click(object sender, EventArgs e)
        {
            DataTable response = SqlResponses.GetSqlFromDB(string.Format("SELECT * FROM Cargo WHERE DriverID = {0}", Session["userID"].ToString()));
            if (response.Rows.Count == 1)
            {
                SqlResponses.SqlFromDB(string.Format("UPDATE Cargo SET DriverID=0, Status='ready for unloading' WHERE DriverID={0}", Session["userID"].ToString()));
                Session["ErrorMessage"] = string.Format("УСПЕХ: Ваш груз c ID={0} отменен!", response.Rows[0]["ID"].ToString());
            }
            else
                Session["ErrorMessage"] = "ОШИБКА: У вас нет активного груза!";
            Server.TransferRequest("Driver.aspx");
        }

        protected void buttonDelivery_Click(object sender, EventArgs e)
        {
            DataTable response = SqlResponses.GetSqlFromDB(string.Format("SELECT * FROM Cargo WHERE DriverID = {0}", Session["userID"].ToString()));
            if (response.Rows.Count == 1)
            {
                SqlResponses.SqlFromDB(string.Format("UPDATE Cargo SET DriverID=0, Status='delivered' WHERE DriverID={0}", Session["userID"].ToString()));
                Session["ErrorMessage"] = string.Format("УСПЕХ: Ваш груз c ID={0} успешно доставлен!", response.Rows[0]["ID"].ToString());
            }
            else
                Session["ErrorMessage"] = "ОШИБКА: У вас нет активного груза!";
            Server.TransferRequest("Driver.aspx");
        }

        protected void gridshow_Click(object sender, EventArgs e)
        {
            Session["GridVisibility"] = true;
            Session["ErrorMessage"] = null;
            Server.TransferRequest("Driver.aspx");
        }

        protected void gridhide_Click(object sender, EventArgs e)
        {
            Session["GridVisibility"] = false;
            Session["ErrorMessage"] = null;
            Server.TransferRequest("Driver.aspx");
        }

        protected void buttonExit_Click(object sender, EventArgs e)
        {
            Session["ErrorMessage"] = null;
            Response.Redirect("~/Login.aspx");
        }
    }
}