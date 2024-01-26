<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="kullaniciupdate.aspx.cs" Inherits="takimdeneme.kullanıcıupdate" %>



<!DOCTYPE html>
<html lang="tr">
<head>
  <meta charset="UTF-8">
  <meta name="viewport" content="width=device-width, initial-scale=1.0">
  <title>Profil Bilgileri</title>
  <style>
    .profile-form {
      width: 400px;
      margin: 0 auto;
      padding: 20px;
      background-color: #f1f1f1;
      border: 1px solid #ddd;
      border-radius: 5px;
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
      width: 100%;
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
      color: #fff;
      font-weight: bold;
      border: none;
      border-radius: 3px;
      cursor: pointer;
    }

    .submit-button:hover {
      background-color: #3c3f7e;
    }
  </style>
</head>
<body>
  <form id="form1" runat="server">
    <div class="profile-form">
      <h2>Profil Bilgileri</h2>
      <div class="form-group">
        <label for="txtName">Ad:</label>
          <asp:TextBox ID="txtName" runat="server"  placeholder="Adınızı girin"></asp:TextBox>
      </div>
      <div class="form-group">
        <label for="txtEmail">E-posta:</label>
          <asp:TextBox ID="txtEmail" runat="server" placeholder="E-posta adresinizi girin"></asp:TextBox>
      </div>
      <div class="form-group">
        <label for="txtPassword">Şifre:</label>
        <asp:TextBox ID="txtPassword" runat="server" CssClass="input-field" TextMode="Password" placeholder="Şifrenizi girin" required></asp:TextBox>
        <span id="Span1" runat="server" visible="false">Şifre en az 6 karakter olmalıdır.</span>
      </div>
      <div class="form-group">
        <label for="txtConfirmPassword">Şifre (Tekrar):</label>
        <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="input-field" TextMode="Password" placeholder="Şifrenizi tekrar girin" required></asp:TextBox>
      </div>
      <asp:Button ID="btnSubmit" runat="server" Text="Bilgileri Güncelle" CssClass="submit-button" OnClick="btnSubmit_Click" />
    </div>
  </form>
 
</body>
</html>
