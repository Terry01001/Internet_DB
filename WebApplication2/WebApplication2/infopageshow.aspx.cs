using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication2
{
    public partial class WebForm3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // 在這裡從Session讀取資料
                LabelName.Text = Session["Name"] as string;
                LabelId.Text = Session["ID"] as string;
                LabelLabNumber.Text = Session["LabNumber"] as string;
                LabelPhone.Text = Session["Phone"] as string;
                LabelEmail.Text = Session["Email"] as string;
                LabelBank.Text = Session["Bank"] as string;
            }
            if (!IsPostBack)
            {
                string id = Session["ID"] as string;
                LabelId.Text = id;

                if (char.IsLetter(id[0]))
                {
                    // 如果ID以字母開頭，從學生表中獲取資料
                    FetchDataFromStudentTable(id);
                }
                else
                {
                    // 否則，從系辦小姐表中獲取資料
                    FetchDataFromStaffTable(id);
                }
            
            }
        }
        private void FetchDataFromStudentTable(string id)
        {
            // 實現從學生表中獲取資料的邏輯
            // 更新Label控件的文本
            // 例如：LabelName.Text = 獲取到的名字;
            string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\\Desktop\\Database11.accdb";
            
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                string query = "SELECT 姓名, 實驗室電話, 聯絡電話, Email, 郵局賬號 FROM 學生 WHERE 學號 = @ID";
                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", id);

                    try
                    {
                        connection.Open();
                        using (OleDbDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                LabelName.Text = reader["姓名"].ToString();
                                LabelLabNumber.Text = reader["實驗室電話"].ToString();
                                LabelPhone.Text = reader["聯絡電話"].ToString();
                                LabelEmail.Text = reader["Email"].ToString();
                                LabelBank.Text = reader["郵局賬號"].ToString();
                            }
                        }
                    }
                    catch (OleDbException ex)
                    {
                        // 處理錯誤
                        Response.Write($"數據庫查詢錯誤: {ex.Message}");
                    }
                }
            }
            
            
        }

        private void FetchDataFromStaffTable(string id)
        {
            // 實現從系辦小姐表中獲取資料的邏輯
            // 更新Label控件的文本
            // 例如：LabelName.Text = 獲取到的名字;
            string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\\Desktop\\Database11.accdb";

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                string query = "SELECT 姓名, 辦公室電話, 聯絡電話, 郵局賬號, Email FROM 系辦小姐 WHERE 職位編號 = @ID";
                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", id);

                    try
                    {
                        connection.Open();
                        using (OleDbDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                LabelName.Text = reader["姓名"].ToString();
                                LabelLabNumber.Text = reader["辦公室電話"].ToString();
                                LabelPhone.Text = reader["聯絡電話"].ToString();
                                LabelEmail.Text = reader["Email"].ToString();
                                LabelBank.Text = reader["郵局賬號"].ToString();
                            }
                        }
                    }
                    catch (OleDbException ex)
                    {
                        Response.Write($"數據庫查詢錯誤: {ex.Message}");
                    }
                }
            }
        }

        protected void Buttonedit_Click(object sender, EventArgs e)
        {
            Response.Redirect("infopageedit.aspx");
        }

        protected void Buttoneditschedule_Click(object sender, EventArgs e)
        {
            Response.Redirect("scheduleedit.aspx");
        }

        protected void Buttonschedule_Click(object sender, EventArgs e)
        {
            Response.Redirect("scheduleshow.aspx");
        }

        protected void Buttonmanage_Click(object sender, EventArgs e)
        {
            string id = LabelId.Text;
            if (checkmanagestatus(id))
            {
                Response.Redirect("manager.aspx");
            }
            else 
            {
                string message = "alert('你不是管理員,沒有權限權限瀏覽此畫面');";
                ClientScript.RegisterStartupScript(this.GetType(), "AlertMessage", message, true);
            }
        }
        private bool checkmanagestatus(string id)
        {
            string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\\Desktop\\Database11.accdb";
            bool managestatus = false; // 非管理員

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                string query = "SELECT manage FROM 使用者賬密 WHERE 賬號 = @ID";
                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", id);

                    try
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();
                        if (result.ToString() == "manager")
                        {
                            managestatus = true;
                        }
                    }
                    catch (OleDbException ex)
                    {
                        Response.Write($"數據庫查詢錯誤: {ex.Message}");
                    }
                }
            }

            return managestatus;
        }

        protected void Buttonout_Click(object sender, EventArgs e)
        {
            Response.Redirect("login.aspx");
        }
    }
}