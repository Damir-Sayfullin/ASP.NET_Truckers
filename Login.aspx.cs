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
    public partial class Login : System.Web.UI.Page
    {
        private string userPost = "";
        private int userID = 0;
        private string userName = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["visibilitydb"] == null)
            {
                button1.Visible = false;
            }
            else
            {
                bool visible = (bool)Session["visibilitydb"];
                button0.Visible = !visible;
                button1.Visible = visible;
            }
            if (Session["responseLabel"] != null)
            {
                responseLabel.InnerHtml = (string)Session["responseLabel"];

                if (Session["userName"] != null)
                {/*
                    userID = (int)Session["userID"];
                    userPost = (string)Session["userPost"];
                    userName = (string)Session["userName"];*/
                    loginField.Disabled = true;
                    passwordField.Disabled = true;
                }
            }
            else
            {
                responseLabel.InnerHtml = "Вы не авторизованы";

                Session["visibilitydb"] = null;
                Session["userID"] = null;
                Session["userPost"] = null;
                Session["userName"] = null;
                loginField.Disabled = false;
                passwordField.Disabled = false;
            }
        }

        protected void Authorize(object sender, EventArgs e)
        {
            button1.Visible = false;
            string login = loginField.Value.ToString();
            string Password = passwordField.Value.ToString();

            // Проверка на ввод логина и пароля //
            if (login == "" || Password == "")
            {
                Session["responseLabel"] = "Введите логин и пароль!";
                Server.TransferRequest("Login.aspx");
                return;
            }

            // Запрос авторизации //
            DataTable response = SqlResponses.GetSqlFromDB("SELECT * FROM Users WHERE Login='" + login + "' AND Password='" + Password + "'");

            if (response.Rows.Count == 0)
            {
                Session["responseLabel"] = "Неверный логин или пароль!";
                Server.TransferRequest("Login.aspx");
                return;
            }

            // Запоминание ID, логина и должности //
            foreach (DataRow dataRow in response.Rows)
            {
                Session["userID"] = (int)dataRow["ID"];
                Session["userPost"] = (string)dataRow["Post"];
                Session["userName"] = (string)dataRow["Username"];

                Session["responseLabel"] = "Вы авторизованы как " + ((string)dataRow["Post"]).ToLower() + " " + (string)dataRow["Username"];

                Session["visibilitydb"] = true;
            }
            Server.TransferRequest("Login.aspx");
        }

        protected void button1_Click(object sender, EventArgs e)
        {
            if (Session["userPost"].ToString() == "Логист")
                Response.Redirect("~/Logist.aspx");
            else if (Session["userPost"].ToString() == "Водитель")
                Response.Redirect("~/Logist.aspx"); // todo: заменить на driver
        }

        protected void button2_Click(object sender, EventArgs e)
        {
            Session["responseLabel"] = null;
            Session["visibilitydb"] = null;
            Session["userID"] = null;
            Session["userPost"] = null;
            Session["nauserName"] = null;
            Response.Redirect("~/Login.aspx");
        }
    }
}