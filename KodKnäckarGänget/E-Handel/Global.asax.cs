using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace E_Handel
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            HttpException error = Server.GetLastError() as HttpException;
            Response.Clear();
            string errorPage;
            switch (error.GetHttpCode())
            {
                case 404:
                    errorPage = "404";
                    break;
                default:
                    errorPage = "ErrorDefault";
                    break;
            }
            Server.ClearError();
            Response.Redirect($"/ErrorPages/{errorPage}.aspx");
        }
    }
}