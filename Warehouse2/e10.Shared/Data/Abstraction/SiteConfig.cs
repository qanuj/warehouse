using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Policy;

namespace e10.Shared.Data.Abstraction
{
    public enum MailerEnum
    {
        None = 0,
        Emaily = 1,
        SendGrid = 2,
        MailGun = 3
    }

    [ComplexType]
    public class EmailCampaignConfig
    {
        public MailerEnum Mode { get; set; }
        public string ApiKey { get; set; }
    }

    [ComplexType]
    public class PayPayConfig
    {
        public string Email { get; set; }
        public string Url { get; set; }
        public string Command { get; set; }
        public string Shipping { get; set; }
    }

    public abstract class SiteConfig : Entity
    {
        public PaymentConfig Payment { get; set; }
        public RateValidityConfig Credit { get; set; }
        public TaxConfig Tax { get; set; }
        public NotificationConfig Notification { get; set; }
        public AdvertisementPrice Advertisement { get; set; }
        public EmailCampaignConfig Mailer { get; set; }
        public PayPayConfig PayPal { get; set; }

        public IList<Banner> Banners { get; set; }

        protected SiteConfig()
        {
            Payment=new PaymentConfig();
            Credit=new RateValidityConfig();
            Notification=new NotificationConfig();
            Banners = new List<Banner>();
            Tax=new TaxConfig();
            Advertisement = new AdvertisementPrice();
            PayPal=new PayPayConfig();
            Mailer = new EmailCampaignConfig();
        }
    }
}