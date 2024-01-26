<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="transfer.aspx.cs" Inherits="takimdeneme.transfer" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Transfer</title><style>
                        body {
                   background-image: linear-gradient(rgba(0,0,0,0.0),rgba(0,0,0,0.8)),url(saha.jpg);
        background-size: cover;
        background-position: center;
        background-attachment: fixed;
      
        #drop{
           width: 40%;
    margin: auto auto;
    font-size: 16px;
     background-color: #8794D2;
     margin-left:230px;
      border-radius: 40px;
      color:white;
        }
        #btn {
            width: 200px;
    font-size: 16px;
    display: inline-block;
     margin: auto auto;
     background-color: #8794D2;
    text-align: center;
       text-decoration: none;
       border-radius: 5px;
            cursor: pointer;
             margin-left:40px;
              border-radius: 40px;
              color:white;
        }
        #grid {
        width: 70%;
        margin-left: auto;
        margin-right: auto;
        padding-left: 90px;
        padding-right: 90px;
        font-size: 16.5px;
        border: 5px solid #000000;
        color:#000000;
      
         background-color:white;

        }
        #grid tr th, #gridview tr td {
    border: 2px solid #000000;
        color: #333333;

}
        #grid tr:first-child {
    background-color: #FFFFCC;
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
 #grid {
        color: #000000;
        opacity:0.85;
            border-radius: 3%; /* Kareleri yuvarlak hale getirir */
    box-shadow: 0 0 10px rgba(0, 0, 0, 0.1); /* Hafif bir gölge ekler */
    overflow: hidden;
        
    </style>
</head>
<body>
    <form id="form1" runat="server">
               <div class="image-container" runat="server" id="imageContainer">
    <asp:Image runat="server" CssClass="image-box" ImageUrl="pl1.png" />
    <asp:Image runat="server" CssClass="image-box" ImageUrl="Bl.png" />
    <asp:Image runat="server" CssClass="image-box" ImageUrl="l1.png" />
    <asp:Image runat="server" CssClass="image-box" ImageUrl="Sa.png" />
    <asp:Image runat="server" CssClass="image-box" ImageUrl="isp.png" />
    <asp:Image runat="server" CssClass="image-box" ImageUrl="Sl.png" />
</div>
        <div>
            <asp:DropDownList runat="server" ID="drop"></asp:DropDownList>
            <asp:Button runat="server" ID="btn"  Text="Listele" OnClick="Unnamed1_Click"></asp:Button>
        </div>
        <div class="">&nbsp;</div>
        <div class="">
            <asp:GridView runat="server" ID="grid" OnSorting="grid_Sorting"></asp:GridView>
            &nbsp;</div>
    </form>
</body>
</html>
