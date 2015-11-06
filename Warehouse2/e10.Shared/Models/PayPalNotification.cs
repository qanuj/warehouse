using e10.Shared.Services;

namespace e10.Shared.Models
{
    public class PayPalNotification
    {
        public string Data { get; set; }
        public bool IsSandbox { get; set; }
        public string Currency { get; set; }
        public string Code { get; set; }
    }

}