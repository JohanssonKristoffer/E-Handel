using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace E_Handel
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void SendSearch_Click(object sender, EventArgs e)
        {
            Response.Redirect($"Result.aspx?search={SearchBox.Text}");
        }
    }
}