using e10.Shared.Models;

namespace e10.Shared.Services.Abstraction
{
    public interface ISiteService : IService
    {
        void AddFeedback(FeedbackViewModel model);
    }
}