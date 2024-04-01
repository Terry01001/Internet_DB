<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="scheduleshow.aspx.cs" Inherits="WebApplication2.WebForm4" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>檢視排班表</title>
    <style>
        body {
            background: url('background.jpg') no-repeat center center fixed;
            background-size: cover;
            font-family: 'Arial', sans-serif;
            margin: 0;
            padding: 0;
            display: flex;
            justify-content: center;
            align-items: center;
            height: 100vh;
            position: relative;
        }

        .overlay {
            position: absolute;
            top: 0;
            left: 0;
            right: 0;
            bottom: 0;
            background-color: rgba(255, 255, 255, 0.5); 
            z-index: 1;
        }

        #nsysuLogo {
            position: absolute;
            top: 10px;
            right: 10px;
            width: 200px;
            z-index: 2; 
        }

        .content-wrapper {
            position: relative;
            z-index: 2; 
            width: 80%; 
            max-width: 960px; 
            margin-top: 50px; 
            text-align: center;
        }

        .info-table, .aspNetHidden {
            width: 100%;
            margin-top: 20px;
            border-collapse: collapse;
        }

        .info-table th, .info-table td {
            border: 1px solid #ddd;
            padding: 8px;
            background-color: #fff;
        }

        .info-table th {
            background-color: #4CAF50;
            color: white;
        }

        .btn-style {
            padding: 10px 20px;
            background-color: #4CAF50; 
            color: white;
            border: none;
            border-radius: 4px;
            cursor: pointer;
            display: inline-block; 
            font-size: 16px;
            margin: 10px 20px; 
            margin-right: 150px;
            margin-left: 150px;


        }

        /* 特定于返回按钮的样式，确保它位于 GridView 中间 */
        #Button2 {
            margin-left: 0; 
            margin-right: 0; 
        }


        .btn-style:hover {
            background-color: #45a049;
        }
        .gridview-header {
            background-color: #4CAF50;
            color: white;
            text-align: center;
        }

        .gridview-row {
            background-color: white; /* 行背景颜色 */
            /* 可以添加更多样式 */
        }

        .gridview-cell {
            text-align: center; /* 单元格内容居中 */
            width: 200px;
            /* 可以添加更多样式 */
        }

    </style>
</head>
<body>
    <div class="overlay"></div> <!-- 背景覆盖层 -->
    <img src="nsysu.png" alt="NSYSU Logo" id="nsysuLogo"> 
    <div class="content-wrapper">
    <form id="form1" runat="server">
        <div>
            <asp:GridView ID="ScheduleGridView" runat="server" AutoGenerateColumns="False"
                CssClass="info-table" GridLines="None" 
                HeaderStyle-CssClass="gridview-header" RowStyle-CssClass="gridview-row">
                <Columns>
                    <asp:BoundField DataField="Date" HeaderText="日期" ItemStyle-CssClass="gridview-cell" ItemStyle-Width="150px"/>
                    <asp:BoundField DataField="DayOfWeek" HeaderText="星期幾" ItemStyle-CssClass="gridview-cell" ItemStyle-Width="100px"/>
                    <asp:BoundField DataField="MorningShift" HeaderText="上午班" ItemStyle-CssClass="gridview-cell" ItemStyle-Width="200px"/>
                    <asp:BoundField DataField="AfternoonShift" HeaderText="下午班" ItemStyle-CssClass="gridview-cell" ItemStyle-Width="200px"/>
                </Columns>
            </asp:GridView>

        </div>
        <asp:Button ID="Button1" runat="server" CssClass="btn-style" Text="上一周" OnClick="Button1_Click" />
        <asp:Button ID="Button2" runat="server" CssClass="btn-style" Text="返回" OnClick="Button2_Click" />
        <asp:Button ID="Button3" runat="server" CssClass="btn-style" Text="下一周" OnClick="Button3_Click" />

    </form>
    </div>
</body>
</html>
