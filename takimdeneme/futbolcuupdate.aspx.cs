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
    public partial class futbolcuupdate : System.Web.UI.Page
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





        protected void btnEkle_Click(object sender, EventArgs e)
        {
            Random random = new Random();
            SqlConnection baglanti = new SqlConnection(@"Data Source=.\MSSQLSERVER02;Initial Catalog=football;Integrated Security=True");
            baglanti.Open();

            futbolcular fpekle = new futbolcular();
            fpekle.İsim = txtIsim.Text;
            fpekle.Mevki = txtMevki.Text;
            fpekle.TZ_Gol = float.Parse(txtGol.Text);
            fpekle.TZ_Lig_Maç = float.Parse(txtLigMac.Text);
            fpekle.Kullandığı_Ayak = txtKullandigiAyak.Text;
            fpekle.Ülke = txtUlke.Text;
            fpekle.Boy = txtBoy.Text;
            fpekle.Ağırlık = txtAgirlik.Text;
            fpekle.Yaş = int.Parse(txtYas.Text);
            fpekle.Piyasa_Değeri = txtPiyasaDegeri.Text;
           
            int oyuncuID = random.Next(1, 100000); // 1 ile 100000 arasında rastgele bir sayı seçilir
            fpekle.OID = oyuncuID;


            float takimID = float.Parse(ddlTakim.SelectedValue);
            fpekle.Klup_id = takimID;

            SqlCommand komut = new SqlCommand("INSERT INTO futbolcular ([İsim], [Mevki], [TZ Gol], [TZ Lig Maç], [Kullandığı Ayak], [OID], [Klup_id], [Ülke], [Boy], [Ağırlık], [Yaş], [Piyasa Değeri]) VALUES (@Isim, @Mevki, @Gol, @LigMac, @KullandigiAyak, @OID, @KlupID, @Ulke, @Boy, @Agirlik, @Yas, @PiyasaDegeri)", baglanti);
            komut.Parameters.AddWithValue("@Isim", fpekle.İsim);
            komut.Parameters.AddWithValue("@Mevki", fpekle.Mevki);
            komut.Parameters.AddWithValue("@Gol", fpekle.TZ_Gol);
            komut.Parameters.AddWithValue("@LigMac", fpekle.TZ_Lig_Maç);
            komut.Parameters.AddWithValue("@KullandigiAyak", fpekle.Kullandığı_Ayak);
            komut.Parameters.AddWithValue("@OID", fpekle.OID);
            komut.Parameters.AddWithValue("@KlupID", fpekle.Klup_id);
            komut.Parameters.AddWithValue("@Ulke", fpekle.Ülke);
            komut.Parameters.AddWithValue("@Boy", fpekle.Boy);
            komut.Parameters.AddWithValue("@Agirlik", fpekle.Ağırlık);
            komut.Parameters.AddWithValue("@Yas", fpekle.Yaş);
            komut.Parameters.AddWithValue("@PiyasaDegeri", fpekle.Piyasa_Değeri);

            komut.ExecuteNonQuery();


         





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



        protected void Btnllist_Click1(object sender, EventArgs e)
        {


            string arananIsim = txtIsim.Text.Trim();

            using (SqlConnection baglanti = new SqlConnection(@"Data Source=.\MSSQLSERVER02;Initial Catalog=football;Integrated Security=True"))
            {
                baglanti.Open();

                SqlCommand cmd = new SqlCommand("SELECT futbolcular.*, takim.Takım FROM futbolcular INNER JOIN takim ON futbolcular.Klup_id = takim.takim_id WHERE [İsim] LIKE @ArananIsim", baglanti);
                cmd.Parameters.AddWithValue("@ArananIsim", "%" + arananIsim + "%");

                DataTable dt = new DataTable();
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(dt);
                }
                // Klüp ID sütununu kaldır
                dt.Columns.Remove("Klup_id");



                gridFutbolcular.DataSource = dt;
                gridFutbolcular.DataBind();
            }
        }

        protected void btnSil_Click(object sender, EventArgs e)
        {
            int oyuncuId = Convert.ToInt32(txtoid.Text);

            using (SqlConnection baglanti = new SqlConnection(@"Data Source=.\MSSQLSERVER02;Initial Catalog=football;Integrated Security=True"))
            {
                baglanti.Open();

                SqlCommand cmd = new SqlCommand("DELETE FROM futbolcular WHERE OID = @OyuncuId", baglanti);
                cmd.Parameters.AddWithValue("@OyuncuId", oyuncuId);
                cmd.ExecuteNonQuery();
                gridFutbolcular.DataBind();
                baglanti.Close();
            }

        }

        protected void btnGuncelle_Click(object sender, EventArgs e)
        {
            int oyuncuId = Convert.ToInt32(txtoid.Text);

            using (SqlConnection baglanti = new SqlConnection(@"Data Source=.\MSSQLSERVER02;Initial Catalog=football;Integrated Security=True"))
            {
                baglanti.Open();

                SqlCommand cmd = new SqlCommand("SELECT * FROM futbolcular WHERE OID = @OyuncuId", baglanti);
                cmd.Parameters.AddWithValue("@OyuncuId", oyuncuId);
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    string eskiIsim = dr["İsim"].ToString();
                    string eskiMevki = dr["Mevki"].ToString();
                    string eskiGol = dr["TZ Gol"].ToString();
                    string eskiMac = dr["TZ Lig Maç"].ToString();
                    string eskiAyak = dr["Kullandığı Ayak"].ToString();
                    string eskiUlke = dr["Ülke"].ToString();
                    string eskiBoy = dr["Boy"].ToString();
                    string eskiKg = dr["Ağırlık"].ToString();
                    string eskiYas = dr["Yaş"].ToString();
                    string eskiPd = dr["Piyasa Değeri"].ToString();
                    int eskiTakimId = Convert.ToInt32(dr["Klup_id"]);

                    dr.Close();

                    // Güncellenecek diğer verileri al
                    string yeniIsim = string.IsNullOrEmpty(txtIsim.Text) ? eskiIsim : txtIsim.Text;
                    string yenimevki = string.IsNullOrEmpty(txtMevki.Text) ? eskiMevki : txtMevki.Text;
                    string yenigol = string.IsNullOrEmpty(txtGol.Text) ? eskiGol : txtGol.Text;
                    string yenimaç = string.IsNullOrEmpty(txtLigMac.Text) ? eskiMac : txtLigMac.Text;
                    string yeniayak = string.IsNullOrEmpty(txtKullandigiAyak.Text) ? eskiAyak : txtKullandigiAyak.Text;
                    string yeniülke = string.IsNullOrEmpty(txtUlke.Text) ? eskiUlke : txtUlke.Text;
                    string yeniboy = string.IsNullOrEmpty(txtBoy.Text) ? eskiBoy : txtBoy.Text;
                    string yenikg = string.IsNullOrEmpty(txtAgirlik.Text) ? eskiKg : txtAgirlik.Text;
                    string yeniyas = string.IsNullOrEmpty(txtYas.Text) ? eskiYas : txtYas.Text;
                    string yenipd = string.IsNullOrEmpty(txtPiyasaDegeri.Text) ? eskiPd : txtPiyasaDegeri.Text;
                    int yeniTakimId = Convert.ToInt32(ddlTakim.SelectedValue);

                    cmd = new SqlCommand("UPDATE futbolcular SET İsim = @YeniIsim, Mevki = @YeniMevki, [TZ Gol] = @YeniGol, [TZ Lig Maç] = @YeniMac, [Kullandığı Ayak] = @YeniAyak, " +
                        "Ülke = @YeniUlke, Boy = @YeniBoy, Ağırlık = @YeniKg, Yaş = @YeniYas, [Piyasa Değeri] = @YeniPd, Klup_id = @YeniTakimId WHERE OID = @OyuncuId", baglanti);
                    cmd.Parameters.AddWithValue("@YeniIsim", yeniIsim);
                    cmd.Parameters.AddWithValue("@YeniMevki", yenimevki);
                    cmd.Parameters.AddWithValue("@YeniGol", yenigol);
                    cmd.Parameters.AddWithValue("@YeniMac", yenimaç);
                    cmd.Parameters.AddWithValue("@YeniAyak", yeniayak);
                    cmd.Parameters.AddWithValue("@YeniUlke", yeniülke);
                    cmd.Parameters.AddWithValue("@YeniBoy", yeniboy);
                    cmd.Parameters.AddWithValue("@YeniKg", yenikg);
                    cmd.Parameters.AddWithValue("@YeniYas", yeniyas);
                    cmd.Parameters.AddWithValue("@YeniPd", yenipd);
                    cmd.Parameters.AddWithValue("@YeniTakimId", yeniTakimId);
                    cmd.Parameters.AddWithValue("@OyuncuId", oyuncuId);
                    cmd.ExecuteNonQuery();

                    // GridView'i güncelle
                    gridFutbolcular.DataBind();
                }

                baglanti.Close();
            }
        }
    }
}