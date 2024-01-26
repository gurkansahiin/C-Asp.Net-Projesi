using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls; 

namespace takimdeneme
{
    public partial class transfer : System.Web.UI.Page
    {
        footballEntities4 baglantii = new footballEntities4();

        protected void Page_Load(object sender, EventArgs e)
        {
          

            var takimlar = from t in baglantii.transfer
                           from yeni_takim in baglantii.takim.Where(takim => takim.takim_id == t.Yeni_takim_id)
                           from eski_takim in baglantii.takim.Where(takim => takim.takim_id == t.Eski_takim_id)
                           select new { İsim = t.İsim, yeni_takim = yeni_takim.Takım, eski_takim = eski_takim.Takım, Tür = t.Tür, Bedel = t.Bedel };


            grid.DataSource = takimlar.ToList();
            grid.DataBind();

            if (!Page.IsPostBack)
            {
                // DropdownList verileri yalnızca sayfa ilk kez yüklendiğinde yüklenecek
                var tur = (from t in baglantii.transfer
                               select t.Tür).Distinct().ToList();
                drop.DataSource = tur;
                drop.DataBind();
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

        protected void grid_Sorting(object sender, GridViewSortEventArgs e)
        {
            // Seçilen sütunun ismini ve sıralama yönünü alın
            string sortExpression = e.SortExpression;
            SortDirection sortDirection = e.SortDirection;

            // Sıralama yönü tersine çevirin, eğer önceki sıralama aynı sütuna yapılmışsa
            if (grid.SortExpression == sortExpression)
            {
                sortDirection = (grid.SortDirection == SortDirection.Ascending) ?
                    SortDirection.Descending : SortDirection.Ascending;
            }

            // Sıralama ölçütüne göre futbolcuları sıralayın
            var takimlar = from t in baglantii.transfer
                           from yeni_takim in baglantii.takim.Where(takim => takim.takim_id == t.Yeni_takim_id)
                           from eski_takim in baglantii.takim.Where(takim => takim.takim_id == t.Eski_takim_id)
                           select new { İsim = t.İsim, yeni_takim = yeni_takim.Takım, eski_takim = eski_takim.Takım, Tür = t.Tür, Bedel = t.Bedel };

            switch (sortExpression)
            {
                case "Bedel":
                    takimlar = (sortDirection == SortDirection.Ascending) ? takimlar.OrderBy(t => t.Bedel) : takimlar.OrderByDescending(t => t.Bedel);
                    break;
                default:
                    takimlar = (sortDirection == SortDirection.Ascending) ? takimlar.OrderBy(t => t.İsim) : takimlar.OrderByDescending(t => t.İsim);
                    break;
            }

            // GridView'e yeniden bağlanın ve sıralama özelliklerini güncelleyin
            grid.DataSource = takimlar.ToList();
            grid.DataBind();
            
        }
        protected void grid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grid.PageIndex = e.NewPageIndex;
            grid.DataBind();
        }

        protected void Unnamed1_Click(object sender, EventArgs e)
        {
            var secilentur = drop.SelectedValue;
            var takimlar = from t in baglantii.transfer
                           from yeni_takim in baglantii.takim.Where(takim => takim.takim_id == t.Yeni_takim_id)
                           from eski_takim in baglantii.takim.Where(takim => takim.takim_id == t.Eski_takim_id)
                           where t.Tür == secilentur
                           select new { İsim = t.İsim, yeni_takim = yeni_takim.Takım, eski_takim = eski_takim.Takım, Tür = t.Tür, Bedel = t.Bedel };


            grid.DataSource = takimlar.ToList();
            grid.DataBind();
        }
    }
}