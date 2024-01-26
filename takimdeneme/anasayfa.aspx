<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="anasayfa.aspx.cs" Inherits="takimdeneme.anasayfa" %>




<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>İstatiksel Futbol</title>
    <style>
        * {
            margin: 0;
            padding: 0;
            box-sizing: border-box;
            font-family: sans-serif;
        }
        
        body {
            margin: 0;
            padding: 0;
        }
        
        .banner {
            height: 100vh;
            width: 100%;
            background-image: linear-gradient(rgba(0, 0, 0, 0.8), rgba(0, 0, 0, 0.8)), url(futbol.jpg);
            background-position: center;
            background-size: cover;
        }
        
        .navbar {
            display: flex;
            width: 90%;
            margin: auto;
            padding: 32px 0;
            align-items: center;
            justify-content: space-between;
        }
        
        .logo {
            cursor: pointer;
            border-radius: 50%;
        }
        
        .navbar ul li {
            list-style-type: none;
            display: inline-block;
            margin: 0 25px;
        }
        
        .navbar ul li a {
            text-decoration: none;
            color: #fff;
        }
        
        .content {
            width: 100%;
            position: absolute;
            top: 40%;
            color: white;
            text-align: center;
        }
        
        .content h1 {
            font-size: 50px;
        }
        
        .content p {
            font-weight: bold;
            font-size: 20px;
        }
        
        #btnKayitOl, #btnGiris {
             width: 200px;
            padding: 15px 0;
            border-radius: 35px;
            margin: 20px;
            text-align: center;
            color: white;
            font-weight: 600;
            background-color: #6166B3;
            font-size: 17px;
            cursor: pointer;
            border: none; /* Butonun kenarlığını kaldırır */
            outline: none; /* Butonun tıklanınca çevresindeki vurguyu kaldırır */
        }
        
        #btnKayitOl:hover, #btnGiris:hover {
            background-color: #4b4e9e;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="banner">
            <div class="navbar">
                <img src="akü.jpg" alt="Logo" class="logo" width="100" height="100" />
                <ul>
                    <li><a href="anasayfa.aspx">ANASAYFA</a></li>
                    <li><a href="hakkinda.aspx">HAKKINDA</a></li>
                    <li><a href="iletisim.aspx">İLETİŞİM</a></li>
                    <li><a href="sss.aspx">SSS</a></li>
                </ul>
            </div>
            <div class="content">
                <h1>İSTATİSTİKSEL FUTBOL</h1>
                <p>Futbolun Sistematği</p>
                <asp:Button ID="btnKayitOl" runat="server" Text="KAYIT OL" OnClick="btnKayitOl_Click" />
                <asp:Button ID="btnGiris" runat="server" Text="GİRİŞ" OnClick="btnGiris_Click" />
            </div>
        </div>
    </form>
</body>
</html>
