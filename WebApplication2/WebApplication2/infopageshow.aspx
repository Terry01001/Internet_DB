<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="infopageshow.aspx.cs" Inherits="WebApplication2.WebForm3" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>個人資訊展示</title>
    <style>
    body {
        background: url('background.jpg') no-repeat center center fixed;
        background-size: cover;
        font-family: Arial, sans-serif;
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
        z-index: 2; /* 确保 Logo 在覆盖层之上 */
    }

    .content-wrapper {
        position: relative;
        z-index: 2; /* 更高的 z-index */
        width: 100%; /* 根据需要调整宽度 */
        max-width: 1100px; /* 限制最大宽度 */
    }

    .info-table {
        width: 100%; /* 表格宽度占据内容包装器的全部宽度 */
        margin: 20px 0;
        border-collapse: collapse;
        box-shadow: 0 2px 5px rgba(0, 0, 0, 0.2);
    }

    .info-table th, .info-table td {
        border: 1px solid #ddd;
        padding: 10px;
        text-align: left;
        background-color: white; /* 为所有单元格设置白色背景 */
    }

    .info-table th {
        background-color: #4CAF50;
        color: white;
    }

    .btn-style {
        padding: 10px 20px;
        background-color: #4CAF50; /* 暗绿色 */
        color: white;
        border: none;
        border-radius: 4px;
        cursor: pointer;
        margin: 5px;
        margin-right: 60px;
        display: inline-block; /* 保持按钮水平排列 */
        font-size: 14px;
    }

    .btn-style:hover {
        background-color: #45a049;
    }

    p {
        text-align: center; /* 居中排列按钮 */
        margin-top: 10px; /* 调整按钮与表格的上边距 */
    }

</style>
</head>
<body>
    <div class="overlay"></div>
    <img src="nsysu.png" alt="NSYSU Logo" id="nsysuLogo">
    <div class="content-wrapper">
    <form id="form1" runat="server">
        <table class="info-table">
            <tr class="header-row">
                <th colspan="2">個人資料</th>
            </tr>
            <tr>
                <td>姓名</td>
                <td><asp:Label ID="LabelName" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td>學號 (職位編號)</td>
                <td><asp:Label ID="LabelId" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td>實驗室 (辦公室) 電話</td>
                <td><asp:Label ID="LabelLabNumber" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td>聯絡電話</td>
                <td><asp:Label ID="LabelPhone" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td>郵局賬號</td>
                <td><asp:Label ID="LabelBank" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td>Email</td>
                <td><asp:Label ID="LabelEmail" runat="server"></asp:Label></td>
            </tr>
        </table>
        <p>
            <asp:Button ID="Buttonedit" runat="server" CssClass="btn-style" OnClick="Buttonedit_Click" Text="編輯個人資料" />
            <asp:Button ID="Buttoneditschedule" runat="server"  CssClass="btn-style" Text="編輯排班表" OnClick="Buttoneditschedule_Click" />
            <asp:Button ID="Buttonschedule" runat="server"  CssClass="btn-style" Text="檢視排班表" OnClick="Buttonschedule_Click" />
            <asp:Button ID="Buttonmanage" runat="server"  CssClass="btn-style" Text="管理員頁面" OnClick="Buttonmanage_Click" />
            <asp:Button ID="Buttonout" runat="server" Text="登出"  CssClass="btn-style" OnClick="Buttonout_Click" />
        </p>
    </form>
   </div>
</body>
</html>
