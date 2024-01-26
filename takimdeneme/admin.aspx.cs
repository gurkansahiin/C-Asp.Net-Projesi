using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace takimdeneme
{
    public partial class admin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void loginButton_Click(object sender, EventArgs e)
        {
            SqlConnection baglanti = new SqlConnection(@"Data Source=.\MSSQLSERVER02;Initial Catalog=football;Integrated Security=True");
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select * From admin where kullanıcı_adi=@P1 AND sifre=@P2", baglanti);
            komut.Parameters.AddWithValue("@P1", kullanıcı.Text);
            komut.Parameters.AddWithValue("@P2", password.Text);
            SqlDataReader dr = komut.ExecuteReader();
            if (dr.Read())
            {
                Response.Redirect("https://localhost:44364/adminpanel.aspx");
            }
            else
            {
                loginForm.Visible = false; // Hide the login form

                // Display error message in white color and centered
                string errorMessage = "Hatalı Veri Girişi";
                string styledErrorMessage = "<p style='color: white; text-align: center;'>" + errorMessage + "</p>";
                Response.Write(styledErrorMessage);
            }
            baglanti.Close();
        }
    }
}