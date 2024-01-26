using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI.WebControls;

namespace takimdeneme
{
    public partial class soru : System.Web.UI.Page
    {
        List<string> soruResimleri;
        List<List<string>> cevaplar;
        int soruIndex;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Soruları veritabanından al ve göster
                soruResimleri = new List<string>();
                cevaplar = new List<List<string>>();
                soruIndex = 0;

                using (SqlConnection baglanti = new SqlConnection(@"Data Source=.\MSSQLSERVER02;Initial Catalog=football;Integrated Security=True"))
                {
                    string sorgu = "SELECT soru_image, cevap FROM sorular";
                    SqlCommand komut = new SqlCommand(sorgu, baglanti);
                    baglanti.Open();

                    SqlDataReader reader = komut.ExecuteReader();
                    while (reader.Read())
                    {
                        soruResimleri.Add(reader["soru_image"].ToString());

                        List<string> cevapSecenekleri = new List<string>();
                        for (int i = 1; i < reader.FieldCount; i++)
                        {
                            cevapSecenekleri.Add(reader[i].ToString());
                        }
                        cevaplar.Add(cevapSecenekleri);
                    }
                    reader.Close();
                }

                ViewState["soruResimleri"] = soruResimleri;
                ViewState["cevaplar"] = cevaplar;
                ViewState["soruIndex"] = soruIndex;

                // İlk soruyu göster
                if (soruResimleri.Count > 0)
                {
                    soruResim.ImageUrl = soruResimleri[0];
                    BindCevapSecenekleri(0);
                }

                // Kullanıcının bakiyesini al ve label'a yazdır
                if (Session["mail"] != null)
                {
                    string userEmail = Session["mail"].ToString();
                    int kullaniciBakiye = GetBakiye(userEmail);
                    bakiye.Text = "Bakiye: " + kullaniciBakiye.ToString();
                }
            }
        }

        protected void ileriButton_Click(object sender, EventArgs e)
        {
            List<string> soruResimleri = (List<string>)ViewState["soruResimleri"];
            List<List<string>> cevaplar = (List<List<string>>)ViewState["cevaplar"];
            int soruIndex = (int)ViewState["soruIndex"];

            string secilenCevap = secenekler.SelectedValue;
            List<string> dogruCevaplar = cevaplar[soruIndex];

            // Doğru cevap kontrolü yapabilirsiniz
            bool dogruCevap = dogruCevaplar.Contains(secilenCevap);

            // Bakiye arttırma işlemleri
            if (dogruCevap)
            {
                string userEmail = Session["mail"].ToString();
                int bakiyeArtis = 20;
                UpdateBakiye(userEmail, bakiyeArtis);
            }

            int yeniBakiye = GetBakiye(Session["mail"].ToString());

            // Bakiye labelini güncelleyin ve doğru/yanlış cevaba göre CSS sınıfını ekleyin
            bakiye.Text = "Bakiye: " + yeniBakiye.ToString();
            bakiye.CssClass = dogruCevap ? "dogru-cevap" : "yanlis-cevap";

            // JavaScript ile belirli bir süre sonra CSS sınıfını kaldırın
            ClientScript.RegisterStartupScript(this.GetType(), "ResetBakiyeColor", "setTimeout(function() { document.getElementById('" + bakiye.ClientID + "').classList.remove('dogru-cevap', 'yanlis-cevap'); }, 1000);", true);

            // Diğer cevapları da göstermek için RadioButtonList'i doldur
            BindCevapSecenekleri(soruIndex);

            // Yeni soruya geçme
            soruIndex++;
            if (soruIndex < soruResimleri.Count)
            {
                ViewState["soruIndex"] = soruIndex;
                secenekler.ClearSelection();
                soruResim.ImageUrl = soruResimleri[soruIndex];
            }
            else
            {
                // Sorular tamamlandı, istediğiniz işlemleri yapabilirsiniz
            }
        }

        private void BindCevapSecenekleri(int index)
        {
            List<List<string>> cevaplar = (List<List<string>>)ViewState["cevaplar"];
            secenekler.Items.Clear();

            // Tüm şıkları bir listeye ekleyelim
            List<string> tumSecenekler = new List<string>();
            foreach (var cevapSecenekleri in cevaplar)
            {
                tumSecenekler.AddRange(cevapSecenekleri);
            }

            // Şıkları karıştırarak her şıktan yalnızca birini seçelim
            Random random = new Random();
            List<string> secenekListesi = tumSecenekler.OrderBy(x => random.Next()).Distinct().ToList();

            // Tüm şıkları listele
            foreach (string cevap in secenekListesi)
            {
                secenekler.Items.Add(new ListItem(cevap, cevap));
            }

        
         
        }



        private void UpdateBakiye(string userEmail, int bakiyeArtis)
        {
            using (SqlConnection baglanti = new SqlConnection(@"Data Source=.\MSSQLSERVER02;Initial Catalog=football;Integrated Security=True"))
            {
                string sorgu = "UPDATE uye SET bakiye = bakiye + @artis WHERE e_mail = @mail";
                SqlCommand komut = new SqlCommand(sorgu, baglanti);
                komut.Parameters.AddWithValue("@artis", bakiyeArtis);
                komut.Parameters.AddWithValue("@mail", userEmail);

                baglanti.Open();
                komut.ExecuteNonQuery();
            }
        }

        private int GetBakiye(string userEmail)
        {
            int bakiye = 0;
            using (SqlConnection baglanti = new SqlConnection(@"Data Source=.\MSSQLSERVER02;Initial Catalog=football;Integrated Security=True"))
            {
                string sorgu = "SELECT bakiye FROM uye WHERE e_mail = @mail";
                SqlCommand komut = new SqlCommand(sorgu, baglanti);
                komut.Parameters.AddWithValue("@mail", userEmail);

                baglanti.Open();
                SqlDataReader reader = komut.ExecuteReader();
                if (reader.Read())
                {
                    bakiye = Convert.ToInt32(reader["bakiye"]);
                }
                reader.Close();
            }

            return bakiye;
        }

        protected void menü_Click(object sender, EventArgs e)
        {
            // Panel.aspx sayfasına yönlendirme işlemi
            Response.Redirect("panel.aspx");
        }

    }
}
