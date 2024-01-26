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
    public partial class kullanıcıupdate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
             if (!IsPostBack)
    {
        if (Session["email"] != null)
        {
            string userEmail = Session["email"].ToString();
            SqlConnection baglanti = new SqlConnection(@"Data Source=.\MSSQLSERVER02;Initial Catalog=football;Integrated Security=True");
            baglanti.Open();
            SqlCommand cmd = new SqlCommand("SELECT ad, e_mail FROM uye WHERE e_mail = @Email", baglanti);
            cmd.Parameters.AddWithValue("@Email", userEmail);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                txtAd.Text = reader["ad"].ToString();
                txtEmail.Text = reader["e_mail"].ToString();
            }
            reader.Close();
            baglanti.Close();
        }
    }



        }

       
    }
}