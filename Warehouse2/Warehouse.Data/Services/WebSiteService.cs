using e10.Shared.Data.Abstraction;
using e10.Shared.Models;
using e10.Shared.Repository;
using e10.Shared.Services;
using e10.Shared.Services.Abstraction;

namespace Warehouse.Data.Services
{
    public class WebSiteService : SiteService, IWebSiteService
    {
        private readonly IFeedbackRepository _feedbackRepository;

        public WebSiteService(IFeedbackRepository feedbackRepository) : base(feedbackRepository)
        {
            _feedbackRepository = feedbackRepository;
        }

        public override void AddFeedback(FeedbackViewModel model)
        {
            _feedbackRepository.Create(new Feedback
            {
                Name = model.Name,
                Email = model.Email,
                Message = model.Message,
                Subject = model.Subject
            });
            _feedbackRepository.SaveChanges();
        }
    }
}