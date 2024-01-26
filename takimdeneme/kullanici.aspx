<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="kullanici.aspx.cs" Inherits="takimdeneme.kullanici" %>

<!DOCTYPE html>
<html lang="tr">
<head>
  <meta charset="UTF-8">
  <meta name="viewport" content="width=device-width, initial-scale=1.0">
  <title>Profil Bilgileri</title>
  <style>
          * {
            font-family: sans-serif;
            padding: 0;
            margin: 0;
            position:center;
            background-size: cover;

        }
   body {
    height: 100%;
    width: 100%;
    background-image: linear-gradient(rgba(0,0,0,0.0),rgba(0,0,0,0.1)),url(giris.jpg);
    background-position: center ;
    background-size: cover;
    display: flex;
    justify-content: center;
    align-items: center;
    color:white;
  
    
}


    .profile-form {
      width: 450px;
      margin: 0 auto;
      padding: 20px;
      background-color: #f1f1f1;
       margin-top: 50px;
      border: 1px solid #ddd;
      border-radius: 5px;
      background: rgba(0,0,0,0.8);
      border-radius:40px;
    }

    .profile-form h2 {
      text-align: center;
      margin-bottom: 20px;
    }

    .form-group {
      margin-bottom: 15px;
    }

    label {
      display: block;
      font-weight: bold;
      margin-bottom: 5px;
    }

    .input-field {
      width: 75%;
      padding: 8px;
      border-radius: 3px;
      border: 1px solid #ccc;
    }

    #password-error {
      color: red;
      font-size: 12px;
      margin-top: 5px;
    }

    .submit-button {
      display: block;
      width: 100%;
      padding: 10px;
      margin-top: 20px;
      background-color: #4b4e9e;
      color: white;
      font-weight: bold;
      border: none;
      border-radius: 3px;
      cursor: pointer;
    }

    .submit-button:hover {
      background-color: #3c3f7e;
     
            transition: 1s;
    }
  </style>
</head>
<body>
  <form id="form1" runat="server">
    <div class="profile-form">
      <h2>Profil Bilgileri</h2>
      <div class="form-group">
        <label for="txtName">Ad:</label>
          <asp:TextBox ID="txtName" runat="server" CssClass="input-field"  placeholder="Adınızı girin"></asp:TextBox>
      </div>
      <div class="form-group">
        <label for="txtEmail">E-posta:</label>
          <asp:TextBox ID="txtEmail" runat="server" CssClass="input-field" placeholder="E-posta adresinizi girin"></asp:TextBox>
      </div>
      <div class="form-group">
        <label for="txtPassword">Şifre:</label>
        <asp:TextBox ID="txtPassword" runat="server" CssClass="input-field" TextMode="Password" placeholder="Şifrenizi girin" minlength="6" required></asp:TextBox>
        <span id="Span1" runat="server" visible="false">Şifre en az 6 karakter olmalıdır.</span>
      </div>
      <div class="form-group">
        <label for="txtConfirmPassword">Şifre (Tekrar):</label>
        <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="input-field" TextMode="Password" placeholder="Şifrenizi tekrar girin"  minlength="6" required></asp:TextBox>
      </div>
      <asp:Button ID="btnSubmit" runat="server" Text="Bilgileri Güncelle" CssClass="submit-button" OnClick="btnSubmit_Click" />
    </div>
  </form>
 
</body>
</html>

