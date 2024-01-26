<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="dene.aspx.cs" Inherits="takimdeneme.dene" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Takım Deneme</title>
    <style>
        body {
            font-style: initial;
                 height: 100%;
            width: 100%;
            background-image: linear-gradient(rgba(0,0,0,0.0),rgba(0,0,0,0.5)),url(st1.jpg);
            background-position: center;
            background-size: cover;
        }

        .button-container {
            display: flex;
            align-items: flex-start;
            margin-top: -20px;
            width:30px;
            border-radius:15px;



        }


        .textbox-container {
            margin-left: 90px;
            padding-top: 10px;
            border-radius:20px;
            text-align:center;
        }

        .item-container {
            display: flex;
            flex-direction: column;
        }

        .container {
            display: flex;
            flex-direction: column;
            padding: 80px;
            font-size: 18px;
            margin-top: -20px;
            background-color: #f1f1f1;
            border-radius: 5px;
            margin: 0 auto;
            max-width: 400px;
            max-height: 575px;
            background: rgba(0,0,0,0.8);
            color: #FFFFFF;
            border-radius: 40px;
        }

        .team-name {
            font-weight: bold;
            margin-bottom: 10px;
            font-size:20px;
              font-weight: bold;
        }

        .balance-container {
            display: flex;
            justify-content: flex-end;
            font-size: 20px;
            color:red;
            padding-top:-30px;
            font-weight: bold;
          padding-left:30px;
          margin-right:-20px;
        }

    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div style="position: absolute; top: 10px; left: 10px;">
    <asp:Button ID="btnMenuyeDon" runat="server" Text="<- Menüye Dön" OnClick="btnMenuyeDon_Click" CssClass="btn btn-primary" />
</div>
        <div class="container">
            <h1>Günün Maçları</h1>
            <div class="balance-container">
                Kullanıcının Bakiyesi: <asp:Label ID="lblBalance" runat="server" Text="0" />
            </div>
         <asp:Repeater ID="rptSonuclar" runat="server">

    <ItemTemplate>
        <div>
            <span class="team-name"><%# Eval("Takim1") %> VS <%# Eval("Takim2") %></span>
            <br />
            <asp:RadioButton ID="rbTakim1" runat="server" GroupName="secim" Text="Ev Sahibi Takım" />
            <asp:RadioButton ID="rbBerabere" runat="server" GroupName="secim" Text="Berabere" />
            <asp:RadioButton ID="rbTakim2" runat="server" GroupName="secim" Text="Deplasman Takım" />
            <div class="textbox-container">
                <asp:TextBox ID="txtBox" runat="server" class="textbox-container" ></asp:TextBox>

            </div>
            <div class="button-container">
                <asp:Button ID="btnOyna" runat="server" Text="Oyna" CommandArgument='<%# Container.ItemIndex %>' OnClick="btnOyna_Click" />
            </div>
            <br />
            <br />
            <br />
            <div class="takim-container">
                <asp:Label ID="lblTakim1Id" runat="server" Text='<%# Eval("Takim1Id") %>' Visible="false" />
                <asp:Label ID="lblTakim2Id" runat="server" Text='<%# Eval("Takim2Id") %>' Visible="false" />
            </div>
        </div>
    </ItemTemplate>
</asp:Repeater>

        </div>
    </form>
</body>
</html>