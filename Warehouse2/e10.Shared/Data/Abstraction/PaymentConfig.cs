using System.ComponentModel.DataAnnotations.Schema;

namespace e10.Shared.Data.Abstraction
{
    [ComplexType]
    public class PaymentConfig
    {
        public string Key { get; set; }
        public string Salt { get; set; }
        public string MerchantId { get; set; }
        public string Url { get; set; }

        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(Key) && !string.IsNullOrWhiteSpace(Salt) && !string.IsNullOrWhiteSpace(MerchantId) && !string.IsNullOrWhiteSpace(Url);
        }
    }
}