using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace e10.Shared.Security
{
    public class WhatsAppSmsService : IIdentitySmsMessageService
    {
        public WhatsAppSmsService()
        {
            
        }
        public Task SendAsync(IdentityMessage message)
        {
            var wapp = new WhatsAppApi.WhatsApp("9867763174", "", "Anuj", true, false);
            wapp.SendMessage(message.Destination, message.Body);
            return Task.FromResult(0);
        }
    }
}