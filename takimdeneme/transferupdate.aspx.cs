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
    public partial class transferupdate : System.Web.UI.Page
    {
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

                ddlyeniTakim.DataSource = dr;
                ddlyeniTakim.DataTextField = "Takım";
                ddlyeniTakim.DataValueField = "takim_id";
                ddlyeniTakim.DataBind();
                baglanti.Close();
            }
        }
        protected void btnEkle_Click(object sender, EventArgs e)
        {
            Random random = new Random();
            SqlConnection baglanti = new SqlConnection(@"Data Source=.\MSSQLSERVER02;Initial Catalog=football;Integrated Security=True");
            baglanti.Open();

            transfer fpekle = new transfer();
            fpekle.İsim = txtIsim.Text;
            fpekle.Bedel = txtbedel.Text;
            fpekle.Tür = txttur.Text;

            string oyuncuIsim = txtIsim.Text; // Oyuncunun ismini girdiğiniz giriş alanının adına göre değiştirin

            // Oyuncunun ID'sini veritabanından isme göre alın
            SqlCommand oyuncuIdKomut = new SqlCommand("SELECT [OID] FROM futbolcular WHERE [İsim] = @Isim", baglanti);
            oyuncuIdKomut.Parameters.AddWithValue("@Isim", oyuncuIsim);
            int oyuncuId = (int)oyuncuIdKomut.ExecuteScalar();

            // Mevcut takım ID'sini veritabanından alın
            SqlCommand eskiTakimKomut = new SqlCommand("SELECT [Klup_id] FROM futbolcular WHERE [OID] = @OID", baglanti);
            eskiTakimKomut.Parameters.AddWithValue("@OID", oyuncuId);
            int eskiTakimId = (int)eskiTakimKomut.ExecuteScalar();

            fpekle.Eski_takim_id = eskiTakimId;
            int yenitakimid = int.Parse(ddlyeniTakim.SelectedValue);
            fpekle.Yeni_takim_id = yenitakimid;

            // futbolcular tablosundaki Klup_id değerine Yeni_takim_id değerini atayın
            SqlCommand klupIdGuncelleKomut = new SqlCommand("UPDATE futbolcular SET [Klup_id] = @Yeni_takim_id WHERE [OID] = @OID", baglanti);
            klupIdGuncelleKomut.Parameters.AddWithValue("@Yeni_takim_id", yenitakimid);
            klupIdGuncelleKomut.Parameters.AddWithValue("@OID", oyuncuId);
            klupIdGuncelleKomut.ExecuteNonQuery();
            fpekle.o_id = oyuncuId;
            SqlCommand komut = new SqlCommand("INSERT INTO transfer ([İsim], [o_id], [Yeni_takim_id], [Eski_takim_id], [Tür], [Bedel]) VALUES (@Isim, @o_id, @Yeni_takim_id, @Eski_takim_id, @Tür, @Bedel)", baglanti);
            komut.Parameters.AddWithValue("@Isim", fpekle.İsim);
            komut.Parameters.AddWithValue("@Bedel", fpekle.Bedel);
            komut.Parameters.AddWithValue("@Tür", fpekle.Tür);
            komut.Parameters.AddWithValue("@o_id", fpekle.o_id);
            komut.Parameters.AddWithValue("@Yeni_takim_id", fpekle.Yeni_takim_id);
            komut.Parameters.AddWithValue("@Eski_takim_id", fpekle.Eski_takim_id);
            
            komut.ExecuteNonQuery();

        }

        protected void btnGuncelle_Click(object sender, EventArgs e)
        {
            int oyuncuId = Convert.ToInt32(txtoid.Text);

            string yeniTakimId = ddlyeniTakim.SelectedValue;
            string bedel = txtbedel.Text;
            string tur = txttur.Text;

            using (SqlConnection baglanti = new SqlConnection(@"Data Source=.\MSSQLSERVER02;Initial Catalog=football;Integrated Security=True"))
            {
                baglanti.Open();

                // Mevcut takım ID'sini veritabanından alın
                SqlCommand eskiTakimKomut = new SqlCommand("SELECT [Eski_takim_id] FROM transfer WHERE [o_id] = @OyuncuId", baglanti);
                eskiTakimKomut.Parameters.AddWithValue("@OyuncuId", oyuncuId);
                int eskiTakimId = (int)eskiTakimKomut.ExecuteScalar();

                if (yeniTakimId != eskiTakimId.ToString())
                {
                    // Yeni takım ile eski takım farklı ise sadece ilgili alanları güncelle
                    SqlCommand cmd = new SqlCommand("UPDATE transfer SET [Yeni_takim_id] = @YeniTakimId", baglanti);
                    cmd.Parameters.AddWithValue("@YeniTakimId", yeniTakimId);

                    if (!string.IsNullOrEmpty(bedel))
                    {
                        cmd.CommandText += ", [Bedel] = @Bedel";
                        cmd.Parameters.AddWithValue("@Bedel", bedel);
                    }

                    if (!string.IsNullOrEmpty(tur))
                    {
                        cmd.CommandText += ", [Tür] = @Tur";
                        cmd.Parameters.AddWithValue("@Tur", tur);
                    }

                    cmd.CommandText += " WHERE [o_id] = @OyuncuId";
                    cmd.Parameters.AddWithValue("@OyuncuId", oyuncuId);
                    cmd.ExecuteNonQuery();

                    // futbolcular tablosundaki Klup_id değerini güncelle
                    SqlCommand klupIdGuncelleKomut = new SqlCommand("UPDATE futbolcular SET [Klup_id] = @YeniTakimId WHERE [OID] = @OyuncuId", baglanti);
                    klupIdGuncelleKomut.Parameters.AddWithValue("@YeniTakimId", yeniTakimId);
                    klupIdGuncelleKomut.Parameters.AddWithValue("@OyuncuId", oyuncuId);
                    klupIdGuncelleKomut.ExecuteNonQuery();
                }
                else
                {
                    // Yeni takım ile eski takım aynı ise tüm alanları güncelle
                    SqlCommand cmd = new SqlCommand("UPDATE transfer SET [İsim] = @Isim, [Yeni_takim_id] = @YeniTakimId", baglanti);
                    cmd.Parameters.AddWithValue("@Isim", txtIsim.Text);
                    cmd.Parameters.AddWithValue("@YeniTakimId", yeniTakimId);

                    if (!string.IsNullOrEmpty(bedel))
                    {
                        cmd.CommandText += ", [Bedel] = @Bedel";
                        cmd.Parameters.AddWithValue("@Bedel", bedel);
                    }

                    if (!string.IsNullOrEmpty(tur))
                    {
                        cmd.CommandText += ", [Tür] = @Tur";
                        cmd.Parameters.AddWithValue("@Tur", tur);
                    }

                    cmd.CommandText += " WHERE [o_id] = @OyuncuId";
                    cmd.Parameters.AddWithValue("@OyuncuId", oyuncuId);
                    cmd.ExecuteNonQuery();
                }

                // Gridview'i güncelle
                gridtransfer.DataBind();
            }

        }

        protected void btnSil_Click(object sender, EventArgs e)
        {
            int oyuncuId = Convert.ToInt32(txtoid.Text);

            using (SqlConnection baglanti = new SqlConnection(@"Data Source=.\MSSQLSERVER02;Initial Catalog=football;Integrated Security=True"))
            {
                baglanti.Open();

                SqlCommand cmd = new SqlCommand("DELETE FROM transfer WHERE o_id = @OyuncuId", baglanti);
                cmd.Parameters.AddWithValue("@OyuncuId", oyuncuId);
                cmd.ExecuteNonQuery();
                gridtransfer.DataBind();
                baglanti.Close();
            }

        }

        protected void Btnllist_Click1(object sender, EventArgs e)
        {
            string arananIsim = txtIsim.Text.Trim();

            using (SqlConnection baglanti = new SqlConnection(@"Data Source=.\MSSQLSERVER02;Initial Catalog=football;Integrated Security=True"))
            {
                baglanti.Open();

                SqlCommand cmd = new SqlCommand(@"SELECT transfer.*, futbolcular.İsim, takim.Takım AS 'Yeni Takım', eskiTakim.Takım AS 'Eski Takım'
                                    FROM transfer
                                    INNER JOIN futbolcular ON transfer.o_id = futbolcular.OID
                                    INNER JOIN takim ON transfer.Yeni_takim_id = takim.takim_id
                                    INNER JOIN takim AS eskiTakim ON transfer.Eski_takim_id = eskiTakim.takim_id
                                    WHERE futbolcular.İsim LIKE @ArananIsim", baglanti);
                cmd.Parameters.AddWithValue("@ArananIsim", "%" + arananIsim + "%");

                DataTable dt = new DataTable();
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(dt);
                }
                dt.Columns.Remove("transfer_id");

                gridtransfer.DataSource = dt;
                gridtransfer.DataBind();
                foreach (DataRow row in dt.Rows)
                {
                    int oyuncuId = Convert.ToInt32(row["o_id"]);
                    int eskiTakimId = Convert.ToInt32(row["Eski_takim_id"]);

                    SqlCommand klupIdGuncelleKomut = new SqlCommand("UPDATE futbolcular SET [Klup_id] = @EskiTakimId WHERE [OID] = @OyuncuId", baglanti);
                    klupIdGuncelleKomut.Parameters.AddWithValue("@EskiTakimId", eskiTakimId);
                    klupIdGuncelleKomut.Parameters.AddWithValue("@OyuncuId", oyuncuId);
                    klupIdGuncelleKomut.ExecuteNonQuery();
                }
            }

        }
    }
}