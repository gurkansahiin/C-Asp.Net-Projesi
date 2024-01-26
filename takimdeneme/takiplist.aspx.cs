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
    public partial class takiplist : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGridView();
            }
        }

        private void BindGridView()
        {
            // GridView'i veri kaynağına bağla
            DataTable dt = GetYourData(); // Veri kaynağınızdan verileri getirin
            gridft.DataSource = dt;
            gridft.DataBind();
        }

        private DataTable GetYourData()
        {
            // Veri kaynağınızdan verileri getirin ve bir DataTable nesnesine doldurun
            DataTable dt = new DataTable();
            // Veri kaynağından verileri dt DataTable'ına yükleyin
            return dt;
        }

        protected void Unnamed3_Click(object sender, EventArgs e)
        {

        }
    }
}