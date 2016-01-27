using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            Exception error = Server.GetLastError();
            string eventLogName = "KKG-ErrorLog";

            HttpException httpError = error as HttpException;
            Response.Clear();
            string errorPage;
            switch (httpError.GetHttpCode())
            {
                case 404:
                    errorPage = "404.aspx";
                    break;
                default:
                    long logId = CreateLogEntry(error, eventLogName);
                    errorPage = "ErrorDefault.aspx?logId=" + logId;
                    break;
            }
            Server.ClearError();
            Response.Redirect($"/ErrorPages/{errorPage}");
        }

        private long CreateLogEntry(Exception error, string eventLogName)
        {
            long logId = 0;
            if (!EventLog.SourceExists(eventLogName))
                EventLog.CreateEventSource(eventLogName, eventLogName);
            EventLog log = new EventLog { Source = eventLogName };
            if (log.Entries.Count > 0)
                logId = log.Entries[log.Entries.Count - 1].InstanceId + 1;
            log.WriteEntry(error.ToString(), EventLogEntryType.Error, (int)logId);
            return logId;
        }
    }
}