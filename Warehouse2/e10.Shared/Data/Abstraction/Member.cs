using System;
using System.Collections.Generic;

namespace e10.Shared.Data.Abstraction
{
    public abstract class Member : Person
    {
        public IList<Subscription> Subscriptions { get; set; }
        public IList<Invite> Invites { get; set; }

        public string Profile { get; set; }
        public Social Social { get; set; }

        public string About { get; set; }

        public User Owner { get; set; }
        public string OwnerId { get; set; }

        public Guid? ContactId { get; set; }

        public int Complete { get; set; }

        protected Member()
        {
            this.Social = new Social();
        }
        
    }
}