using System;
using System.Collections.Generic;
using e10.Shared.Data.Abstraction;

namespace e10.Shared.Models
{
    public class InvoiceViewModel
    {
        public MemberViewModel Member { get; set; }
        public IEnumerable<Transaction> Transactions { get; set; }
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public float Total { get; set; }
        public int UnitPrice { get; set; }
        public double Tax { get; set; }
        public string TaxName { get; set; }
        public double TaxAmount { get; set; }
    }
    
}