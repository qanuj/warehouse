using System;
using System.Configuration;
using System.Linq;

namespace e10.Shared.Payment
{
    public class SellingOptions
    {
        public readonly string Key = "JBZaLc";
        public readonly string Salt = "GQs7yium";
        public readonly string PaymentUrl = "https://test.payu.in/_payment";
        public readonly int CreditPrice = 999;
        public readonly double TaxRate = 14.00;
        public readonly string TaxName = "Service Tax";
        public readonly string FbPage = "341120649428915";
        public readonly string Title = "Post Job to 50+ Job Boards with One Submission | FBHire";
        public readonly string Description = "Employers, recruiters and staffing agencies post jobs free to 50+ job boards with 1 click. Active resume database. Free employment postings on FBHire.";
        public readonly string CreditWarning = "Draft Saved!, Credit Balance Low, Please add more credits.";
        public readonly string Copyright = "All rights reserved, Conscript HR Advisor Private Limited 2015";
        public readonly string Logo = "content/image/fbhire-45.png";
        public readonly string AppName = "FBHire";

        public SellingOptions()
        {
            if (ConfigurationManager.AppSettings.AllKeys.Any(x => x == "AppName"))
            {
                var tmp = ConfigurationManager.AppSettings["AppName"];
                if (!string.IsNullOrWhiteSpace(tmp)) AppName = tmp;
            }
            if (ConfigurationManager.AppSettings.AllKeys.Any(x => x == "CreditWarning"))
            {
                var tmp = ConfigurationManager.AppSettings["CreditWarning"];
                if (!string.IsNullOrWhiteSpace(tmp)) CreditWarning = tmp;
            }
            if (ConfigurationManager.AppSettings.AllKeys.Any(x => x == "Copyright"))
            {
                var tmp = ConfigurationManager.AppSettings["Copyright"];
                if (!string.IsNullOrWhiteSpace(tmp)) Copyright = tmp;
            }
            if (ConfigurationManager.AppSettings.AllKeys.Any(x => x == "Logo"))
            {
                var tmp = ConfigurationManager.AppSettings["Logo"];
                if (!string.IsNullOrWhiteSpace(tmp)) Logo = tmp;
            }

            if (ConfigurationManager.AppSettings.AllKeys.Any(x => x == "PaymentKey"))
            {
                var tmp = ConfigurationManager.AppSettings["PaymentKey"];
                if (!string.IsNullOrWhiteSpace(tmp)) Key = tmp;
            }
            if (ConfigurationManager.AppSettings.AllKeys.Any(x => x == "PaymentSalt"))
            {
                var tmp = ConfigurationManager.AppSettings["PaymentSalt"];
                if (!string.IsNullOrWhiteSpace(tmp)) Salt = tmp;
            }
            if (ConfigurationManager.AppSettings.AllKeys.Any(x => x == "PaymentUrl"))
            {
                var tmp = ConfigurationManager.AppSettings["PaymentUrl"];
                if (!string.IsNullOrWhiteSpace(tmp)) PaymentUrl = tmp;
            }
            if (ConfigurationManager.AppSettings.AllKeys.Any(x => x == "FbPage"))
            {
                var tmp = ConfigurationManager.AppSettings["FbPage"];
                if (!string.IsNullOrWhiteSpace(tmp)) FbPage = tmp;
            }
            if (ConfigurationManager.AppSettings.AllKeys.Any(x => x == "CreditPrice"))
            {
                var tmp = ConfigurationManager.AppSettings["CreditPrice"];
                if (!string.IsNullOrWhiteSpace(tmp)) CreditPrice = Convert.ToInt32(tmp);
            }
            if (ConfigurationManager.AppSettings.AllKeys.Any(x => x == "TaxRate"))
            {
                var tmp = ConfigurationManager.AppSettings["TaxRate"];
                if (!string.IsNullOrWhiteSpace(tmp)) TaxRate = Convert.ToDouble(tmp);
            }
            if (ConfigurationManager.AppSettings.AllKeys.Any(x => x == "AppTitle"))
            {
                var tmp = ConfigurationManager.AppSettings["AppTitle"];
                if (!string.IsNullOrWhiteSpace(tmp)) Title = tmp;
            }
            if (ConfigurationManager.AppSettings.AllKeys.Any(x => x == "TaxName"))
            {
                var tmp = ConfigurationManager.AppSettings["TaxName"];
                if (!string.IsNullOrWhiteSpace(tmp)) TaxName = tmp;
            }
            if (ConfigurationManager.AppSettings.AllKeys.Any(x => x == "AppDescription"))
            {
                var tmp = ConfigurationManager.AppSettings["AppDescription"];
                if (!string.IsNullOrWhiteSpace(tmp)) Description = tmp;
            }

        }
    }
}