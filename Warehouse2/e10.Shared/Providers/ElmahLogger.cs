using System;
using System.Diagnostics;
using System.Web;
using Elmah;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Logging;

namespace e10.Shared.Providers
{
    public class ElmahLogger : ILogger
    {
        public bool WriteCore(TraceEventType eventType, int eventId, object state, Exception exception, Func<object, Exception, string> formatter)
        {
            if (exception != null)
            {
                ErrorLog.GetDefault(HttpContext.Current).Log(new Error(exception));
            }
            return true;
        }
    }
}
