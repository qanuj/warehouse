using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using e10.Shared.Providers;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Logging;

namespace e10.Shared.Security
{
    public class SmsService : IIdentitySmsMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            return Task.FromResult(0);
        }
    }
}