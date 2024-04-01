using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;

namespace WebApplication2
{
    public partial class WebForm4 : Page
    {
        private DateTime CurrentWeekStart
        {
            get { return ViewState["CurrentWeekStart"] != null ? (DateTime)ViewState["CurrentWeekStart"] : DateTime.Now.StartOfWeek(DayOfWeek.Sunday); }
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
            var scheduleData = GetWeeklySchedule(CurrentWeekStart); // use currentweekstart as parameter
            ScheduleGridView.DataSource = scheduleData;
            ScheduleGridView.DataBind();
        }

        private List<Schedule> GetWeeklySchedule(DateTime startDate)
        {
            // get data from database
            DataTable dt = GetScheduleDataFromDatabase(startDate, startDate.AddDays(7));

            // create a list including a week
            var scheduleData = Enumerable.Range(0, 7)
                .Select(i => startDate.AddDays(i))
                .Select(date => new Schedule
                {
                    Date = date.ToString("yyyy-MM-dd"),
                    DayOfWeek = date.DayOfWeek.ToString(),
                    // 如果是周六或周日，show 公休
                    MorningShift = (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday) ? "公休" : "",
                    AfternoonShift = (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday) ? "公休" : ""
                }).ToList();

            // fill data load from database
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

    public class Schedule
    {
        public string Date { get; set; }
        public string DayOfWeek { get; set; }
        public string MorningShift { get; set; }
        public string AfternoonShift { get; set; }
    }


    // 一個幫助方法來獲取一周的開始日期
    public static class DateTimeExtensions
    {
        public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
        {
            int diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7;
            return dt.AddDays(-1 * diff).Date;
        }
    }

}