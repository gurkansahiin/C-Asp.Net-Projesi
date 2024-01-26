<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="transferupdate.aspx.cs" Inherits="takimdeneme.transferupdate" %>




<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Futbolcu İşlemleri</title>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.2.1/css/all.min.css" />
    <style>
        /* CSS kodu buraya ekleyebilirsiniz */
        .container {
            display: flex;
            flex-direction: column;
            justify-content: center;
            align-items: center;
            height: 100vh;
            background-image: url('cc.jpg');
        background-size: cover;
        color:white;
        font-size:16px;
        padding:0px;
       
        }

        .container .content {
            max-width: 900px;
            background-color: rgba(0, 0, 0, 0.7);
            padding:15px;
            
      
        }

        .container .content div {
            display: flex;
            align-items: center;
            margin-bottom: 10px;

        }

        .container .content label {
            width: 150px;
        }

        .container .content input[type="text"],
        .container .content button {
            margin-left: 10px;
             /* Yeni satır */
              

        }

        .container .content .grid-container {
            margin-bottom: 20px;
           
        }

        .container .content .grid-container table {
            margin-top: 10px;
            width: 300px; /* Gridview'in genişliği yüzde olarak ayarlanacak */
           
        }

        .container .content .button-container {
            display: flex;
            justify-content: space-between; /* Butonların sola hizalanması için */
            margin-top: 20px;


            
        }

        .container .content .button-container button {
            margin: 0; /* Butonlar arasında hiç boşluk olmayacak */

        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="content">
                <h2>Transfer İşlemleri</h2>
                <div>
                    <label for="txtIsim">İsim:</label>
                    <asp:TextBox ID="txtIsim" runat="server"></asp:TextBox>
                </div>
               
               
                <div>
                    <label for="txtbedel">Bedel:</label>
                    <asp:TextBox ID="txtbedel" runat="server"></asp:TextBox>
                </div>
                <div>
                    <label for="txttut">Tür:</label>
                    <asp:TextBox ID="txttur" runat="server"></asp:TextBox>
                </div>

                <div>
                    <label for="ddlLig">Lig:</label>
                    <asp:DropDownList ID="ddlLig" runat="server" AutoPostBack="True"></asp:DropDownList>
                </div>
                <div>
                    <label for="ddlyeniTakim">Yeni Takım:</label>
                    <asp:DropDownList ID="ddlyeniTakim" runat="server"></asp:DropDownList>
                </div>
                <div>
                    <label for="txtOyuncuid">Silmek / Güncellemek İstediğiniz Oyuncunun İD değeri:</label>
                    <asp:TextBox ID="txtoid" runat="server"></asp:TextBox>
                </div>
                <div class="button-container">
                    <asp:Button ID="btnEkle" runat="server" Text="Ekle"  OnClick="btnEkle_Click" />
                    <asp:Button ID="btnGuncelle" runat="server" Text="Güncelle" OnClick="btnGuncelle_Click" />
                    <asp:Button ID="btnSil" runat="server" Text="Sil" OnClick="btnSil_Click" />
                    <asp:Button ID="Btnllist" runat="server" Text="Listele" OnClick="Btnllist_Click1" />
                </div>
                <div class="grid-container">
                    <asp:GridView ID="gridtransfer" runat="server" AutoGenerateColumns="true"></asp:GridView>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
