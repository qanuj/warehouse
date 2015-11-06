using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e10.Shared
{
    public class OAuthWebConfigProvider
    {
        public KeySecret Twitter { get; set; }
        public KeySecret Facebook { get; set; }
        public KeySecret GooglePlus { get; set; }
        public KeySecret Microsoft { get; set; }
        public KeySecret LinkedIn { get; set; }
        public KeySecret GitHub { get; set; }
        public KeySecret Yahoo { get; set; }
        public KeySecretUriCert Xero { get; set; }
        public OAuthWebConfigProvider()
        {
            Twitter = new KeySecret("Twitter");
            Facebook = new KeySecret("Facebook");
            GooglePlus = new KeySecret("GooglePlus");
            Microsoft = new KeySecret("Microsoft");
            LinkedIn = new KeySecret("LinkedIn");
            GitHub = new KeySecret("GitHub");
            Yahoo = new KeySecret("Yahoo");
            Xero = new KeySecretUriCert("Xero");
        }
    }

}
