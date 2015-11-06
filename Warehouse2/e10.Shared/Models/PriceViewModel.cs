using e10.Shared.Data.Abstraction;

namespace e10.Shared.Models
{
    public class PriceViewModelBase
    {
        public RateValidityConfig Credit { get; set; }
        public SubscriptionConfigWithTrial Membership { get; set; }
        public TaxConfig Tax { get; set; }
    }
}