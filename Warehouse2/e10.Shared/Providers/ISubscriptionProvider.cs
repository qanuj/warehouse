using System.Threading.Tasks;

namespace e10.Shared.Providers
{
    public interface ISubscriptionProvider
    {
        Task<bool> SubscribeAsync(string email, string name);
    }
}