using System.ComponentModel.DataAnnotations;

namespace e10.Shared.Data.Abstraction
{
    public class NotificationConfig
    {
        [EmailAddress]
        public string Feedback { get; set; }
    }
}