using System;

namespace e10.Shared.Data.Abstraction
{
    public class Subscription : Entity
    {
        public Member Subscriber { get; set; }
        public int SubscriberId { get; set; }

        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public Payment Payment { get; set; }
        public int? PaymentId { get; set; }
    }
}