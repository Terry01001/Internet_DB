<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="infopageedit.aspx.cs" Inherits="WebApplication2.WebForm2" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>個人資料填寫</title>
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
        }
        .info-table {
            width: 110%; /* 调整为适当的宽度 */
            border-collapse: collapse;
            margin: 45px 0;
            background-color: #ffffff;
            box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
        }
        .info-table th, .info-table td {
            border: 1px solid #ddd;
            padding: 10px;
            text-align: left;
        }
        .info-table th {
            background-color: #4CAF50; /* 暗绿色 */
            color: white;
        }
        .header-row {
            background-color: #9c9c9c;
            color: white;
        }
        .input-field {
            width: 95%;
            padding: 5px;
            margin: 5px;
            border: 1px solid #ccc;
            border-radius: 4px;
        }
        .btn-submit {
            width: 100px;
            padding: 10px 20px;
            background-color: #4CAF50; /* 暗绿色 */
            color: white;
            border: none;
            border-radius: 4px;
            cursor: pointer;
            font-size: 16px;
            text-align: center;
            display: block;
            margin: 20px auto;
        }
        .btn-submit:hover {
            background-color: #45a049;
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
                <th colspan="4">個人資料填寫</th>
            </tr>
            <tr>
                <td>姓名</td>
                <td><asp:TextBox ID="TextBoxName" runat="server" CssClass="input-field"></asp:TextBox></td>
                <td>學號 (職位編號)</td>
                <td><asp:TextBox ID="TextBoxId" runat="server" CssClass="input-field" ReadOnly="true"></asp:TextBox></td>
            </tr>
            <tr>
                <td>密碼</td>
                <td><asp:TextBox ID="TextBoxPassword" runat="server" CssClass="input-field" TextMode="Password"></asp:TextBox></td>
                <td>密碼確認</td>
                <td><asp:TextBox ID="TextBoxPasswordConfirm" runat="server" CssClass="input-field" TextMode="Password"></asp:TextBox></td>
            </tr>
            <tr>
                <td>實驗室 (辦公室) 電話</td>
                <td><asp:TextBox ID="TextBoxLabNumber" runat="server" CssClass="input-field"></asp:TextBox></td>
                <td>聯絡電話</td>
                <td><asp:TextBox ID="TextBoxPhone" runat="server" CssClass="input-field"></asp:TextBox></td>
            </tr>
            <tr>
                <td>郵局賬號</td>
                <td colspan="3"><asp:TextBox ID="TextBoxBank" runat="server" CssClass="input-field"></asp:TextBox></td>
            </tr>
            <tr>
                <td>Email</td>
                <td colspan="3"><asp:TextBox ID="TextBoxEmail" runat="server" CssClass="input-field"></asp:TextBox></td>
            </tr>
        </table>
        <asp:Button ID="ButtonSubmit" runat="server" CssClass="btn-submit" Text="提交資料" OnClick="ButtonSubmit_Click" />
    </form>
    </div>
</body>
</html>
