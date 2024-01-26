using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace takimdeneme
{
    public partial class anasayfa : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

      
        protected void btnKayitOl_Click(object sender, EventArgs e)
        {
            Response.Redirect("kayit.aspx");

        }

        protected void btnGiris_Click(object sender, EventArgs e)
        {
            Response.Redirect("giris.aspx");
        }
    }
}