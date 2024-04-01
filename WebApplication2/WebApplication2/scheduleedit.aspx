<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="scheduleedit.aspx.cs" Inherits="WebApplication2.WebForm5" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
        <title>編輯排班表</title>
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
        background-color: rgba(255, 255, 255, 0.5); /* 半透明覆盖层 */
        z-index: 1;
    }

    #nsysuLogo {
        position: absolute;
        top: 10px;
        right: 10px;
        width: 200px;
        z-index: 2; /* 确保 Logo 在覆盖层之上 */
    }

    .content-wrapper {
        position: relative;
        z-index: 2; /* 高于覆盖层的 z-index */
        width: 80%; /* 调整内容宽度 */
        max-width: 960px; /* 最大宽度 */
        margin-top: 50px; /* 顶部距离 */
        text-align: center;
    }

    .info-table, .aspNetHidden {
        width: 100%;
        margin-top: 20px; /* 表格上方距离 */
        border-collapse: collapse;
    }

    .info-table th, .info-table td {
        border: 1px solid #ddd;
        padding: 8px;
        background-color: #fff; /* 单元格背景颜色 */
    }

    .info-table th {
        background-color: #4CAF50;
        color: white;
    }
    .info-table .TemplateField {
        /* 模板字段样式，可根据需要调整 */
        text-align: center;
    }
    .info-table .TextBox {
        width: 100%; /* 根据需要调整宽度 */
        border: 1px solid #ccc;
        padding: 5px;
    }

    .btn-style {
        padding: 10px 20px;
        background-color: #4CAF50; /* 按钮背景颜色 */
        color: white;
        border: none;
        border-radius: 4px;
        cursor: pointer;
        display: inline-block; /* 使按钮并排显示 */
        font-size: 16px;
        margin: 10px 20px; /* 增加左右间距以分开按钮 */
        margin-right: 150px;
        margin-left: 150px;


    }

    /* 特定于返回按钮的样式，确保它位于 GridView 中间 */
    #Button2 {
        margin-left: 0; /* 移除额外的左边距 */
        margin-right: 0; /* 移除额外的右边距 */
    }
    #btnSubmitChanges {
        width: auto;
        margin-top: 10px; 
        display: block; /* 使按钮单独一行 */
    }


    .btn-style:hover {
        background-color: #45a049;
    }
    .gridview-header {
        background-color: #4CAF50; /* 表头背景颜色 */
        color: white;
        text-align: center;
        /* 可以添加更多样式 */
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
    .btn-submit-center {
        display: block; /* 使按钮呈块级元素，可以应用居中 */
        margin: 20px auto; /* 自动边距实现居中 */
        padding: 10px 20px;
        background-color: #4CAF50; /* 按钮背景颜色 */
        color: white;
        border: none;
        border-radius: 4px;
        cursor: pointer;
        font-size: 16px;
    }

    .btn-submit-center:hover {
        background-color: #45a049;
    }

</style>
</head>
<body>
    <div class="overlay"></div> <!-- 背景覆盖层 -->
    <img src="nsysu.png" alt="NSYSU Logo" id="nsysuLogo"> 
    <div class="content-wrapper">
    <form id="form1" runat="server">
        <div>
            <asp:GridView ID="ScheduleGridView2" runat="server" AutoGenerateColumns="False"
    CssClass="info-table" GridLines="None" 
    HeaderStyle-CssClass="gridview-header" RowStyle-CssClass="gridview-row">
    <Columns>
        <asp:TemplateField HeaderText="日期">
            <ItemTemplate>
                <asp:Label ID="lblDate" runat="server" Text='<%# Bind("Date") %>' CssClass="gridview-cell"></asp:Label>
            </ItemTemplate>
            <ItemStyle Width="150px" />
        </asp:TemplateField>

        <asp:BoundField DataField="DayOfWeek" HeaderText="星期幾" 
            ItemStyle-CssClass="gridview-cell" ItemStyle-Width="100px" />

        <asp:TemplateField HeaderText="上午班">
            <ItemTemplate>
                <asp:Label ID="lblMorningShift" runat="server" Text='<%# Bind("MorningShift") %>' Visible='<%# !string.IsNullOrEmpty(Eval("MorningShift").ToString()) %>' CssClass="gridview-cell"></asp:Label>
                <asp:TextBox ID="tbMorningShift" runat="server" Visible='<%# string.IsNullOrEmpty(Eval("MorningShift").ToString()) %>' CssClass="gridview-cell"></asp:TextBox>
            </ItemTemplate>
            <ItemStyle Width="200px" />
        </asp:TemplateField>

        <asp:TemplateField HeaderText="下午班">
            <ItemTemplate>
                <asp:Label ID="lblAfternoonShift" runat="server" Text='<%# Bind("AfternoonShift") %>' Visible='<%# !string.IsNullOrEmpty(Eval("AfternoonShift").ToString()) %>' CssClass="gridview-cell"></asp:Label>
                <asp:TextBox ID="tbAfternoonShift" runat="server" Visible='<%# string.IsNullOrEmpty(Eval("AfternoonShift").ToString()) %>' CssClass="gridview-cell"></asp:TextBox>
            </ItemTemplate>
            <ItemStyle Width="200px" />
        </asp:TemplateField>

    </Columns>
</asp:GridView>

        </div>
        <asp:Button ID="Button1" runat="server" CssClass="btn-style" Text="上一周" OnClick="Button1_Click" />
        <asp:Button ID="Button2" runat="server" CssClass="btn-style" Text="返回" OnClick="Button2_Click" />
        <asp:Button ID="Button3" runat="server" CssClass="btn-style" Text="下一周" OnClick="Button3_Click" />
        <p>
        <asp:Button ID="btnSubmitChanges" runat="server" CssClass="btn-submit-center" Text="提交更改" OnClick="btnSubmitChanges_Click" />

        </p>

    </form>
    </div>
</body>
</html>
