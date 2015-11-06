using System;
using e10.Shared.Models;
using WhatsAppApi;

namespace e10.Shared.Services.Abstraction
{
    public interface IInvoiceProvider
    {
        InvoicePaymentViewModel GetById(string id);
        string CreateInvoice(InvoiceCreateViewModel invoice);
        void SendInvoice(string code);
        Guid CreateOrUpdateContact(ContactCreateViewModel contact);
        void AddPayment(string id, decimal amount, string hash);
    }
}