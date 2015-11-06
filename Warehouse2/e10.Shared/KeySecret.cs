using System.Configuration;

namespace e10.Shared
{
    public class KeySecret
    {
        public string Key { get; set; }
        public string Secret { get; set; }
        public string Scope { get; set; }

        public KeySecret(string name)
        {
            this.Key = ConfigurationManager.AppSettings[string.Format("{0}Id", name)];
            this.Secret = ConfigurationManager.AppSettings[string.Format("{0}Secret", name)];
            this.Scope = ConfigurationManager.AppSettings[string.Format("{0}Scope", name)];
        }
    }

    public class KeySecretUri : KeySecret
    {
        public string Uri { get; set; }
        public KeySecretUri(string name) : base(name)
        {
            this.Uri = ConfigurationManager.AppSettings[string.Format("{0}Uri", name)];
        }
    }

    public class KeySecretUriCert : KeySecretUri
    {
        public string Certificate { get; set; }
        public string Password { get; set; }

        public KeySecretUriCert(string name) : base(name)
        {
            this.Certificate = ConfigurationManager.AppSettings[string.Format("{0}Certificate", name)];
            this.Password = ConfigurationManager.AppSettings[string.Format("{0}Password", name)];
        }
    }
}