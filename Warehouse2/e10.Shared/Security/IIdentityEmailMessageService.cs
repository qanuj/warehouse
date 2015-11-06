using System.Threading.Tasks;
using e10.Shared.Models;
using Microsoft.AspNet.Identity;

namespace e10.Shared.Security
{
    public interface IIdentityEmailMessageService : IIdentityMessageService
    {
        Task SendAsync(IdentityMessage message,params MessageAttachement[] attachments);
    }
}