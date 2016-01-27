using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace E_Handel.ErrorPages
{
    public partial class ErrorDefault : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
                ViewState["PreviousPage"] = Request.UrlReferrer;
            if (Request.QueryString["logId"] != null)
                ReportErrorButton.OnClientClick = $"window.open('mailto:support@kkg.tbi?subject=An error has occurred with LogId = {Request.QueryString["logId"]}', 'email')";
        }

        protected void GoBackErrorButton_OnClick(object sender, EventArgs e)
        {
            if (ViewState["PreviousPage"] != null)
                Response.Redirect(ViewState["PreviousPage"].ToString());
        }
    }
}