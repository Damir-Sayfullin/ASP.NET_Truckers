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
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}