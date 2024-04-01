<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="WebApplication1.WebForm1" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>工讀生排班系統 - 登錄</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            margin: 0;
            padding: 0;
            display: flex;
            justify-content: center;
            align-items: center;
            height: 100vh;
            background: url('background.jpg') no-repeat center center fixed;
            background-size: cover;
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

        form {
            z-index: 2;
            position: relative;
            background-color: white;
            padding: 20px;
            border-radius: 5px;
            box-shadow: 0 2px 4px rgba(0, 0, 0, 0.2);
            text-align: center;
        }

        .input-field {
            width: 100%;
            padding: 10px;
            margin: 5px 0;
            border-radius: 4px;
            border: 1px solid #ddd;
            box-sizing: border-box; 
        }

        .btn {
            width: 100%;
            padding: 10px;
            border: none;
            border-radius: 4px;
            background-color: #5cb85c;
            color: white;
            cursor: pointer;
        }

        .btn:hover {
            background-color: #4cae4c;
        }

        h4, p {
            margin: 10px 0;
        }
    </style>
</head>
<body>
    <div class="overlay"></div>
    <img src="nsysu.png" alt="NSYSU Logo" id="nsysuLogo">
    <form id="form1" runat="server">
        <div>
            <h2>工讀生排班系統</h2>
            <h4>賬號</h4>
            <asp:TextBox ID="TextBox2" runat="server" CssClass="input-field"></asp:TextBox>

            <h4>密碼</h4>
            <asp:TextBox ID="TextBox1" runat="server" TextMode="Password" CssClass="input-field" OnTextChanged="TextBox1_TextChanged"></asp:TextBox>

            <asp:Button ID="Button1" runat="server" CssClass="btn" OnClick="Button1_Click" Text="Login" />
            <p>*第一次登錄只需填寫學號作為賬號 - 系統會寄密碼給您的信箱 </p>
        </div>
    </form>
</body>
</html>
