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
using System.EnterpriseServices;

namespace takimdeneme
{
    public partial class dene : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadMatches();

                if (Session["mail"] != null)
                {
                    string userEmail = Session["mail"].ToString();
                    string connectionString = @"Data Source=.\MSSQLSERVER02;Initial Catalog=football;Integrated Security=True";

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        SqlCommand cmd = new SqlCommand("SELECT bakiye, e_mail FROM uye WHERE e_mail = @Email", connection);
                        cmd.Parameters.AddWithValue("@Email", userEmail);
                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            lblBalance.Text = reader["bakiye"].ToString();
                        }

                        reader.Close();
                    }
                }
            }
        }

        protected void LoadMatches()
        {
            string connectionString = @"Data Source=.\MSSQLSERVER02;Initial Catalog=football;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT TOP 4 Takim1.takim_id AS Takim1Id, Takim2.takim_id AS Takim2Id, " +
                    "Takim1.Takım AS Takim1, Takim2.Takım AS Takim2 " +
                    "FROM takim Takim1, takim Takim2 " +
                    "WHERE Takim1.takim_id <> Takim2.takim_id " +
                    "AND Takim1.lig_id <> 1 AND Takim2.lig_id <> 1 " + // Türkiye dışındaki ligleri filtrele
                    "ORDER BY NEWID()";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        rptSonuclar.DataSource = reader;
                        rptSonuclar.DataBind();
                    }
                }
            }
        }

        protected void btnOyna_Click(object sender, EventArgs e)
        {
            Button btnOyna1 = (Button)sender;
            RepeaterItem item = (RepeaterItem)btnOyna1.NamingContainer;
            Label lblTakim1Id = (Label)item.FindControl("lblTakim1Id");
            Label lblTakim2Id = (Label)item.FindControl("lblTakim2Id");
            TextBox txtBox = (TextBox)item.FindControl("txtBox");
            RadioButton rbTakim1 = (RadioButton)item.FindControl("rbTakim1");
            RadioButton rbBerabere = (RadioButton)item.FindControl("rbBerabere");
            RadioButton rbTakim2 = (RadioButton)item.FindControl("rbTakim2");

            string takim1Id = lblTakim1Id.Text;
            string takim2Id = lblTakim2Id.Text;
            int textBoxValue =Convert.ToInt32(txtBox.Text);
            string radioSelection = string.Empty;

            if (rbTakim1.Checked)
                radioSelection = "Ev Sahibi Takım";
            else if (rbBerabere.Checked)
                radioSelection = "Berabere";
            else if (rbTakim2.Checked)
                radioSelection = "Deplasman Takım";
            if(textBoxValue != 0 )
            {
                Session.Add("textBoxValue",textBoxValue);
                Session.Add("radioSelection", radioSelection);
            }
            // Verileri WebForm3.aspx'e aktar
            Response.Redirect("WebForm3.aspx?takim1Id=" + takim1Id + "&takim2Id=" + takim2Id /*+ "&textBoxValue=" + textBoxValue + "&radioSelection=" + radioSelection*/);
        }


        protected void btnMenuyeDon_Click(object sender, EventArgs e)
        {
            // Redirect to your menu page
            Response.Redirect("panel.aspx");
        }


    }
}
    

