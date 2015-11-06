using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using e10.Shared.Extensions;
using e10.Shared.Models;
using e10.Shared.Repository;
using e10.Shared.Services.Abstraction;

namespace e10.Shared.Services
{
    public class PayPalService  : IPayPalService
    {
        private readonly ITransactionRepository _transactionRepository;

        public PayPalService(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        private string GetPaypalUrl(bool sand)
        {
            return sand ? "https://www.sandbox.paypal.com/us/cgi-bin/webscr" : "https://www.paypal.com/us/cgi-bin/webscr";
        }
        private static PayPalPaymentStatus GetPaymentStatus(string paymentStatus, string pendingReason)
        {
            var result = PayPalPaymentStatus.Pending;

            if (paymentStatus == null)
                paymentStatus = string.Empty;

            if (pendingReason == null)
                pendingReason = string.Empty;

            switch (paymentStatus.ToLowerInvariant())
            {
                case "pending":
                    switch (pendingReason.ToLowerInvariant())
                    {
                        case "authorization":
                            result = PayPalPaymentStatus.Authorized;
                            break;
                        default:
                            result = PayPalPaymentStatus.Pending;
                            break;
                    }
                    break;
                case "processed":
                case "completed":
                case "canceled_reversal":
                    result = PayPalPaymentStatus.Paid;
                    break;
                case "denied":
                case "expired":
                case "failed":
                case "voided":
                    result = PayPalPaymentStatus.Voided;
                    break;
                case "refunded":
                case "reversed":
                    result = PayPalPaymentStatus.Refunded;
                    break;
                default:
                    break;
            }
            return result;
        }
        private decimal GetAmount(Dictionary<string, string> values)
        {
            var amount = values.TryGetCurrency("mc_gross");
            if (amount > 0) return amount;
            return values.TryGetCurrency("mc_amount3");
        }
        public bool Verify(PayPalNotification cmd)
        {
            var transaction = _transactionRepository.ByCode(cmd.Code);
            var req = (HttpWebRequest)WebRequest.Create(GetPaypalUrl(cmd.IsSandbox));
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            string formContent = string.Format("{0}&cmd=_notify-validate", cmd.Data);
            req.ContentLength = formContent.Length;
            using (var sw = new StreamWriter(req.GetRequestStream(), Encoding.ASCII)){
                sw.Write(formContent);
            }                     
            using (var sr = new StreamReader(req.GetResponse().GetResponseStream())){
                transaction.Reason = HttpUtility.UrlDecode(sr.ReadToEnd());
            }
            var values = cmd.Data.ToDictionary(); 
            var total = values.TryGetCurrency("mc_gross");
            var newPaymentStatus = GetPaymentStatus(values.TryGet("payment_status"), values.TryGet("pending_reason"));
            if (newPaymentStatus == PayPalPaymentStatus.Paid || newPaymentStatus == PayPalPaymentStatus.Authorized)
            {
                transaction.IsSuccess = true;
                transaction.PaymentCapture = cmd.Data;
                transaction.Actual = total; 
                _transactionRepository.Update(transaction);
                _transactionRepository.SaveChanges();
                return true;
            }
            return false;
        }
    }
}