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
    public partial class futbolcular1 : System.Web.UI.Page
    {


        protected void Page_Load(object sender, EventArgs e)
        {





            dropft1.SelectedIndexChanged += new EventHandler(dropft1_SelectedIndexChanged);

            if (!Page.IsPostBack)
            {
                using (SqlConnection baglanti = new SqlConnection(@"Data Source=.\MSSQLSERVER02;Initial Catalog=football;Integrated Security=True"))
                {
                    baglanti.Open();
                    SqlCommand cmd = new SqlCommand("SELECT DISTINCT adi FROM lig", baglanti);
                    SqlDataReader dr = cmd.ExecuteReader();

                    dropft1.DataSource = dr;
                    dropft1.DataTextField = "adi";
                    dropft1.DataValueField = "adi";
                    dropft1.DataBind();
                    baglanti.Close();
                }

                ListeleTakimlar(dropft1.SelectedValue);
            }
            if (!Page.IsPostBack)
            {
                int takimId = Convert.ToInt32(dropft2.SelectedValue);

                using (SqlConnection baglanti = new SqlConnection(@"Data Source=.\MSSQLSERVER02;Initial Catalog=football;Integrated Security=True"))
                {
                    baglanti.Open();
                    SqlCommand cmd = new SqlCommand("SELECT İsim, Mevki, [TZ Gol], [TZ Lig Maç], [Kullandığı Ayak], takim.Takım AS 'Takım', Ülke, Boy, Ağırlık, Yaş, [Piyasa Değeri] FROM futbolcular INNER JOIN takim ON futbolcular.Klup_id = takim.takim_id WHERE futbolcular.Klup_id = @takimId", baglanti);
                    cmd.Parameters.AddWithValue("@takimId", takimId);
                    SqlDataReader dr = cmd.ExecuteReader();

                    // Futbolcuları listeleyin
                    DataTable dt = new DataTable();
                    dt.Load(dr);
                    gridft.DataSource = dt;
                    gridft.DataBind();
                    baglanti.Close();
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


        }
        protected void dropft1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedLigAdi = dropft1.SelectedItem.Text;
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

                dropft2.DataSource = dr;
                dropft2.DataSource = dr;
                dropft2.DataSource = dr;
                dropft2.DataTextField = "Takım";
                dropft2.DataValueField = "takim_id";
                dropft2.DataBind();
                baglanti.Close();
            }
        }

        protected void Unnamed3_Click(object sender, EventArgs e)
        {
            int takimId = Convert.ToInt32(dropft2.SelectedValue);

            using (SqlConnection baglanti = new SqlConnection(@"Data Source=.\MSSQLSERVER02;Initial Catalog=football;Integrated Security=True"))
            {
                baglanti.Open();
                SqlCommand cmd = new SqlCommand("SELECT İsim, Mevki, [TZ Gol], [TZ Lig Maç], [Kullandığı Ayak], takim.Takım AS 'Takım', Ülke, Boy, Ağırlık, Yaş, [Piyasa Değeri] FROM futbolcular INNER JOIN takim ON futbolcular.Klup_id = takim.takim_id WHERE futbolcular.Klup_id = @takimId", baglanti);
                cmd.Parameters.AddWithValue("@takimId", takimId);
                SqlDataReader dr = cmd.ExecuteReader();

                // Futbolcuları listeleyin
                DataTable dt = new DataTable();
                dt.Load(dr);
                gridft.DataSource = dt;
                gridft.DataBind();
                baglanti.Close();
            }

        }



        protected void btnListele_Click(object sender, EventArgs e)
        {
            string arananIsim = searchInput.Text.Trim();

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



                gridft.DataSource = dt;
                gridft.DataBind();
            }
        }
    }



}

