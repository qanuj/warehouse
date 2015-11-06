using System.ComponentModel.DataAnnotations;

namespace Warehouse.Data.Core
{
    public class NotificationConfig
    {
        [EmailAddress]
        public string Feedback { get; set; }
    }
}