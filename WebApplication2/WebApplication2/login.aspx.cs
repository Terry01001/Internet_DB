using System;
using System.Net.Mail;

using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;

namespace WebApplication1
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //string studentId = TextBox2.Text; // 學號從 TextBox2 獲取
            //string password = GenerateRandomPassword(8); // 生成8位隨機密碼
            //string email = $"{studentId}@student.nsysu.edu.tw"; // 拼接郵箱地址

            //SendPasswordEmail(email, password);
            //SavePasswordToDatabase(studentId, password); // 儲存到資料庫
            string Id = TextBox2.Text; 
            string inputPassword = TextBox1.Text; 

            if (string.IsNullOrWhiteSpace(inputPassword))
            {
                // 如果沒有輸入密碼，則生成新密碼並發送郵件
                string newPassword = GenerateRandomPassword(8);
                SendPasswordEmail($"{Id}@student.nsysu.edu.tw", newPassword);
                SavePasswordToDatabase(Id, newPassword); // 儲存到資料庫
            }
            else
            {
                // 如果輸入了賬號和密碼，檢查是否正確
                if (CheckCredentials(Id, inputPassword))
                {
                    // 如果賬號和密碼正確，則跳轉到填寫個人資料的頁面
                    Session["ID"] = Id;
                    if (IsPostalAccountEmpty(Id))
                    {
                        
                        Response.Redirect("infopageedit.aspx"); 
                    }
                    else
                    {
                        Response.Redirect("infopageshow.aspx"); 
                    }
                }
                else
                {
                    // 如果賬號或密碼錯誤，則顯示錯誤信息
                    string message = "alert('賬號或密碼輸入錯誤');";
                    ClientScript.RegisterStartupScript(this.GetType(), "AlertMessage", message, true);
                }
            }
        }
        private bool IsPostalAccountEmpty(string id) 
        {
            string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\\Desktop\\Database11.accdb";
            bool isPostalAccountEmpty = true; // 預設為空

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                string query = "SELECT 郵局賬號 FROM 學生 WHERE 學號 = @ID";
                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", id);

                    try
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();
                        if (result != null && !string.IsNullOrWhiteSpace(result.ToString()))
                        {
                            // 郵局帳號不為空
                            isPostalAccountEmpty = false;
                        }
                    }
                    catch (OleDbException ex)
                    {
                        // 處理錯誤
                        Response.Write($"數據庫查詢錯誤: {ex.Message}");
                    }
                }

                string query2 = "SELECT 郵局賬號 FROM 系辦小姐 WHERE 職位編號 = @ID";
                using (OleDbCommand command = new OleDbCommand(query2, connection))
                {
                    command.Parameters.AddWithValue("@ID", id);

                    try
                    {
                        object result = command.ExecuteScalar();
                        if (result != null && !string.IsNullOrWhiteSpace(result.ToString()))
                        {
                            // 郵局帳號不為空
                            isPostalAccountEmpty = false;
                        }
                    }
                    catch (OleDbException ex)
                    {
                        // 處理錯誤
                        Response.Write($"數據庫查詢錯誤: {ex.Message}");
                    }
                }
            }

            return isPostalAccountEmpty;
        }
        private bool CheckCredentials(string Id, string password)
        {
            string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\\Desktop\\Database11.accdb";
            bool credentialsValid = false;

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM 使用者賬密 WHERE 賬號 = @ID AND 密碼 = @Password";
                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", Id);
                    command.Parameters.AddWithValue("@Password", password);

                    try
                    {
                        connection.Open();
                        int userCount = (int)command.ExecuteScalar();
                        if (userCount > 0)
                        {
                            credentialsValid = true;
                        }
                    }
                    catch (OleDbException ex)
                    {
                        // 處理錯誤
                        Response.Write($"數據庫查詢錯誤: {ex.Message}");
                    }
                }
            }

            return credentialsValid;
        }

        private string GenerateRandomPassword(int length)
        {
            var random = new Random();
            string password = "";
            for (int i = 0; i < length; i++)
            {
                password += random.Next(10).ToString();
            }
            return password;
        }
        private void SavePasswordToDatabase(string id, string password)
        {
            string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\\Desktop\\Database11.accdb";
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                string query = "INSERT INTO 使用者賬密 (賬號, 密碼, manage) VALUES (@ID, @Password, @manage)";
                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", id);
                    command.Parameters.AddWithValue("@Password", password);

                    if (char.IsLetter(id[0]))
                    {
                        command.Parameters.AddWithValue("@manage", "common");
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@manage", "manager");
                    }

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (OleDbException ex)
                    {
                        // 處理錯誤
                        Response.Write($"數據庫錯誤: {ex.Message}");
                    }
                }
            }
        }
        private void SendPasswordEmail(string email, string password)
        {
            // 設置郵件服務器，這裡需要您自己的SMTP服務器設置
            //SmtpClient smtpClient = new SmtpClient("smtp.example.com");
            //smtpClient.Credentials = new System.Net.NetworkCredential("username", "password");
            //smtpClient.EnableSsl = true; // 如果SMTP服務器需要SSL加密

            // 創建郵件
            //MailMessage mail = new MailMessage();
            //mail.From = new MailAddress("noreply@example.com");
            //mail.To.Add(email);
            //mail.Subject = "您的初始密碼";
            //mail.Body = $"您的初始密碼為: {password}";
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");

            // 使用Gmail地址和應用程序密碼
            smtpClient.Credentials = new System.Net.NetworkCredential("cyufeng778@gmail.com", "hmom berf jrjc yehs");

            // Gmail要求使用SSL
            smtpClient.EnableSsl = true;
            smtpClient.Port = 587; // Gmail使用的TLS端口

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("cyufeng778@gmail.com");
            mail.To.Add(email);
            mail.Subject = "您的初始密碼";
            mail.Body = $"您的初始密碼為: {password}";

            // 發送郵件
            try
            {
                smtpClient.Send(mail);
            }
            catch (Exception ex)
            {
                Response.Write($"郵件發送失敗: {ex.ToString()}");
            }
        }
    }
}