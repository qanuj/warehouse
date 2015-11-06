using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using e10.Shared.Models;

namespace e10.Shared.Services.Abstraction
{
    public interface IPayPalService
    {
        bool Verify(PayPalNotification cmd);
    }

   
}