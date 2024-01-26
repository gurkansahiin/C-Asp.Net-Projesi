<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="soru.aspx.cs" Inherits="takimdeneme.soru" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Soru Bölümü</title>
     <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.3/css/all.min.css" rel="stylesheet" />
    <style>
        /* CSS stil tanımlamalarını buraya ekleyebilirsiniz */
        /* Örnek olarak: */
        .soru-metni {
            font-size: 18px;
            font-weight: bold;
            margin-bottom: 40px;
        }
        .sik-kutusu input[type="radio"] {
    transform: scale(2); /* Boyutu istediğiniz oranda arttırabilirsiniz */
}
        .sik-kutusu {
            display: inline-block;
      
         margin-left:100px;
         
        }
        .sik {
            font-size: 60px;
           

        }
       .dogru-cevap {
    color: green;
    animation: blink 1s infinite;
}

.yanlis-cevap {
    color: red;
    animation: blink 1s infinite;
}

@keyframes blink {
    0% { opacity: 1; }
    50% { opacity: 0; }
    100% { opacity: 1; }
}


           #bakiye {
    position: absolute;
    top: 10px;
    right: 10px;
    margin-top: 10px;
    font-size: 24px;
    font-weight: bold;
}
#sayaç {
    position: absolute;
    top: 30px;
    right: 10px;
    font-size: 20px;
    color: #777;
    font-weight: bold;
    margin-top: 20px;
    color:red;
}

        .sik-kutusu label {
    font-size: 29px;
    margin-right:185px;
    
}
        .container  {
            margin-top:150px;
            text-align:center;
        }
        #ileriButton {
            text-align: center;
            margin-top: 60px;
       
            margin-right:150px;
            width: 150px;
            padding: 15px 0;
            border-radius: 35px;
            color: white;
            font-weight: 600;
            background-color: #6166B3;
            font-size: 19px;
            cursor: pointer;
            border: none; /* Butonun kenarlığını kaldırır */
            outline: none; /* Butonun tıklanınca çevresindeki vurguyu kaldırır */
             
        }
         #menü {
            text-align: center;
            margin-top: 60px;
            margin-right:80px;
          padding: 15px 0;
            width: 150px;
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
          .container h1 {
        margin-top: -80px;
        font-size: 30px;
        font-weight: bold;
        font-style: italic;
    }
             #ileriButton:hover, #menü:hover {
            background-color: #4b4e9e;
        }
        
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
           
              <div class="container">
                   <h1> Futbol Quiz </h1>
            <div class="soru-metni">
                <asp:Literal ID="soruMetni" runat="server"></asp:Literal>
            </div>

            <br />
            <asp:Image ID="soruResim" runat="server" ImageUrl="" />
            <br />

            <asp:RadioButtonList ID="secenekler" runat="server" RepeatDirection="Horizontal" CssClass="sik-kutusu">
                <asp:ListItem Text=" A " CssClass="sik" />
                <asp:ListItem Text=" B " CssClass="sik" />
                <asp:ListItem Text=" C " CssClass="sik" />
                <asp:ListItem Text=" D " CssClass="sik" />
            </asp:RadioButtonList>

            <br />

<asp:Button ID="ileriButton" runat="server" Text="İleri" OnClick="ileriButton_Click" Enabled="true" />
                  <asp:Button ID="menü" runat="server" Text="Menü" OnClick="menü_Click" />

 <asp:Label ID="bakiye" runat="server" Text='<i class="fas fa-coins"></i> Kullanıcı Bakiyesi: 100'></asp:Label>
                 <asp:Label ID="sayaç" runat="server"></asp:Label>

        </div>
            </div>

      
        <script type="text/javascript">
            var counter = 15;
            var interval;

            function startCounter() {
                interval = setInterval(updateCounter, 850);
            }

            function updateCounter() {
                counter--;
                document.getElementById('<%= sayaç.ClientID %>').innerHTML = "Sayaç: " + counter + " saniye";

    if (counter === 0) {
        clearInterval(interval);
        ileriButtonClick();
    }
}

function ileriButtonClick() {
                var ileriButton = document.getElementById('<%= ileriButton.ClientID %>');
                if (!ileriButton.disabled && !ileriButton.clicked) {
                    ileriButton.clicked = true;
                    ileriButton.click();
                }
            }

            window.onload = startCounter;

        </script>


    </form>
</body>
</html>
