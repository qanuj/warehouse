using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e10.Shared.Providers
{
    public interface IEmailConfigProvider
    {
        string From { get; }
        string Name { get; }
        string Server { get; }
        int Port { get; }
        bool IsGmail { get; }
        string Password { get;}
        string SendGridApiKey { get;}
    }
}
