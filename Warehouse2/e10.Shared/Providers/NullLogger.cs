using System;
using System.Diagnostics;
using Microsoft.Owin.Logging;

namespace e10.Shared.Providers
{
    public class NullLogger : ILogger
    {
        public bool WriteCore(TraceEventType eventType, int eventId, object state, Exception exception, Func<object, Exception, string> formatter)
        {
            //do nothing
            return true;
        }
    }
}