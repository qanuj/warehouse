using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web.Hosting;
using e10.Shared.Models;
using e10.Shared.Services.Abstraction;
using Microsoft.SqlServer.Server;
using Xero.Api.Core;
using Xero.Api.Core.Model;
using Xero.Api.Core.Model.Status;
using Xero.Api.Core.Model.Types;
using Xero.Api.Example.Applications.Private;
using Xero.Api.Infrastructure.OAuth;
using Xero.Api.Serialization;
using Xero.Api.Infrastructure.Exceptions;

namespace e10.Shared.Services
{
    public class XeroInvoiceProvider : IInvoiceProvider
    {
        private readonly XeroCoreApi _xeroApi;

        public XeroInvoiceProvider(OAuthWebConfigProvider _socialProvider)
        {
            var x = _socialProvider.Xero;
            if (!string.IsNullOrWhiteSpace(x.Certificate))
            {
                _xeroApi = new XeroCoreApi(x.Uri, 
                    new PrivateAuthenticator(GetCertificate(x.Certificate, x.Password)),
                    new Consumer(x.Key, x.Secret),
                    null, new DefaultMapper(), new DefaultMapper());
            }
        }

        private static X509Certificate2 GetCertificate(string relativePath, string pwd)
        {
            var _certificate = new X509Certificate2();
            _certificate.Import(HostingEnvironment.MapPath(relativePath), pwd, X509KeyStorageFlags.DefaultKeySet);
            return _certificate;
        }

        public void SendInvoice(string code)
        {
            //do  nothing;
        }

        public Guid CreateOrUpdateContact(ContactCreateViewModel model)
        {
            try
            {
                var isNew = true;
                var contact = new Contact
                {
                    FirstName = model.Name.First,
                    LastName =model.Name.Last,
                    Name = model.Company,
                    EmailAddress = model.Email
                };

                var contacts = _xeroApi.Contacts.Where(string.Format("EmailAddress==\"{0}\"", model.Email)).Find().ToList();
                if (contacts.Count > 0)
                {
                    contact = contacts[0];
                    isNew = false;
                }
                
                contact = isNew ? _xeroApi.Contacts.Create(contact) : _xeroApi.Contacts.Update(contact);

                return contact.Id;
            }
            catch (ValidationException ex)
            {
                var tmp = ex.ValidationErrors.Aggregate("", (current, error) => current + (error.Message + ","));
                throw new Exception(tmp ,ex);
            }

        }
        
        public string CreateInvoice(InvoiceCreateViewModel model)
        {
            var invoice = new Invoice
            {
                Type = InvoiceType.AccountsReceivable,
                Contact = new Contact {
                    Id = model.ContactId
                },
                Date = model.Date,
                DueDate = model.DueDate,
                LineAmountTypes =
                    model.TaxExtra == null
                        ? LineAmountType.NoTax
                        : model.TaxExtra ?? true ? LineAmountType.Exclusive : LineAmountType.Inclusive,
                LineItems = new List<LineItem>(),
                SentToContact = true,
                Status = InvoiceStatus.Authorised
            };

            foreach (var line in model.Lines)
            {
                if (line.UnitAmount != null)
                    invoice.LineItems.Add(new LineItem
                    {
                        Description = line.Description,
                        Quantity = line.Quantity,
                        UnitAmount =  (decimal)line.UnitAmount,
                        ItemCode = line.Code,
                        AccountCode = "SERVICE-IN"
                    });
            }
            try
            {
                invoice = _xeroApi.Invoices.Create(invoice);
            }
            catch (ValidationException ex)
            {
                throw new Exception(ex.ValidationErrors.First().Message,ex);
            }

            return invoice.Number;
        }

        public void AddPayment(string id, decimal amount, string hash)
        {
            try
            {
                var invoice = _xeroApi.Invoices.Find(id);
                var account = _xeroApi.Accounts.Find("payu");
                var payment = new Xero.Api.Core.Model.Payment
                {
                    Invoice = invoice,
                    Amount = amount,
                    Date = DateTime.UtcNow,
                    Reference = hash,
                    Status = PaymentStatus.Authorised,
                    Account = account
                };
                _xeroApi.Payments.Create(payment);
            }
            catch (ValidationException ex)
            {
                throw new Exception(ex.ValidationErrors.FirstOrDefault().Message,ex);
            }
            
        }

        public InvoicePaymentViewModel GetById(string id)
        {
            var org = _xeroApi.Organisation;
            var invoice = _xeroApi.Invoices.Find(id);
            if (invoice == null) return null;
            return new InvoicePaymentViewModel
            {
                Id = invoice.Id.ToString("D"),
                Organisation = org.LegalName,
                Total = invoice.CurrencyCode=="USD" ? (invoice.AmountDue ?? 0)*65 : (invoice.AmountDue ?? 0),
                ContactName = invoice.Contact.Name,
                Email = invoice.Contact.EmailAddress,
                Phone = invoice.Contact.Phones.Count > 0 ? invoice.Contact.Phones[0].PhoneNumber : string.Empty,
                Description = string.Format("Payment for Invoice {0} - {1}", id, org.Name),
            };
        }
    }
}
