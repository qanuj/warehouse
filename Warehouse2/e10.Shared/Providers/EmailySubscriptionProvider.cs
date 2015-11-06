using System.Threading.Tasks;
using e10.Shared.Emaily;

namespace e10.Shared.Providers
{
    public class EmailySubscriptionProvider : ISubscriptionProvider
    {
        public Task<bool> SubscribeAsync(string email, string name)
        {
            var emaily = new Subscription();
            return Task.FromResult(emaily.Subscribe(string.Empty,email,name,false));
        }
    }
}