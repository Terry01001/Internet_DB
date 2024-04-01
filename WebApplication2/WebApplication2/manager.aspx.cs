using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication2
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
            }
        }

        private void BindData()
        {
            string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\\Desktop\\Database11.accdb";
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT 賬號, 密碼, manage FROM 使用者賬密", conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                MyGridView.DataSource = dt;
                MyGridView.DataBind();
            }
        }

        protected void MyGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            // 設置要編輯的行索引
            MyGridView.EditIndex = e.NewEditIndex;

            // 重新綁定數據以更新 GridView
            BindData();
        }

        protected void MyGridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            // 從 GridView 中獲取更新後的數據
            string username = e.Keys["賬號"].ToString();
            string password = e.NewValues["密碼"].ToString();
            string manage = e.NewValues["manage"].ToString();

            // 更新資料庫的邏輯
            string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\\Desktop\\Database11.accdb";
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                OleDbCommand cmd = new OleDbCommand("UPDATE 使用者賬密 SET 密碼 = @Password, manage = @Manage WHERE 賬號 = @Username", conn);
                cmd.Parameters.AddWithValue("@Password", password);
                cmd.Parameters.AddWithValue("@Manage", manage);
                cmd.Parameters.AddWithValue("@Username", username);

                conn.Open();
                cmd.ExecuteNonQuery();

                // update the other table
                string tableName = char.IsLetter(username[0]) ? "學生" : "系辦小姐";
                string id = char.IsLetter(username[0]) ? "學號" : "職位編號";
                OleDbCommand cmd2 = new OleDbCommand($"UPDATE {tableName} SET 密碼 = @Password, manage = @Manage WHERE {id} = @Username", conn);
                cmd2.Parameters.Add("@Password", OleDbType.VarChar).Value = password;
                cmd2.Parameters.Add("@Manage", OleDbType.VarChar).Value = manage;
                cmd2.Parameters.Add("@Username", OleDbType.VarChar).Value = username;

                cmd2.ExecuteNonQuery();
            }

            // 重置編輯索引並重新綁定數據
            MyGridView.EditIndex = -1;
            BindData();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("infopageshow.aspx");
        }

        protected void MyGridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            // 重置编辑索引
            MyGridView.EditIndex = -1;

            // 重新绑定数据以显示原始数据
            BindData();
        }

    }
}