using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e10.Shared.Models
{
    public class ResellerClubPaymentViewModel
    {
        public string PaymentTypeId { get; set; }
        public string TransId { get; set; }
        public string UserId { get; set; }
        public string UserType { get; set; }
        public string TransactionType { get; set; }
        public string InvoiceIds{ get; set; }
        public string DebitNoteIds { get; set; }
        public string Description { get; set; }
        public float SellingCurrencyAmount { get; set; }
        public float AccountingCurrencyAmount { get; set; }
        public string RedirectUrl { get; set; }
        public string CheckSum { get; set; }
        public string Name { get; set; }
        public string Company { get; set; }
        public string EmailAddr { get; set; }
        public string Address1 { get; set; }
        public string Address3 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Zip { get; set; }
        public string TelNoCc { get; set; }
        public string TelNo { get; set; }
        public string FaxNoCc { get; set; }
        public string FaxNo { get; set; }
        public string ResellerEmail { get; set; }
        public string ResellerURL { get; set; }
        public string ResellerCompanyName { get; set; }
        public string ResellerCurrency { get; set; }
        public string BrandName { get; set; }
        public string Xero { get; set; }
    }

    public class PaymentReceiptViewModel
    {
        public string status { get; set; }
        public string hash { get; set; }
        public string txnid { get; set; }
        public string productinfo { get; set; }
        public string key { get; set; }
        public string firstname { get; set; }
        public string email { get; set; }
        public string udf1 { get; set; }
        public string udf2 { get; set; }
        public string udf3 { get; set; }
        public string udf4 { get; set; }
        public string udf5 { get; set; }
        public string udf6 { get; set; }
        public string udf7 { get; set; }
        public string udf8 { get; set; }
        public string udf9 { get; set; }
        public string udf10 { get; set; }
    }
}
