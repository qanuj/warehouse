using System.ComponentModel.DataAnnotations.Schema;

namespace e10.Shared.Data.Abstraction
{
    [ComplexType]
    public class RateValidityConfig
    {
        public int Rate { get; set; }
        public int Validity { get; set; }
    }


    [ComplexType]
    public class Quota : SubscriptionConfig
    {
        public int Size { get; set; }
    }

    [ComplexType]
    public class SubscriptionConfig : RateValidityConfig
    {
        public string Xero { get; set; }
        public string Name { get; set; }
    }

    [ComplexType]
    public class SubscriptionConfigWithTrial : SubscriptionConfig
    {
        public int Trial { get; set; }
        public int Discount { get; set; }
    }
}