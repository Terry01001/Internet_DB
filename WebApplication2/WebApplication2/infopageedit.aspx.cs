using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication2
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            TextBoxId.Text = Session["ID"].ToString();
        }
        protected void ButtonSubmit_Click(object sender, EventArgs e) 
        {
            string name = TextBoxName.Text;
            string Id = TextBoxId.Text;
            string password = TextBoxPassword.Text;
            string confirmPassword = TextBoxPasswordConfirm.Text;
            string labNumber = TextBoxLabNumber.Text;
            string phone = TextBoxPhone.Text;
            string email = TextBoxEmail.Text;
            string bank = TextBoxBank.Text;

            // 這裡添加代碼來驗證密碼是否匹配，以及其他任何必要的驗證

            // 如果一切驗證通過，則保存數據到資料庫
            if (password == confirmPassword)
            {
                if (StudentExists(Id))
                {
                    // 如果學號/職位編號存在，則更新記錄
                    UpdateStudentInfo(Id, name, password, labNumber, phone, bank, email);
                }
                else if (StaffExists(Id))
                {
                    UpdateStaffInfo(Id, name, password, labNumber, phone, bank, email);
                }
                else
                {
                    // 如果學號/職位編號不存在，則插入新記錄
                    InsertNewStudentOrStaff(Id, name, password, labNumber, phone, bank, email);
                }
                Session["ID"] = Id;
                Session["Password"] = password;
                Session["Name"] = name;
                Session["LabNumber"] = labNumber;
                Session["Phone"] = phone;
                Session["Email"] = email;
                Session["Bank"] = bank;
                Response.Redirect("infopageshow.aspx"); // 請替換為實際的頁面地址
            }
            else
            {
                // 密碼不匹配的錯誤處理
                Response.Write("密碼和確認密碼不匹配");
            }
        }
        private bool StudentExists(string id)
        {
            // 檢查資料庫中是否存在該學號/職位編號
            // 返回 true 如果存在，否則返回 false
            // 實現您的資料庫查詢邏輯...
            string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\\Desktop\\Database11.accdb";
            bool credentialsValid = false;

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                string query1 = "SELECT COUNT(*) FROM 學生 WHERE 學號 = @ID";
                using (OleDbCommand command = new OleDbCommand(query1, connection))
                {
                    command.Parameters.AddWithValue("@ID", id);

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
                        Response.Write($"數據庫學生查詢錯誤: {ex.Message}");
                    }
                }
            }

            return credentialsValid;
        }
        private bool StaffExists(string id)
        {
            // 檢查資料庫中是否存在該學號/職位編號
            // 返回 true 如果存在，否則返回 false
            // 實現您的資料庫查詢邏輯...
            string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\\Desktop\\Database11.accdb";
            bool credentialsValid = false;

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                string query2 = "SELECT COUNT(*) FROM 系辦小姐 WHERE 職位編號 = @ID";
                using (OleDbCommand command = new OleDbCommand(query2, connection))
                {
                    command.Parameters.AddWithValue("@ID", id);

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
                        Response.Write($"數據庫系辦小姐查詢錯誤: {ex.Message}");
                    }
                }
            }

            return credentialsValid;
        }

        private void UpdateStudentInfo(string id, string name, string password, string labNumber, string phone, string bank, string email)
        {
            // 更新資料庫中的學生/系辦小姐資訊
            // 實現您的資料庫更新邏輯...
            string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\\Desktop\\Database11.accdb";

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                string query = "UPDATE 學生 SET 姓名 = @Name, 密碼 = @Password, 實驗室電話 = @LabNumber, 聯絡電話 = @Phone, 郵局賬號 = @Bank, Email = @Email WHERE 學號 = @ID";
                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    command.Parameters.Add("@Name", OleDbType.VarChar).Value = name;
                    command.Parameters.Add("@Password", OleDbType.VarChar).Value = password;
                    command.Parameters.Add("@LabNumber", OleDbType.VarChar).Value = labNumber;
                    command.Parameters.Add("@Phone", OleDbType.VarChar).Value = phone;
                    command.Parameters.Add("@Bank", OleDbType.VarChar).Value = bank;
                    command.Parameters.Add("@Email", OleDbType.VarChar).Value = email;
                    command.Parameters.Add("@ID", OleDbType.VarChar).Value = id;

                    try
                    {
                        connection.Open();
                        int result = command.ExecuteNonQuery();
                    }
                    catch (OleDbException ex)
                    {
                        // 處理錯誤
                        Response.Write($"更新學生資訊錯誤: {ex.Message}");
                    }
                }

                string query2 = "UPDATE 使用者賬密 SET 密碼 = @Password WHERE 賬號 = @ID";
                using (OleDbCommand command = new OleDbCommand(query2, connection))
                {
                    command.Parameters.Add("@Password", OleDbType.VarChar).Value = password;
                    command.Parameters.Add("@ID", OleDbType.VarChar).Value = id;
                    try
                    {
                        //connection.Open();
                        int result = command.ExecuteNonQuery();
                    }
                    catch (OleDbException ex)
                    {
                        // 處理錯誤
                        Response.Write($"更新使用者賬密資訊錯誤: {ex.Message}");
                    }
                }

            }

            
        }
        private void UpdateStaffInfo(string id, string name, string password, string labNumber, string phone, string bank, string email)
        {
            // 更新資料庫中的學生/系辦小姐資訊
            // 實現您的資料庫更新邏輯...
            string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\\Desktop\\Database11.accdb";

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                string query = "UPDATE 系辦小姐 SET 姓名 = @Name, 密碼 = @Password, 辦公室電話 = @LabNumber, 聯絡電話 = @Phone, 郵局賬號 = @Bank, Email = @Email WHERE 職位編號 = @ID";
                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    command.Parameters.Add("@Name", OleDbType.VarChar).Value = name;
                    command.Parameters.Add("@Password", OleDbType.VarChar).Value = password;
                    command.Parameters.Add("@LabNumber", OleDbType.VarChar).Value = labNumber;
                    command.Parameters.Add("@Phone", OleDbType.VarChar).Value = phone;
                    command.Parameters.Add("@Bank", OleDbType.VarChar).Value = bank;
                    command.Parameters.Add("@Email", OleDbType.VarChar).Value = email;
                    command.Parameters.Add("@ID", OleDbType.VarChar).Value = id;

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (OleDbException ex)
                    {
                        // 處理錯誤
                        Response.Write($"更新系辦小姐資訊錯誤: {ex.Message}");
                    }
                }

                string query2 = "UPDATE 使用者賬密 SET 密碼 = @Password WHERE 賬號 = @ID";
                using (OleDbCommand command = new OleDbCommand(query2, connection))
                {
                    command.Parameters.Add("@Password", OleDbType.VarChar).Value = password;
                    command.Parameters.Add("@ID", OleDbType.VarChar).Value = id;
                    try
                    {
                        //connection.Open();
                        int result = command.ExecuteNonQuery();
                    }
                    catch (OleDbException ex)
                    {
                        // 處理錯誤
                        Response.Write($"更新使用者賬密資訊錯誤: {ex.Message}");
                    }
                }
            }
        }

        private void InsertNewStudentOrStaff(string id, string name, string password, string labNumber, string phone, string bank, string email)
        {
            string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\\Desktop\\Database11.accdb";
            string tableName = char.IsLetter(id[0]) ? "學生" : "系辦小姐";
            string columnIdName = tableName == "學生" ? "學號" : "職位編號";
            string LabNumber = char.IsLetter(id[0]) ? "實驗室電話" : "辦公室電話";

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                string query = $"INSERT INTO {tableName} (姓名, 密碼, {LabNumber}, 聯絡電話, Email, {columnIdName}, 郵局賬號, manage) VALUES (@Name, @Password, @LabNumber, @Phone, @Email, @ID, @Bank, @manage)";
                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Password", password);
                    command.Parameters.AddWithValue("@LabNumber", labNumber);
                    command.Parameters.AddWithValue("@Phone", phone);
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@ID", id);
                    command.Parameters.AddWithValue("@Bank", bank);

                    if (char.IsLetter(id[0]))
                    {
                        command.Parameters.AddWithValue("@manage", "common");
                    }
                    else 
                    {
                        command.Parameters.AddWithValue("@manage", "manager");
                        Response.Write("check");
                    }

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (OleDbException ex)
                    {
                        // 處理錯誤
                        Response.Write($"插入新記錄錯誤: {ex.Message}");
                    }
                }

                string query2 = $"UPDATE 使用者賬密 SET 密碼 = @Password WHERE 賬號 = @ID";
                using (OleDbCommand command = new OleDbCommand(query2, connection))
                {

                    command.Parameters.Add("@Password", OleDbType.VarChar).Value = password;
                    command.Parameters.Add("@ID", OleDbType.VarChar).Value = id;
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (OleDbException ex)
                    {
                        // 處理錯誤
                        Response.Write($"插入使用者賬密新記錄錯誤: {ex.Message}");
                    }
                }
            }
        }
    }
}