using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace takimdeneme
{
    public partial class kayit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnKayitOl_Click(object sender, EventArgs e)
        {
            DataSet_kayitTableAdapters.uyeTableAdapter  dt= new DataSet_kayitTableAdapters.uyeTableAdapter();
            dt.uyeekle(username.Text, email.Text, password.Text);
        }
    }
}