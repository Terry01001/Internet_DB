using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication2
{
    public partial class WebForm5 : System.Web.UI.Page
    {
        private DateTime CurrentWeekStart
        {
            get { return ViewState["CurrentWeekStart"] != null ? (DateTime)ViewState["CurrentWeekStart"] : DateTime.Now.StartOfWeek2(DayOfWeek.Sunday); }
            set { ViewState["CurrentWeekStart"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindScheduleData();
            }
        }

        private void BindScheduleData()
        {
            var scheduleData = GetWeeklySchedule(CurrentWeekStart); 
            ScheduleGridView2.DataSource = scheduleData;
            ScheduleGridView2.DataBind();
        }
        protected void btnSubmitChanges_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in ScheduleGridView2.Rows)
            {
                string date = ((Label)row.FindControl("lblDate")).Text; // 假設有一個 lblDate 用於顯示日期
                TextBox tbMorning = (TextBox)row.FindControl("tbMorningShift");
                TextBox tbAfternoon = (TextBox)row.FindControl("tbAfternoonShift");

                if (tbMorning.Visible && !string.IsNullOrWhiteSpace(tbMorning.Text))
                {
                    InsertScheduleData(date, "AM", tbMorning.Text);
                }
                if (tbAfternoon.Visible && !string.IsNullOrWhiteSpace(tbAfternoon.Text))
                {
                    InsertScheduleData(date, "PM", tbAfternoon.Text);
                }
            }

            BindScheduleData(); // 重新綁定數據以顯示更新
        }
        private void InsertScheduleData(string date, string shift, string studentId)
        {
            string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\\Desktop\\Database11.accdb";
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                string cmdText = "INSERT INTO 工作時間 (學號, 工作日期, 時段) VALUES (?, ?, ?)";
                using (OleDbCommand cmd = new OleDbCommand(cmdText, conn))
                {
                    cmd.Parameters.AddWithValue("?", studentId);
                    cmd.Parameters.AddWithValue("?", date);
                    cmd.Parameters.AddWithValue("?", shift);
                    
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }



        private List<Schedule2> GetWeeklySchedule(DateTime startDate)
        {
            // get data from db
            DataTable dt = GetScheduleDataFromDatabase(startDate, startDate.AddDays(7));

            // create a list including a week
            var scheduleData = Enumerable.Range(0, 7)
                .Select(i => startDate.AddDays(i))
                .Select(date => new Schedule2
                {
                    Date = date.ToString("yyyy-MM-dd"),
                    DayOfWeek = date.DayOfWeek.ToString(),
                    //如果是周六或周日，show 公休
                    MorningShift = (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday) ? "公休" : "",
                    AfternoonShift = (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday) ? "公休" : ""
                }).ToList();

            // fill data loaded from db
            foreach (DataRow row in dt.Rows)
            {
                // find corresponding date
                var date = Convert.ToDateTime(row["工作日期"]).ToString("yyyy-MM-dd");
                var schedule = scheduleData.FirstOrDefault(s => s.Date == date);

                if (schedule != null && !(schedule.DayOfWeek == "Saturday" || schedule.DayOfWeek == "Sunday"))
                {
                    // 如果數據庫有數據且不是週末，填入對應的班次
                    schedule.MorningShift = row["時段"].ToString() == "AM" ? row["學號"].ToString() : schedule.MorningShift;
                    schedule.AfternoonShift = row["時段"].ToString() == "PM" ? row["學號"].ToString() : schedule.AfternoonShift;
                }
            }

            return scheduleData;
        }

        private DataTable GetScheduleDataFromDatabase(DateTime startDate, DateTime endDate)
        {
            DataTable dt = new DataTable();

            string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\\Desktop\\Database11.accdb";
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                string query = @"SELECT * FROM 工作時間 WHERE 工作日期 >= ? AND 工作日期 < ?";
                using (OleDbCommand cmd = new OleDbCommand(query, conn))
                {
                    cmd.Parameters.Add(new OleDbParameter("?", OleDbType.Date)).Value = startDate;
                    cmd.Parameters.Add(new OleDbParameter("?", OleDbType.Date)).Value = endDate;

                    OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                    da.Fill(dt);
                }
            }

            return dt;
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("infopageshow.aspx");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            // 更新至上一周
            CurrentWeekStart = CurrentWeekStart.AddDays(-7);
            BindScheduleData();
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            // 更新至下一周
            CurrentWeekStart = CurrentWeekStart.AddDays(7);
            BindScheduleData();
        }
    }
    public class Schedule2
    {
        public string Date { get; set; }
        public string DayOfWeek { get; set; }
        public string MorningShift { get; set; }
        public string AfternoonShift { get; set; }
    }


    // 一個幫助方法來獲取一周的開始日期
    public static class DateTimeExtensions2
    {
        public static DateTime StartOfWeek2(this DateTime dt, DayOfWeek startOfWeek)
        {
            int diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7;
            return dt.AddDays(-1 * diff).Date;
        }
    }
}