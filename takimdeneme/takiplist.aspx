<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="takiplist.aspx.cs" Inherits="takimdeneme.takiplist" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css" />
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Futbolcular</title> 
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css" />
   <style>
    body {
                   background-image: linear-gradient(rgba(0,0,0,0.0),rgba(0,0,0,0.6)),url(saha.jpg);
        background-size: cover;
        background-position: center;
        background-attachment: fixed;
      
    }
      .search-container {
            position: fixed;
            top: 10px;
            right: 10px;
            display: flex;
            align-items: center;
            background-color: #3F3F3F;
            height: 40px;
            padding: 8px;
            border-radius: 40px;

        }

        #searchInput {
            width: 0px;
            padding: 5px;
            color: white;
            background-color: transparent;
            border: none;
            border-radius: 40px;
            margin-left:10px;
            transition:1s;
            
        }

        #search-icon {
            position: absolute;
            top: 50%;
            right: 40px;
            transform: translateY(-50%);
            pointer-events: none;
            color: white;
            transition: transform 0.3s ease;
        }

        #search-input:focus + #search-icon {
            transform: translateY(-50%) rotate(90deg);
        }

        #search-button {
            display: block;
            margin-left: 5px;
            padding: 5px 10px;
            background-color: #4CAF50;
            color: white;
            border: none;
            cursor: pointer;
        }

        #search-results {
            display: none;
            position: absolute;
            top: 100%;
            right: 10px;
            z-index: 1;
            width: 200px;
            background-color: #fff;
            border: 1px solid #ccc;
            padding: 10px;

        }

        #btnListele {
            display: block;
            margin-top: 100px;
            padding: 5px 10px;
            background-color:  #3F3F3F;
            color: white;
            border: none;
            cursor: pointer;
            border-radius: 40px;
            padding:10px;
            
        }
        .search-container:hover > #searchInput{
             width: 200px;
        }


    #dropft1 {
        width: 20%;
        margin: auto auto;
        font-size: 16px;
        background-color: #8794D2;
        margin-left: 300px;
        color: white;
        border-radius: 40px;
    }

    #dropft2 {
        width: 20%;
        margin: auto auto;
        font-size: 16px;
        background-color: #8794D2;
        margin-left: 20px;
        color: white;
         border-radius: 40px;
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
        margin-left: 40px;
        color: white;
         border-radius: 40px;
        
    }

    #gridft {
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

    #gridft tr th,
    #gridview tr td {
        border: 2px solid #000000;
        color: #333333;

    }

    #gridft tr:first-child {
        background-color: #FFFFCC;

      
    }
    #gridft tr th {
    border-bottom-width: 5px;
     
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
        border-radius: 25%; /* Kareleri yuvarlak hale getirir */
    box-shadow: 0 0 10px rgba(0, 0, 0, 0.1); /* Hafif bir gölge ekler */
       
    }

    .centered-image {
        max-width: 100%;
        max-height: 100%;
        object-fit: contain;
    }

    #gridft {
        color: black;
        opacity:0.85;
            border-radius: 3%; /* Kareleri yuvarlak hale getirir */
    box-shadow: 0 0 10px rgba(0, 0, 0, 0.1); /* Hafif bir gölge ekler */
    overflow: hidden;
        
    }
</style>

</head>
<body>
    <form id="form1" runat="server">
         <div class="search-container">
            <asp:TextBox ID="searchInput" runat="server" placeholder="Futbolcu Arayın..."></asp:TextBox>
             <i id="search-icon" class="fas fa-search"></i>
             <asp:Button ID="btnListele" runat="server" Text="Listele" CssClass="button"  />
        </div>
        <div id="Div1" runat="server"></div>
                <div class="image-container" runat="server" id="imageContainer">
    <asp:Image runat="server" CssClass="image-box" ImageUrl="pl1.png" />
    <asp:Image runat="server" CssClass="image-box" ImageUrl="Bl.png" />
    <asp:Image runat="server" CssClass="image-box" ImageUrl="l1.png" />
    <asp:Image runat="server" CssClass="image-box" ImageUrl="Sa.png" />
    <asp:Image runat="server" CssClass="image-box" ImageUrl="isp.png" />
    <asp:Image runat="server" CssClass="image-box" ImageUrl="Sl.png" />
</div>
        <div>
        </div>
        <div class="wlp-whitespace-only-element-expansion">
            <asp:DropDownList runat="server" ID="dropft1"  AutoPostBack="true"></asp:DropDownList>
<%--                <asp:DropDownList ID="dropft1" runat="server" AutoPostBack="true" OnSelectedIndexChanged="dropft1_SelectedIndexChanged"></asp:DropDownList>--%>


            <asp:DropDownList runat="server" ID="dropft2"> </asp:DropDownList>

          <asp:Button runat="server" ID="btn" Text="Listele" ></asp:Button>&nbsp;</div>
        <div class="wlp-whitespace-only-element-expansion">&nbsp;</div>
        <div class="wlp-whitespace-only-element-expansion">&nbsp;</div>
        <div class="wlp-whitespace-only-element-expansion">
          <asp:GridView runat="server" ID="gridft" AutoGenerateColumns="true" AutoGenerateSelectButton="True"></asp:GridView>


           &nbsp;</div>
    </form>
</body>
</html>






</html>
