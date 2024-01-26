using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace takimdeneme
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        footballEntities baglanti=new footballEntities();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            //var takimlar = (from t in baglanti.takim
            //                join l in baglanti.lig on t.lig_id equals l.lig_id
                         
            //                select new { Takım = t.Takım, Lig = l.adi, Ülke = t.ulke }).ToList();

            //gridview.DataSource = takimlar;
            //gridview.DataBind();

            if (!Page.IsPostBack)
            {
                // DropdownList verileri yalnızca sayfa ilk kez yüklendiğinde yüklenecek
                var ulkeler = (from t in baglanti.takim
                               select t.ulke).Distinct().ToList();
                drop.DataSource = ulkeler;
                drop.DataBind();
                var secilenUlke = drop.SelectedValue;
                var takimlar = (from t in baglanti.takim
                                join l in baglanti.lig on t.lig_id equals l.lig_id
                                where t.ulke == secilenUlke
                                select new { Takım = t.Takım, Lig = l.adi, Ülke = t.ulke }).ToList();

                gridview.DataSource = takimlar;
                gridview.DataBind();
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



        }




        protected void Unnamed1_Click(object sender, EventArgs e)
        {

            
                var secilenUlke = drop.SelectedValue;
                var takimlar = (from t in baglanti.takim
                                join l in baglanti.lig on t.lig_id equals l.lig_id
                                where t.ulke == secilenUlke
                                select new { Takım = t.Takım, Lig = l.adi, Ülke = t.ulke }).ToList();

                gridview.DataSource = takimlar;
                gridview.DataBind();
            

        }
    }
    }
