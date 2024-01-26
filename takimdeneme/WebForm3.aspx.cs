using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data.Entity;
using System.Data;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Web.Services;

namespace takimdeneme
{
    public partial class WebForm3 : System.Web.UI.Page
    {
        private readonly string connectionString = @"Data Source=.\MSSQLSERVER02;Initial Catalog=football;Integrated Security=True";

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                // Sayfa ilk defa yüklendiğinde yapılacak işlemler


                if (Request.HttpMethod == "POST")
                {
                    int bakiyeUpdate = Convert.ToInt32(Request.Form["bakiyeUpdate"]);

                    int textBoxValue = Convert.ToInt32(Request.Form["textBoxValue"]);
                    string userEmail = Request.Form["userEmail"];

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        string updateQuery = "UPDATE uye SET bakiye = bakiye";

                        if (bakiyeUpdate > 0)
                        {
                            updateQuery += " + @bakiyeUpdate";
                        }
                        else if (bakiyeUpdate < 0)
                        {
                            updateQuery += "+ @bakiyeUpdate";
                        }

                        updateQuery += " WHERE e_mail = @userEmail";

                        using (SqlCommand command = new SqlCommand(updateQuery, connection))
                        {
                            command.Parameters.AddWithValue("@bakiyeUpdate", bakiyeUpdate);

                            command.Parameters.AddWithValue("@textBoxValue", textBoxValue);
                            command.Parameters.AddWithValue("@userEmail", userEmail);

                            command.ExecuteNonQuery();
                        }
                    }

                    // İşlem tamamlandı, 200 HTTP koduyla yanıt verin
                    Response.StatusCode = 200;
                    Response.End();
                }

                if (Request.QueryString["takim1ID"] != null && Request.QueryString["takim2ID"] != null)
                {
                    string takim1ID = Request.QueryString["takim1ID"];
                    string takim2ID = Request.QueryString["takim2ID"];
                    string secilenSonuc = Request.QueryString["secilenSonuc"];

                    string takim1Adi = GetTakimAdi(takim1ID);
                    string takim2Adi = GetTakimAdi(takim2ID);


                    evSahibiTakimLabel.Text = takim1Adi;
                    deplasmanTakimLabel.Text = takim2Adi;

                    // Futbolcuları getir ve gridview'e bağla
                    BindHomePlayers(takim1ID);
                    BindAwayPlayers(takim2ID);

                    // Diğer işlemleri burada yapabilirsiniz
                }
            }
        }

        private string GetTakimAdi(string takimID)
        {
            string takimAdi = "";

            string query = "SELECT Takım FROM takim WHERE takim_id = @TakimID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@TakimID", takimID);
                    connection.Open();
                    object result = command.ExecuteScalar();

                    if (result != null)
                    {
                        takimAdi = result.ToString();
                    }
                }
            }

            return takimAdi;
        }

        // Seçilen oyuncuların mevkilerini takip etmek için bir liste oluşturuluyor
        private void BindHomePlayers(string takimID)
        {
            const int MAX_PLAYERS = 11;

            string query = "SELECT OID, İsim, Mevki, [Piyasa Değeri] FROM " +
     "(SELECT TOP 1 OID, İsim, Mevki, [Piyasa Değeri] FROM futbolcular WHERE Klup_id = @TakimID AND Mevki = 'K' " +
     "UNION ALL " +
     "SELECT TOP 3 OID, İsim, Mevki, [Piyasa Değeri] FROM futbolcular WHERE Klup_id = @TakimID AND Mevki = 'D (M)' " +
     "AND OID NOT IN (SELECT TOP " + MAX_PLAYERS + " OID FROM futbolcular WHERE Klup_id = @TakimID) " +
     "UNION ALL " +
     "SELECT TOP 2 OID, İsim, Mevki, [Piyasa Değeri] FROM futbolcular WHERE Klup_id = @TakimID AND Mevki LIKE '%ST %' " +
     "AND OID NOT IN (SELECT TOP " + MAX_PLAYERS + " OID FROM futbolcular WHERE Klup_id = @TakimID) " +
     "UNION ALL " +
     "SELECT TOP 5 OID, İsim, Mevki, [Piyasa Değeri] FROM futbolcular WHERE Klup_id = @TakimID AND Mevki LIKE '%OS%' " +
     "AND Mevki NOT LIKE '%ST%' " +
     "AND OID NOT IN (SELECT TOP " + MAX_PLAYERS + " OID FROM futbolcular WHERE Klup_id = @TakimID)) AS Players " +
     "ORDER BY " +
     "CASE WHEN Mevki = 'K' THEN 1 " +
     "     WHEN Mevki = 'D (M)' THEN 2 " +
     "     WHEN Mevki LIKE '%ST %' THEN 3 " +
     "     WHEN Mevki LIKE '%OS%' AND Mevki NOT LIKE '%ST%' THEN 4 " +
     "     WHEN Mevki = 'OOS (M)' THEN 5 END";





            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@TakimID", takimID);
                    connection.Open();

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        // Kontrol ediyoruz, maksimum 11 oyuncu ise veriyi kesiyoruz
                        if (dt.Rows.Count >= MAX_PLAYERS)
                        {

                            DataTable truncatedDt = dt.AsEnumerable().Take(MAX_PLAYERS).CopyToDataTable();
                            gridviewHomePlayers.DataSource = truncatedDt;
                        }
                        else
                        {
                            string additionalQuery = "SELECT TOP " + (MAX_PLAYERS - dt.Rows.Count) + " OID, İsim, Mevki, [Piyasa Değeri] FROM futbolcular " +
                    "WHERE Klup_id = @TakimID AND Mevki <> 'K' AND OID NOT IN (SELECT TOP " + MAX_PLAYERS + " OID FROM futbolcular WHERE Klup_id = @TakimID) " +
                    "ORDER BY NEWID()"; // Rastgele bir sıralama yapmak için NEWID() kullanılıyor
                            gridviewHomePlayers.DataSource = dt;
                        }

                        gridviewHomePlayers.DataBind();
                    }
                }
            }
        }


        private void BindAwayPlayers(string takimID)
        {
            const int MAX_PLAYERS = 11;

            string query = "SELECT OID, İsim, Mevki, [Piyasa Değeri] FROM " +
     "(SELECT TOP 1 OID, İsim, Mevki, [Piyasa Değeri] FROM futbolcular WHERE Klup_id = @TakimID AND Mevki = 'K' " +
     "UNION ALL " +
     "SELECT TOP 3 OID, İsim, Mevki, [Piyasa Değeri] FROM futbolcular WHERE Klup_id = @TakimID AND Mevki = 'D (M)' " +
     "AND OID NOT IN (SELECT TOP " + MAX_PLAYERS + " OID FROM futbolcular WHERE Klup_id = @TakimID) " +
     "UNION ALL " +
     "SELECT TOP 2 OID, İsim, Mevki, [Piyasa Değeri] FROM futbolcular WHERE Klup_id = @TakimID AND Mevki LIKE '%ST %' " +
     "AND OID NOT IN (SELECT TOP " + MAX_PLAYERS + " OID FROM futbolcular WHERE Klup_id = @TakimID) " +
     "UNION ALL " +
     "SELECT TOP 5 OID, İsim, Mevki, [Piyasa Değeri] FROM futbolcular WHERE Klup_id = @TakimID AND Mevki LIKE '%OS%' " +
     "AND Mevki NOT LIKE '%ST%' " +
     "AND OID NOT IN (SELECT TOP " + MAX_PLAYERS + " OID FROM futbolcular WHERE Klup_id = @TakimID)) AS Players " +
     "ORDER BY " +
     "CASE WHEN Mevki = 'K' THEN 1 " +
     "     WHEN Mevki = 'D (M)' THEN 2 " +
     "     WHEN Mevki LIKE '%ST %' THEN 3 " +
     "     WHEN Mevki LIKE '%OS%' AND Mevki NOT LIKE '%ST%' THEN 4 " +
     "     WHEN Mevki = 'OOS (M)' THEN 5 END";





            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@TakimID", takimID);
                    connection.Open();

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        // Kontrol ediyoruz, maksimum 11 oyuncu ise veriyi kesiyoruz
                        if (dt.Rows.Count >= MAX_PLAYERS)
                        {

                            DataTable truncatedDt = dt.AsEnumerable().Take(MAX_PLAYERS).CopyToDataTable();
                            gridviewAwayPlayers.DataSource = truncatedDt;
                        }
                        else
                        {
                            string additionalQuery = "SELECT TOP " + (MAX_PLAYERS - dt.Rows.Count) + " OID, İsim, Mevki, [Piyasa Değeri] FROM futbolcular " +
                    "WHERE Klup_id = @TakimID AND Mevki <> 'K' AND OID NOT IN (SELECT TOP " + MAX_PLAYERS + " OID FROM futbolcular WHERE Klup_id = @TakimID) " +
                    "ORDER BY NEWID()"; // Rastgele bir sıralama yapmak için NEWID() kullanılıyor
                            gridviewAwayPlayers.DataSource = dt;
                        }

                        gridviewAwayPlayers.DataBind();
                    }
                }
            }
        }

        protected void btnStartMatch_Click(object sender, EventArgs e)
        {
        //    double elapsedTime = 0;
        //    double matchDuration = 91;
        //    double increment = 10;
        //    string matchResult = Request.QueryString["matchResult"];

        //    // Set the text of the lbldeneme label
        //    lbldeneme.Text = matchResult;

        //    // Get the values from session variables
        //    string radioSelection = Session["RadioSelection"].ToString();
        //    int textBoxValue = Convert.ToInt32(Session["TextBoxValue"]);
        //    string userEmail = Session["mail"].ToString();

        //    while (elapsedTime < matchDuration)
        //    {
        //        // elapsedTime increment
        //        elapsedTime += increment;

        //        if (elapsedTime >= matchDuration)
        //        {
        //            // Update the balance in the database
        //            using (SqlConnection connection = new SqlConnection(connectionString))
        //            {
        //                connection.Open();
        //                int bakiyeUpdate = 0;

        //                if (lbldeneme.Text.ToLower() == radioSelection.ToLower())
        //                {
        //                    bakiyeUpdate += textBoxValue;
        //                }
        //                else
        //                {
        //                    bakiyeUpdate -= textBoxValue;
        //                }

        //                string updateQuery = string.Empty;

        //                if (bakiyeUpdate != 0)
        //                {
        //                    updateQuery = "UPDATE uye SET bakiye = bakiye + @bakiyeUpdate WHERE e_mail = @userEmail";
        //                }

        //                if (!string.IsNullOrEmpty(updateQuery))
        //                {
        //                    using (SqlCommand command = new SqlCommand(updateQuery, connection))
        //                    {
        //                        command.Parameters.AddWithValue("@bakiyeUpdate", Math.Abs(bakiyeUpdate));
        //                        command.Parameters.AddWithValue("@userEmail", userEmail);

        //                        command.ExecuteNonQuery();
        //                    }
        //                }
        //            }
        //        }
        //        else
        //        {
        //            // elapsedTime is less than matchDuration, perform any necessary actions here
        //            // ...
        //        }

        //        // Sleep between each iteration
        //        System.Threading.Thread.Sleep(100);
        //    }
        }




    }
}
