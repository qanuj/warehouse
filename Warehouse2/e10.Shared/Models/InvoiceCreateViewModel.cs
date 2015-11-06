using System;
using System.Collections;
using System.Collections.Generic;
using e10.Shared.Extensions;

namespace e10.Shared.Models
{
    public enum InvoiceRepeatEnum
    {
        None,
        Monthly,
        Yearly
    }

    public class InvoiceCreateViewModel
    {
        public Guid ContactId { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? DueDate { get; set; }
        public bool? TaxExtra { get; set; }
        public InvoiceRepeatEnum Repeat { get; set; }
        public int? RepeatQuantity { get; set; }
        public IEnumerable<InvoiceLineViewModel> Lines { get; set; }
    }

    public class InvoiceLineViewModel
    {
        public string Description { get; set; }
        public decimal? Quantity { get; set; }
        public float? UnitAmount { get; set; }
        public string Code { get; set; }
    }

    public class ContactCreateViewModel
    {
        public string Company { get; set; }
        public string Email { get; set; }
        public Name Name { get; set; }
    }
}