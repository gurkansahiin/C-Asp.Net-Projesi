<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="kayit.aspx.cs" Inherits="takimdeneme.kayit" %>




<!DOCTYPE html>

<html>
<head>
    <title>Kayıt Formu</title>
     <style>
        * {
            box-sizing: border-box;
            font-family: Arial, sans-serif;
            
        }

        body {
            height: 100%;
            width: 100%;
            background-image: url(kayit.jpg);
            background-position: center;
            background-size: cover;
            margin-top: 50px;
        }

        .container {
            background-color: #f1f1f1;
            border-radius: 5px;
            padding: 20px;
            margin: 0 auto;
            max-width: 400px;
            background: rgba(0,0,0,0.8);
            color: #FFFFFF;
             border-radius:40px;
        }

        h2 {
            text-align: center;
            margin-bottom: 20px;
        }

        .form-group {
            margin-bottom: 20px;
        }

            .form-group label {
                display: block;
                margin-bottom: 5px;
            }

            .form-group input {
                width: 100%;
                padding: 10px;
                border-radius: 5px;
                border: 1px solid #ccc;
            }

            .form-group .error-message {
                color: red;
                font-size: 14px;
                margin-top: 5px;
            }

        .btn-container {
            text-align: center;
        }

        .btn {
            padding: 10px 20px;
            background-color: #6166B3;
            border: none;
            color: white;
            text-align: center;
            text-decoration: none;
            display: inline-block;
            font-size: 16px;
            border-radius: 5px;
            cursor: pointer;
        }

        .login-link {
            display: block;
            text-align: center;
            margin-top: 10px;
            color:white;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h2>Kayıt Formu</h2>
            <div class="form-group">
                <label for="username">Kullanıcı Adı:</label>
                <asp:TextBox ID="username" runat="server"  required></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="email">E-mail:</label>
                <asp:TextBox ID="email" runat="server" required></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="password">Şifre:</label>
                <asp:TextBox ID="password" runat="server"  TextMode="Password" minlength="6" required></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="confirm-password">Şifre Tekrarı:</label>
                <asp:TextBox ID="TextBox1" runat="server" TextMode="Password" minlength="6" required></asp:TextBox>
            </div>
            <div class="btn-container">
                <asp:Button ID="btnKayitOl" runat="server" Text="Kayıt Ol" CssClass="btn" OnClick="btnKayitOl_Click" />
            </div>
        </div>
    </form>
    <p class="login-link">Zaten üye misiniz? <a href="giris.aspx" style="color: white;">Giriş yapın </a></p>

</body>
</html>
