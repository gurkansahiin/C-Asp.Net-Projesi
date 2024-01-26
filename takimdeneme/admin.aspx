<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="admin.aspx.cs" Inherits="takimdeneme.admin" %>




<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
    <style>
        * {
            font-family: sans-serif;
            padding: 0;
            margin: 0;
        }

        body {
            height: 100%;
            width: 100%;
            background-image: linear-gradient(rgba(0,0,0,0.0),rgba(0,0,0,0.1)),url(giris.jpg);
            background-position: center;
            background-size: cover;
            display: flex;
            justify-content: center;
            align-items: center;
        }

        .login-form {
            background: rgba(0,0,0,0.8);
            min-width: 25%;
            min-height: 80%;
            color: white;
            display: flex;
            flex-direction: column;
            padding: 25px;
            margin-top: 20px;
             border-radius:40px;
        }

        h1 {
            text-transform: uppercase;
            text-align: center;
            margin-bottom: 10px
        }

        .tbox {
            margin-top: 15px;
            padding: 10px;
            border: none;
            font-size: 18px;
        }

        .pbox {
            margin-top: 20px;
            padding: 10px;
            margin-bottom: 10px;
            border: none;
            font-size: 18px;
        }

        .btn {
            width: 160px;
            padding: 15px 0;
            border-radius: 25px;
            margin: 75px auto;
            text-align: center;
            margin-top: 50px;
            font-size: 18px;
            font-weight: 600;
            background-color: #6166B3;
            color: white;
            cursor: pointer;
        }

        .btn:hover {
            color: black;
            transition: 1s;
        }

        .login-form a {
            text-decoration: none;
            color: white;
            text-align: center;
        }
    </style>
</head>
<body>
    <form id="loginForm" runat="server" class="login-form">
        <h1>GİRİŞ</h1>
        <asp:TextBox ID="kullanıcı" runat="server" CssClass="tbox" placeholder="kullancı adı  giriniz"></asp:TextBox>
        <asp:TextBox ID="password" runat="server" CssClass="pbox" TextMode="Password" placeholder="Şifrenizi giriniz"></asp:TextBox>
        <asp:Button ID="loginButton" runat="server" CssClass="btn" Text="Giriş Yap" OnClick="loginButton_Click" />
   
        
    </form>
</body>
</html>
