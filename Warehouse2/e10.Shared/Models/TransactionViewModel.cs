using System;
using System.ComponentModel.DataAnnotations.Schema;
using e10.Shared.Data.Abstraction;

namespace e10.Shared.Models
{
    public class TransactionViewModel
    {
        public int Id { get; set; }
        public string Reason { get; set; }
        public bool IsSuccess { get; set; }
        public int Credit { get; set; }
        public string Code { get; set; }
        public string PaymentCapture { get; set; }
        public string Name { get; set; }

        public float Amount { get; set; }
        public DateTime Created { get; set; }

        internal User User { get; set; }

        [NotMapped]
        public string UserName => User.UserName;

        public string Mode { get; set; }
        public bool IsFailed { get; set; }
        public string CreatedBy { get; set; }
    }

    public class TransactionPaidViewModel
    {
        public string Code { get; set; }
        public string Capture { get; set; }
        public bool IsSuccess { get; set; }
        public string Reason { get; set; }
    }
}