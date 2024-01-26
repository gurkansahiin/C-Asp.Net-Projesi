<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="hakkinda.aspx.cs" Inherits="takimdeneme.hakkinda" %>
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>Hakkında</title>
    <link rel="stylesheet" href="//code.jquery.com/ui/1.13.2/themes/base/jquery-ui.css">
    <link rel="stylesheet" href="/resources/demos/style.css">
    <script src="https://code.jquery.com/jquery-3.6.0.js"></script>
    <script src="https://code.jquery.com/ui/1.13.2/jquery-ui.js"></script>
   
    <style>
        body {
            background-image: linear-gradient(rgba(0,0,0,0.0),rgba(0,0,0,0.6)),url(kayit.jpg);
            background-size: cover;
            background-position: center;
            background-attachment: fixed;
        }

        .hakkında h3 {
            font-size: 50px;
            margin: auto;
            text-align: center;
            color: white;
            padding: 20px;
            padding-bottom: 1px;
            margin-right: 10px;
            
        }

        #accordion {
            width: 75%;
            padding: 50px;
            padding-left: 190px;
            padding-bottom:10px;
            border-radius: 40px;
            font-size: 18px;
            opacity:89%;
            border-radius:40px;
        }
            #accordion h3 {
                background-color: #d30034;
                opacity: 89%;
                font-style:italic;
            }
    </style>
    <script>
        $(function () {
            $("#accordion").accordion({
                heightStyle: "fill"
            });

            $("#accordion-resizer").resizable({
                minHeight: 140,
                minWidth: 200,
                resize: function () {
                    $("#accordion").accordion("refresh");
                }
            });
        });
    </script>
    <title>Futbol Sayfası - SSS</title>



</head>
<body>
    <div class="hakkında">
        <h3>İstatistiksel Futbol </h3>
        <h3>Hakkında </h3>
    </div>



    <div id="accordion">

        <h3>Biz Kimiz?</h3>
        <div>
            <p>
                İstatistiksel Futbol Sayfası, 5 büyük Majör Lig (Premier Lig, La Liga, Bundesliga, Serie A ve Ligue 1) ve Türkiye Süper Ligi'nde yer alan takımların puan durumlarını, gerçekleşen transferleri ve futbolculara ait çeşitli verileri sunan kapsamlı bir futbol web sitesidir. Sizlere hizmet vermek amacıyla 120'ye yakın takımın ve 5.000'i aşkın futbolcunun verilerini içermektedir.
            </p>
            <p>
                İstatistiksel Futbol Sayfası, futbol severlerin güncel lig tablolarını, takımların performansını ve oyuncuların istatistiklerini takip etmelerini sağlar. Her bir takımın sonuçlarına, attığı ve yediği gollerin sayısına, puan durumuna ve diğer istatistiklere kolayca ulaşabilirsiniz. Ayrıca, transfer dönemlerinde gerçekleşen transferlerle ilgili güncel bilgilere erişebilirsiniz.
            </p>
            <p>
                Sayfamız, futbolcuların kariyer istatistiklerini ve kişisel bilgilerini sunarak oyuncular hakkında daha fazla bilgi edinmenizi sağlar. Her futbolcunun oynadığı maç sayısı, attığı goller, asistler, sarı ve kırmızı kartlar gibi verileri görüntüleyebilirsiniz. Ayrıca, futbolcuların geçmiş ve mevcut takımlarıyla ilgili bilgilere ulaşabilir ve transfer haberlerini takip edebilirsiniz.
            </p>
            <p>
                İstatistiksel Futbol Sayfası, futbol tutkunlarına en güncel ve kapsamlı verileri sunmayı hedeflemektedir.
            </p>
        </div>
       
    </div>
</body>
</html>
