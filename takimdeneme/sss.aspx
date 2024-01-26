<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="sss.aspx.cs" Inherits="takimdeneme.sss" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <link rel="stylesheet" href="//code.jquery.com/ui/1.13.2/themes/base/jquery-ui.css">
    <link rel="stylesheet" href="/resources/demos/style.css">
    <script src="https://code.jquery.com/jquery-3.6.0.js"></script>
    <script src="https://code.jquery.com/ui/1.13.2/jquery-ui.js"></script>
    <title>Sıkça Sorulan Sorular</title>
    <style>
        body {
            background-image: linear-gradient(rgba(0,0,0,0.0),rgba(0,0,0,0.6)),url(kayit.jpg);
            background-size: cover;
            background-position: center;
            background-attachment: fixed;
        }

        .sss h3 {
            font-size: 50px;
            margin: auto;
            text-align: center;
            color: white;
            padding: 20px;
            padding-bottom: 1px;
            margin-right: 10px;
        }

        #accordion {
            width: 70%;
            padding: 50px;
            padding-left: 240px;
            border-radius: 40px;
            font-size: 18px;
            border-radius:40%;
        }
            #accordion h3 {
                background-color: #d30034;
                opacity: 89%;
                font-style: italic;
                color:white;
            }
    </style>
    <script>
        $(function () {
            $("#accordion").accordion();
        });
    </script>
    <title>Futbol Sayfası - SSS</title>



</head>
<body>
    <div class="sss">
        <h3>Sıkça Sorulan </h3>
        <h3>Sorular </h3>
    </div>



    <div id="accordion">

        <h3>1. Hangi liglerin puan durumlarına erişebilirim?</h3>
        <div>
            <p>Cevap: İstatistiksel Futbol Sayfası, Premier Lig, La Liga, Bundesliga, Serie A, Ligue 1 ve Türkiye Süper Ligi'nin puan durumlarını sunmaktadır.</p>
        </div>
        <h3>2. Kaç takımın ve futbolcunun verilerine erişebilirim?</h3>
        <div>
            <p>Cevap: İstatistiksel Futbol Sayfası, 120'ye yakın takımın ve 5.000'i aşkın futbolcunun verilerini içermektedir.</p>
        </div>
        <h3>3. Hangi tür verileri bulabilirim?</h3>
        <div>
            <p>Cevap: İstatistiksel Futbol Sayfası, takımların puan durumları, gerçekleşen transferler ve futbolculara ait çeşitli verileri sunmaktadır.</p>
        </div>
    </div>
</body>
</html>

