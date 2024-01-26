w<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="iletisim.aspx.cs" Inherits="takimdeneme.iletisim" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">

    <style>
        body {
            background-image: url(kayit.jpg);
            display: flex;
            justify-content: center;
            align-items: center;
            height: 100vh;
        }

        .container {
            background: rgba(0, 0, 0, 0.6);
            min-width: 25%;
            min-height: 50%;
            color: white;
            display: flex;
            flex-direction: column;
            padding: 25px;
            margin-top: 20px;
            border-radius: 40px;
        }

            .container h3 {
                font-size: 50px;
                margin-bottom: 50px;
                color: white;
                margin-top: 0;
                padding-left:160px;
                padding-top:20px;
            }

            .container p {
                font-size: 23px;
                margin-bottom: 20px;
            }
    </style>
    <title>İletişim</title>
</head>
<body>
    <div class="container">
        <h3>İletişim</h3>

        <p><strong>istatistikfootball@gmail.com</strong></p>

        <p><strong>05312345678</strong></p>

        <p><strong>Manisa/Akhisar/Hürriyet Mah./25.Sokak/No:37</strong></p>
    </div>
</body>
</html>
