<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="haberler.aspx.cs" Inherits="takimdeneme.haberler" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Soru Bölümü</title>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.3/css/all.min.css" rel="stylesheet" />
    <style>
        .mozaik {
            display: grid;
            grid-template-columns: repeat(auto-fill, minmax(200px, 1fr));
            grid-auto-rows: 200px;
            grid-gap: 10px;
        }
        .normal-view {
    filter: none;
}
         .image-container {
        display: flex;
        justify-content: center;
        align-items: center;
        flex-wrap: wrap;
    }

    .image-box {
        width: 100px;
        height: 100px;
        margin: 20px;
        display: flex;
        justify-content: center;
        align-items: center;
        border: 1px solid #ccc;
        overflow: hidden;
        background-color:white;
        border-radius: 50%; /* Kareleri yuvarlak hale getirir */
    box-shadow: 0 0 10px rgba(0, 0, 0, 0.1); /* Hafif bir gölge ekler */
       
    }

    .centered-image {
        max-width: 100%;
        max-height: 100%;
        object-fit: contain;
    }

        .mozaik img {
            object-fit: cover;
            width: 100%;
            height: 100%;
        }

        #bakiye {
            position: absolute;
            top: 10px;
            right: 10px;
            margin-top: 10px;
            font-size: 24px;
            font-weight: bold;
        }

        .container {
            margin-top: 10px;
            text-align: center;
            font-style:italic;
        }

        #ileriButton ,#menuyedon  {
            text-align: center;
            margin-top: 60px;
            margin-right: 80px;
            padding: 15px 0;
           min-width: 170px;
            padding: 15px 0;
            border-radius: 35px;
            color: white;
            font-weight: 600;
            background-color: #6166B3;
            font-size: 19px;
            cursor: pointer;
            border: none;
            outline: none;
        }


        #menü {
            text-align: center;
            margin-bottom: 60px;
            margin-right: 80px;
            padding: 15px 0;
           min-width: 170px;
            color: white;
            font-weight: 600;
            background-color: #6166B3;
            font-size: 19px;
            cursor: pointer;
            border: none;
            outline: none;
            position: absolute;
  top: 90%;

  border-radius:40px;
  left: 51%;
  transform: translate(-50%, -50%);

        }
        #ileriButton:hover, #menü:hover,#menuyedon:hover {
            background-color: #4b4e9e;
        }

        /* Eklenen stiller */
        #haberResim {
            filter: blur(10px); /* Bulanık filtre uygula */
        }

        .normal-view {
            display: none; /* Normal görünümü başlangıçta gizle */
        }

        .normal-view img {
            filter: blur(0); /* Bulanık filtreyi kaldır */
        }
    </style>
     <script>
         function toggleView() {
             var resim = document.getElementById('<%= haberResim.ClientID %>');
             var menubutton = document.getElementById('menü');

             menubutton.innerHTML = 'Menü';
             resim.style.filter = 'blur(0)';
             menubutton.style.display = 'none'; 

            
         }
         function toggleMenu() {
             var menubutton = document.getElementById('menü');
             var ileributton = document.getElementById('<%= ileriButton.ClientID %>');

             menubutton.style.display = 'block'; // Menü butonunu göster
         }
     </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
              <div class="image-container" runat="server" id="imageContainer">
    <asp:Image runat="server" CssClass="image-box" ImageUrl="pl1.png" />
    <asp:Image runat="server" CssClass="image-box" ImageUrl="Bl.png" />
    <asp:Image runat="server" CssClass="image-box" ImageUrl="l1.png" />
    <asp:Image runat="server" CssClass="image-box" ImageUrl="Sa.png" />
    <asp:Image runat="server" CssClass="image-box" ImageUrl="isp.png" />
    <asp:Image runat="server" CssClass="image-box" ImageUrl="Sl.png" />
</div>
            <div class="container">
               <br />
                <asp:Image ID="haberResim" runat="server" ImageUrl="" Height="900" Width="800" />
                <br />
                <asp:Button ID="ileriButton" runat="server" Text="İleri" OnClick="ileriButton_Click" Enabled="true" />
                <asp:Button ID="menuyedon" runat="server" Text="Menü" OnClick="menuyedon_Click" Enabled="true" />
<asp:Button ID="menü" runat="server"  OnClick="menü_Click" Text=' AL VE GÖR '></asp:Button>

                <asp:Label ID="bakiye" runat="server" Text='<i class="fas fa-coins"></i> Kullanıcı Bakiyesi: 100'></asp:Label>
            </div>
        </div>
    </form>
</body>
</html>
