using System;
using System.ComponentModel.DataAnnotations;

namespace e10.Shared.Models
{
    public class BannerCreateViewModel : BannerViewModel
    {

        public bool IdHidden { get; set; }
    }

    public class FeedbackCreateViewModel
    {
        public string Name { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
    }

    public class FeedbackViewModel : FeedbackCreateViewModel
    {
        public int Id { get; set; }
        public bool IsRead { get; set; }
        public DateTime Created { get; set; }
    }

    public class NewsletterSubscriptionViewModel
    {
        public string Email { get; set; }
        public string Name { get; set; }
    }
}