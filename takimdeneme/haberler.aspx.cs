using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace takimdeneme
{
    public partial class haberler : System.Web.UI.Page
    {
        List<string> haberResimleri;
        int haberindex;
        bool normalGorunum = false;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                // Soruları veritabanından al ve göster
                haberResimleri = new List<string>();
                haberindex = 0;
                string userEmail = Session["mail"].ToString();
                int kullaniciBakiye = GetBakiye(userEmail);
                bakiye.Text = "Bakiye: " + kullaniciBakiye.ToString();

                using (SqlConnection baglanti = new SqlConnection(@"Data Source=.\MSSQLSERVER02;Initial Catalog=football;Integrated Security=True"))
                {
                    string sorgu = "SELECT haber_image FROM haberler";
                    SqlCommand komut = new SqlCommand(sorgu, baglanti);
                    baglanti.Open();

                    SqlDataReader reader = komut.ExecuteReader();
                    while (reader.Read())
                    {
                        haberResimleri.Add(reader["haber_image"].ToString());
                    }
                    reader.Close();
                }

                ViewState["soruResimleri"] = haberResimleri;
                ViewState["soruIndex"] = haberindex;

                // İlk soruyu göster
                if (haberResimleri.Count > 0)
                {
                    haberResim.ImageUrl = haberResimleri[0];
                }
                // Resimlerin listesini alın
                List<string> images = new List<string>();
                images.Add("pl1.png");
                images.Add("Bl.png");
                images.Add("l1.png");
                images.Add("Sa.png");
                images.Add("isp.png");
                images.Add("Sl.png");

                // Ortak boyutu bulmak için ilk resmi kullanın
                System.Drawing.Image firstImage = System.Drawing.Image.FromFile(Server.MapPath(images[0]));
                int targetWidth = 100; // Hedef genişlik
                int targetHeight = 100; // Hedef yükseklik
                float ratio = Math.Min((float)targetWidth / firstImage.Width, (float)targetHeight / firstImage.Height);
                int width = (int)(firstImage.Width * ratio);
                int height = (int)(firstImage.Height * ratio);
                firstImage.Dispose();

                // Resimleri döngüyle ekleyin
                int counter = 0; // Resim sayacı
                HashSet<string> addedImages = new HashSet<string>();

                foreach (string image in images)
                {
                    if (counter >= 0)
                        break;

                    if (!addedImages.Contains(image))
                    {
                        // Resim kutusunu oluşturun
                        System.Web.UI.WebControls.Panel imageBox = new System.Web.UI.WebControls.Panel();
                        imageBox.CssClass = "image-box";
                        imageBox.Width = targetWidth;
                        imageBox.Height = targetHeight;

                        // Resmi oluşturun ve özelliklerini ayarlayın
                        System.Web.UI.WebControls.Image img = new System.Web.UI.WebControls.Image();
                        img.ImageUrl = image;
                        img.CssClass = "centered-image"; // Resimlere uygulanacak CSS sınıfı
                        img.Width = width;
                        img.Height = height;

                        // Resmi resim kutusuna ekleyin
                        imageBox.Controls.Add(img);

                        // Resim kutusunu div içine ekleyin
                        imageContainer.Controls.Add(imageBox);

                        // Resmi eklediğimizi işaretlemek için addedImages setine ekleyin
                        addedImages.Add(image);

                        counter++;


                    }
                }

            }
            else
            {
                if (Request.Form["__EVENTTARGET"] == "menü")
                {
                    menü_Click(sender, e);
                }
                // Postback olduğunda normal görünümü kontrol et
                if (normalGorunum)
                {
                    // Normal görünümü göster
                    haberResim.CssClass = "normal-view";
                }
                else
                {
                    // Bulanık görünümü göster
                    haberResim.CssClass = "";
                }

                if (Session["mail"] != null)
                {
                    string userEmail = Session["mail"].ToString();
                    int kullaniciBakiye = GetBakiye(userEmail);
                    bakiye.Text = "Bakiye: " + kullaniciBakiye.ToString();
                }
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

        private void UpdateBakiye(string userEmail, int bakiyeAzalis)
        {
            using (SqlConnection baglanti = new SqlConnection(@"Data Source=.\MSSQLSERVER02;Initial Catalog=football;Integrated Security=True"))
            {
                string sorgu = "UPDATE uye SET bakiye = bakiye - @azalis WHERE e_mail = @mail";
                SqlCommand komut = new SqlCommand(sorgu, baglanti);
                komut.Parameters.AddWithValue("@azalis", bakiyeAzalis);
                komut.Parameters.AddWithValue("@mail", userEmail);

                baglanti.Open();
                komut.ExecuteNonQuery();
            }
        }


        protected void ileriButton_Click(object sender, EventArgs e)
        {
            List<string> soruResimleri = (List<string>)ViewState["soruResimleri"];
            int haberIndex = (int)ViewState["soruIndex"];

            // Yeni soruya geçme
            haberIndex++;
            if (haberIndex < soruResimleri.Count)
            {
                ViewState["soruIndex"] = haberIndex;
                haberResim.ImageUrl = soruResimleri[haberIndex];
                normalGorunum = false; // Bulanık görünümü ayarla
                
            }
            else
            {
                // Sorular tamamlandı, istediğiniz işlemleri yapabilirsiniz
            }
            //if (Session["mail"] != null)
            //{
            //    int bakiyeAzalis = 30;
            //    string userEmail = Session["mail"].ToString();
            //    int kullaniciBakiye = GetBakiye(userEmail);

            //    if (kullaniciBakiye >= bakiyeAzalis)
            //    {
            //        // Bakiye yeterli, düşme işlemini gerçekleştir
            //        UpdateBakiye(userEmail, bakiyeAzalis);


            //        int yeniBakiye = GetBakiye(Session["mail"].ToString());

            //        // Bakiye labelini güncelleyin ve doğru/yanlış cevaba göre CSS sınıfını ekleyin
            //        bakiye.Text = "Bakiye: " + yeniBakiye.ToString();

            //        // Diğer işlemleri yap
            //    }
            //    else
            //    {
            //        // Bakiye yetersiz, hata mesajını göster veya gerekli işlemleri yap
            //    }
            //}
            string script = "<script>toggleMenu();</script>";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "ToggleMenuScript", script);
        }


        protected void menü_Click(object sender, EventArgs e)
        {
            if (Session["mail"] != null)
            {
                int bakiyeAzalis = 30;
                string userEmail = Session["mail"].ToString();
                int kullaniciBakiye = GetBakiye(userEmail);

                if (kullaniciBakiye >= bakiyeAzalis)
                {
                    // Bakiye yeterli, düşme işlemini gerçekleştir
                    UpdateBakiye(userEmail, bakiyeAzalis);


                    int yeniBakiye = GetBakiye(Session["mail"].ToString());

                    // Bakiye labelini güncelleyin ve doğru/yanlış cevaba göre CSS sınıfını ekleyin
                    bakiye.Text = "Bakiye: " + yeniBakiye.ToString();

                    // Diğer işlemleri yap
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ToggleViewScript", "toggleView();", true);
                }
                else
                {
                    // Bakiye yetersiz, hata mesajını göster veya gerekli işlemleri yap
                }
            }

        
            
        }

        protected void menuyedon_Click(object sender, EventArgs e)
        {
            Response.Redirect("panel.aspx");
        }
    }
}
