<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="manager.aspx.cs" Inherits="WebApplication2.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>管理員頁面</title>
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
        padding: 20px; /* 添加一些内边距 */
        background: #fff; /* 为了增强可读性，设置一个白色背景 */
        border-radius: 8px; /* 可选的圆角边框 */
        box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1); /* 可选的阴影效果 */
        text-align: center;
    }

    .info-table {
        width: 100%;
        border-collapse: collapse;
        margin-bottom: 15px; /* 表格和按钮之间的距离 */
    }

    .info-table th, .info-table td {
        border: 1px solid #ddd;
        padding: 8px;
        text-align: left;
    }

    .info-table th {
        background-color: #4CAF50;
        color: white;
    }

    .info-table .CommandField {
        background-color: #f2f2f2; /* 可选的命令字段背景色 */
    }

    a, .btn-style {
        padding: 10px 15px;
        background-color: #4CAF50; /* 按钮背景颜色 */
        color: white;
        text-decoration: none; /* 移除链接的下划线 */
        border: none;
        border-radius: 4px;
        cursor: pointer;
        display: inline-block;
        margin: 5px 0; /* 按钮间距 */
    }

    a:hover, .btn-style:hover {
        background-color: #45a049; /* 鼠标悬浮时的颜色变化 */
    }

    .btn-style {
        width: auto; /* 按钮自适应内容宽度 */
    }

    .gridview-cell {
        width: 150px; /* 或您想要的任何宽度 */
        text-align: center; /* 如果您想居中对齐内容 */
    }

    .gridview-header {
        width: 150px; /* 或您想要的任何宽度 */
        text-align: center; /* 如果您想居中对齐内容 */
        /* 添加您想要的任何其他样式 */
    }

    .btn-center {
        display: block;
        margin: 0 auto; /* 自动边距使按钮居中 */
        text-align: center; /* 文字居中 */
    }
</style>

</head>
<body>
    <div class="overlay"></div> <!-- 背景覆盖层 -->
    <img src="nsysu.png" alt="NSYSU Logo" id="nsysuLogo"> 
    <div class="content-wrapper">
    <form id="form1" runat="server">
        <div>

            <asp:GridView ID="MyGridView" runat="server" AutoGenerateColumns="False" 
              DataKeyNames="賬號" AllowPaging="True" AllowSorting="True" OnRowEditing="MyGridView_RowEditing" OnRowUpdating="MyGridView_RowUpdating" OnRowCancelingEdit="MyGridView_RowCancelingEdit">
    <Columns>
    <asp:BoundField DataField="賬號" HeaderText="賬號" ReadOnly="True" 
        HeaderStyle-CssClass="gridview-header" ItemStyle-CssClass="gridview-cell" />
    <asp:BoundField DataField="密碼" HeaderText="密碼" 
        HeaderStyle-CssClass="gridview-header" ItemStyle-CssClass="gridview-cell" />
    <asp:BoundField DataField="Manage" HeaderText="manage" 
        HeaderStyle-CssClass="gridview-header" ItemStyle-CssClass="gridview-cell" />
    <asp:CommandField ShowEditButton="True" 
        HeaderStyle-CssClass="gridview-header" ItemStyle-CssClass="gridview-cell" />
</Columns>

            </asp:GridView>
        </div>
       <p>
        <asp:Button ID="Button1" runat="server" CssClass="btn-style btn-center" OnClick="Button1_Click" Text="返回" />
    </p>
    </form>
    </div>
</body>
</html>
