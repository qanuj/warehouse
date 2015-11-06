using System;
using System.Configuration;

namespace e10.Shared.Emaily.Config
{
    public class EmailyConfig
    {
        public string InstallationUrl => ConfigurationManager.AppSettings["Emaily.Server"];
        public string ApiKey => ConfigurationManager.AppSettings["Emaily.ApiKey"];
        public string SubscriptionListId => ConfigurationManager.AppSettings["Emaily.List"];
    }
}
