using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data.Entity;
using System.Data;


namespace takimdeneme
{
    public partial class takimupdate : System.Web.UI.Page
    {
        SqlConnection baglanti = new SqlConnection(@"Data Source=.\MSSQLSERVER02;Initial Catalog=football;Integrated Security=True");

        protected void Page_Load(object sender, EventArgs e)
        {
            ddlLig.SelectedIndexChanged += new EventHandler(ddlLig_SelectedIndexChanged);

            if (!Page.IsPostBack)
            {
                using (SqlConnection baglanti = new SqlConnection(@"Data Source=.\MSSQLSERVER02;Initial Catalog=football;Integrated Security=True"))
                {
                    baglanti.Open();
                    SqlCommand cmd = new SqlCommand("SELECT DISTINCT adi FROM lig", baglanti);
                    SqlDataReader dr = cmd.ExecuteReader();

                    ddlLig.DataSource = dr;
                    ddlLig.DataTextField = "adi";
                    ddlLig.DataValueField = "adi";
                    ddlLig.DataBind();
                    baglanti.Close();
                }

                ListeleTakimlar(ddlLig.SelectedValue);
            }

        }
        protected void ddlLig_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedLigAdi = ddlLig.SelectedItem.Text;
            ListeleTakimlar(selectedLigAdi);
        }
        protected void ListeleTakimlar(string ligAdi)
        {
            using (SqlConnection baglanti = new SqlConnection(@"Data Source=.\MSSQLSERVER02;Initial Catalog=football;Integrated Security=True"))
            {
                baglanti.Open();
                SqlCommand cmd = new SqlCommand("SELECT takim_id, Takım FROM takim INNER JOIN lig ON takim.lig_id = lig.lig_id WHERE lig.adi = @ligAdi", baglanti);
                cmd.Parameters.AddWithValue("@ligAdi", ligAdi);
                SqlDataReader dr = cmd.ExecuteReader();

                ddlTakim.DataSource = dr;
                ddlTakim.DataTextField = "Takım";
                ddlTakim.DataValueField = "takim_id";
                ddlTakim.DataBind();
                baglanti.Close();
            }
        }

        public string GetTakimAdi(int takimId)
        {
            string takimAdi = string.Empty;

            using (SqlConnection baglanti = new SqlConnection(@"Data Source=.\MSSQLSERVER02;Initial Catalog=football;Integrated Security=True"))
            {
                baglanti.Open();

                SqlCommand cmd = new SqlCommand("SELECT Takım FROM takim WHERE takim_id = @TakimId", baglanti);
                cmd.Parameters.AddWithValue("@TakimId", takimId);
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    takimAdi = dr.GetString(0);
                }

                baglanti.Close();
            }

            return takimAdi;
        }
        protected void btnEkle_Click(object sender, EventArgs e)
        {
            float ligid;
            Random random = new Random();
            SqlConnection baglanti = new SqlConnection(@"Data Source=.\MSSQLSERVER02;Initial Catalog=football;Integrated Security=True");
            baglanti.Open();
            takim fpekle = new takim();
            fpekle.Takım = txtIsim.Text;
            fpekle.ulke = txtUlke.Text;

            int takimID = random.Next(1, 100000); // 1 ile 100000 arasında rastgele bir sayı seçilir
            fpekle.takim_id = takimID;

            string selectedLigAdi = ddlLig.SelectedItem.Text;
            using (SqlConnection ligBaglanti = new SqlConnection(@"Data Source=.\MSSQLSERVER02;Initial Catalog=football;Integrated Security=True"))
            {
                ligBaglanti.Open();
                SqlCommand ligCmd = new SqlCommand("SELECT lig_id FROM lig WHERE adi = @ligAdi", ligBaglanti);
                ligCmd.Parameters.AddWithValue("@ligAdi", selectedLigAdi);
                object ligIdObj = ligCmd.ExecuteScalar();
                if (ligIdObj != null && !DBNull.Value.Equals(ligIdObj))
                {
                    ligid = Convert.ToSingle(ligIdObj);
                    fpekle.lig_id = ligid;

                    SqlCommand komut = new SqlCommand("INSERT INTO takim ([takim_id], [lig_id], [Takım], [ulke]) VALUES (@takimID, @ligID, @Takım, @ulke)", baglanti);
                    komut.Parameters.AddWithValue("@takimID", fpekle.takim_id);
                    komut.Parameters.AddWithValue("@ligID", fpekle.lig_id);
                    komut.Parameters.AddWithValue("@Takım", fpekle.Takım);
                    komut.Parameters.AddWithValue("@ulke", fpekle.ulke);

                    komut.ExecuteNonQuery();
                }
                else
                {
                    // Geçerli bir lig kaydı bulunamadı, hata işlemleri yapılabilir.
                }
                ligBaglanti.Close();
            }

            baglanti.Close();


        }

        protected void btnGuncelle_Click(object sender, EventArgs e)
        {;
            int takimId = Convert.ToInt32(txttid.Text);
            string yeniIsim = txtIsim.Text;
            string yeniUlke = txtUlke.Text;

            using (SqlConnection baglanti = new SqlConnection(@"Data Source=.\MSSQLSERVER02;Initial Catalog=football;Integrated Security=True"))
            {
                try
                {
                    baglanti.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE takim SET Takım = @yeniIsim, ulke = @yeniUlke WHERE takim_id = @takimId", baglanti);
                    cmd.Parameters.AddWithValue("@yeniIsim", yeniIsim);
                    cmd.Parameters.AddWithValue("@yeniUlke", yeniUlke);
                    cmd.Parameters.AddWithValue("@takimId", takimId);
                    int affectedRows = cmd.ExecuteNonQuery();
                    if (affectedRows > 0)
                    {
                        gridtakimlar.DataBind();
                        Response.Write("Başarılı güncelleme işlemi gerçekleşti");
                    }
                    else
                    {
                        Response.Write("Güncelleme işlemi başarısız");
                    }
                }
                catch (Exception ex)
                {
                    // Hata mesajını işleyebilir veya kaydedebilirsiniz
                    Response.Write("Hata oluştu: " + ex.Message);
                }
                finally
                {
                    baglanti.Close();
                }
            }
        }







        protected void btnSil_Click(object sender, EventArgs e)
        {
            int takimid = Convert.ToInt32(txttid.Text);

            using (SqlConnection baglanti = new SqlConnection(@"Data Source=.\MSSQLSERVER02;Initial Catalog=football;Integrated Security=True"))
            {
                baglanti.Open();

                SqlCommand cmd = new SqlCommand("DELETE FROM takim WHERE takim_id = @takimid", baglanti);
                cmd.Parameters.AddWithValue("@takimid", takimid);
                cmd.ExecuteNonQuery();
                gridtakimlar.DataBind();
                baglanti.Close();
            }
        }

        protected void ddlLig_SelectedIndexChanged1(object sender, EventArgs e)
        {

        }

        protected void ddlTakim_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void Btnllist_Click1(object sender, EventArgs e)
        {
            string selectedTakimId = ddlTakim.SelectedValue;

            using (SqlConnection baglanti = new SqlConnection(@"Data Source=.\MSSQLSERVER02;Initial Catalog=football;Integrated Security=True"))
            {
                baglanti.Open();

                SqlCommand cmd = new SqlCommand("SELECT * FROM takim WHERE takim_id = @TakimID", baglanti);
                cmd.Parameters.AddWithValue("@TakimID", selectedTakimId);

                DataTable dt = new DataTable();
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(dt);
                }

                gridtakimlar.DataSource = dt;
                gridtakimlar.DataBind();
            }
        }

        protected void gridtakimlar_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}