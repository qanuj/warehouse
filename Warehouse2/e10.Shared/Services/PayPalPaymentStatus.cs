using System;
using Xero.Api.Core.Model.Status;

namespace e10.Shared.Services
{
    public enum PayPalPaymentStatus
    {
        Failed=0,
        Pending = 10,
        Authorized = 20,
        Paid = 30,
        PartiallyRefunded = 35,
        Refunded = 40,
        Voided = 50
    }
}