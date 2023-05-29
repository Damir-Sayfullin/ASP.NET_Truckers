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
                cargoID.Items.Add(new ListItem { Text = dataRow["Cargo"].ToString(), Value = dataRow["ID"].ToString()});
            }
            if (Session["cargoDriverID"] != null)
            {
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
            string currentID = cargoID.Items[cargoID.SelectedIndex].Value;
            DataTable response = SqlResponses.GetSqlFromDB("SELECT * FROM Cargo WHERE ID=" + currentID); 
            foreach (DataRow dataRow in response.Rows)
            {
                Session["cargoDriverID"] = dataRow["DriverID"].ToString();
                Session["cargoStatus"] = dataRow["Status"].ToString();
                Session["cargoName"] = dataRow["Cargo"].ToString();
                Session["cargoWeight"] = dataRow["Weight"].ToString();
                Session["cargoFrom"] = dataRow["From"].ToString();
                Session["cargoTo"] = dataRow["To"].ToString();
            }
            Server.TransferRequest("Logist.aspx");
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
            Response.Redirect("~/Login.aspx");
        }
    }
}