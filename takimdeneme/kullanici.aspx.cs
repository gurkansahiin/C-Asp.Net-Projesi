using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data.Entity;
using System.Data;
using System.Xml.Linq;
namespace takimdeneme
{
    public partial class kullanici : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["mail"] != null)
                {
                    string userEmail = Session["mail"].ToString();
                    SqlConnection baglanti = new SqlConnection(@"Data Source=.\MSSQLSERVER02;Initial Catalog=football;Integrated Security=True");
                    baglanti.Open();
                    SqlCommand cmd = new SqlCommand("SELECT ad, e_mail FROM uye WHERE e_mail = @Email", baglanti);
                    cmd.Parameters.AddWithValue("@Email", userEmail);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        txtName.Text = reader["ad"].ToString();
                        txtEmail.Text = reader["e_mail"].ToString();
                    }
                    reader.Close();
                    baglanti.Close();
                }
            }

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string userEmail = Session["mail"].ToString();
            string updatedAd = txtName.Text.Trim();
            string updatedEmail = txtEmail.Text.Trim();
            string updatesifre = txtPassword.Text.Trim();


            if (string.IsNullOrEmpty(updatedAd) || string.IsNullOrEmpty(updatedEmail))
            {
                // Gerekli alanları doldurunuz hatası
                return;
            }

            // Veritabanına güncelleme işlemini gerçekleştir
            SqlConnection baglanti = new SqlConnection(@"Data Source=.\MSSQLSERVER02;Initial Catalog=football;Integrated Security=True");
            baglanti.Open();
            SqlCommand cmd = new SqlCommand("UPDATE uye SET ad = @Ad, e_mail = @Email , sifre = @sifre WHERE e_mail = @UserEmail", baglanti);
            cmd.Parameters.AddWithValue("@Ad", updatedAd);
            cmd.Parameters.AddWithValue("@Email", updatedEmail);
            cmd.Parameters.AddWithValue("@sifre", updatesifre);
            cmd.Parameters.AddWithValue("@UserEmail", userEmail);
            int rowsAffected = cmd.ExecuteNonQuery();
            baglanti.Close();

            if (rowsAffected > 0)
            {
                // Güncelleme başarılı
                Response.Redirect("panel.aspx");
            }
            else
            {
                // Güncelleme başarısız
                // Hata mesajını göster veya işlemi yeniden dene
            }
        }
    }
}