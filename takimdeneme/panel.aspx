<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="panel.aspx.cs" Inherits="takimdeneme.panel" %>

<html>

<head>
    
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.2.1/css/all.min.css">

    <title>Kullanıcı Paneli</title>
    <style>
        * {
            box-sizing: border-box;
            list-style: none;
            margin: 0;
            padding: 0;
            text-decoration: none;
        }

        body {
            height: 100%;
            width: 100%;
            background-image: url(cc.jpg);
            background-position: center;
            background-size: cover;
        }

        .container .sidebar {
            position: fixed;
            width: 300px;
            height: 100%;
            background: rgba(0,0,0,0.8);
            padding: 25px;
            margin-right: -55px;
        }

            .container .sidebar h2 {
                color: #fff;
                text-transform: uppercase;
                text-align: center;
                margin-bottom: 30px;
                cursor: pointer;
                transition: .5s;
            }


                .container .sidebar h2 span {
                    color: #f2b354;
                }

                    .container .sidebar h2 span:hover {
                        color: #fff;
                        transition: .5s;
                    }


                .container .sidebar h2:hover {
                    color: #f2b354;
                }

            .container .sidebar ul li a {
                color: #fff;
            }

            .container .sidebar ul li {
                padding: 15px;
                border-bottom: 1px solid #f2b354;
                transition: .5s;
            }

                .container .sidebar ul li:hover {
                    background: black;
                }

            .container .sidebar .social-media {
                position: absolute;
                bottom: 5px;
                left: 50%;
                transform: translate(-50%);
                display: flex;
            }

                .container .sidebar .social-media a {
                    width: 40px;
                    display: block;
                    background: #f2b354;
                    height: 35px;
                    line-height: 40px;
                    text-align: center;
                    margin: 0 5px;
                    color: black;
                    border-top-right-radius: 5px;
                    border-top-left-radius: 5px;
                    transition: .5s;
                }

                    .container .sidebar .social-media a i {
                        position: center;
                        margin: 10px 5px;
                    }

                    .container .sidebar .social-media a:hover {
                        background-color: white;
                    }
    </style>
</head>

<body>

    <div class="container">
        <div class="sidebar">
            <h2><span>Kullanıcı</span> Paneli</h2>

            <ul>

                <li><a href="anasayfa.aspx"><i class="fas fa-home">Ana Sayfa</i></a></li>
                <li><a href="https://localhost:44364/futbolcular.aspx"><i class="fas fa-user">   Futbolcular</i></a></li>
                <li><a href="https://localhost:44364/takimlar.aspx"><i class="fa-solid fa-circle-half-stroke">   Takımlar</i></a></li>
                <li><a href="https://localhost:44364/transfer.aspx"><i class="fa-solid fa-money-bill-transfer">   Transferler</i></a></li>
                 <li><a href="kullanici.aspx"><i class="fa-solid fa-user">   Kullanıcı</i></a></li>
                <li><a href="soru.aspx"><i class="fa-solid fa-message"> QUİZ</i></a></li>
                <li><a href="https://localhost:44364/giris.aspx"><i class="fa-solid fa-power-off">   CİKİS</i></a></li>
                <li><a href="haberler.aspx"><i class="fa-solid fa-circle-info"> Haberler</i></a></li>
                <li><a href="dene.aspx"><i class="fa-solid fa-question"> SANS OYUNU</i></a></li>
            </ul>
            <div class="social-media">


                <a href="https://www.facebook.com/"><i class="fab fa-facebook"></i></a>
                <a href="https://www.twitter.com/"><i class="fab fa-twitter"></i></a>
                <a href="https://www.instagram.com/"><i class="fa-brands fa-instagram"></i></a>
                <a href="https://web.whatsapp.com/"><i class="fa-brands fa-whatsapp"></i></i></a>

            </div>


        </div>
    </div>
</body>


</html>
