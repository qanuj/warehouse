using e10.Shared.Data.Abstraction;
using e10.Shared.Models;
using e10.Shared.Repository;
using e10.Shared.Services.Abstraction;

namespace e10.Shared.Services
{
    public class SiteService : ISiteService
    {
        private readonly IFeedbackRepository _feedbackRepository;

        public SiteService(IFeedbackRepository feedbackRepository)
        {
            _feedbackRepository = feedbackRepository;
        }

        public virtual void AddFeedback(FeedbackViewModel model)
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