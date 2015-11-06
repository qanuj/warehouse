using System;
using System.Collections.Specialized;
using System.Globalization;
using System.Web.Mvc;
using e10.Data.Repository;
using e10.Shared.Data.Abstraction;
using e10.Shared.Models;
using e10.Shared.Repository;
using Warehouse.Web.Results;
using Newtonsoft.Json;
using IConfigRepository = e10.Data.Repository.IConfigRepository;

namespace Warehouse.Web.Controllers
{
    public class PaymentController : Controller
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IActorRepository _actorRepository;
        private readonly IConfigRepository _configRepository;

        public PaymentController(ITransactionRepository transactionRepository, IConfigRepository configRepository, IActorRepository actorRepository)
        {
            _transactionRepository = transactionRepository;
            _configRepository = configRepository;
            _actorRepository = actorRepository;
        }


        [Authorize, Route("~/pay/return")]
        public ActionResult Paid(PaymentReceiptViewModel model)
        {
            const string hashSeq = "key|txnid|amount|productinfo|firstname|email|udf1|udf2|udf3|udf4|udf5|udf6|udf7|udf8|udf9|udf10";
            var config = _configRepository.Config();
            var transaction = _transactionRepository.ByCode(model.txnid);
            if (transaction == null)
            {
                throw new Exception(string.Format("No such transaction found '{0}'", model.txnid));
            }

            transaction.PaymentCapture = JsonConvert.SerializeObject(new
            {
                model.status,
                model.hash,
                model.txnid,
                model.productinfo,
                model.key,
                model.firstname,
                model.email,
                model.udf1,
                model.udf2,
                model.udf3,
                model.udf4,
                model.udf5,
                model.udf6,
                model.udf7,
                model.udf8,
                model.udf9,
                model.udf10
            });

            var mercHashVarsSeq = hashSeq.Split('|');
            Array.Reverse(mercHashVarsSeq);
            var mercHashString = config.Payment.Salt + "|" + model.status;

            foreach (var mercHashVar in mercHashVarsSeq)
            {
                mercHashString += "|";
                mercHashString = mercHashString + (Request.Form[mercHashVar] ?? "");

            }
            var mercHash = Transaction.GenerateHash512(mercHashString).ToLower();
            transaction.IsSuccess = mercHash == model.hash;
            _transactionRepository.SaveChanges();

            return Redirect("/#/billing?status=" + model.status);

        }

        [Authorize, Route("~/pay/{code}")]
        public ActionResult Pay(string code)
        {
            var transction = _transactionRepository.ByCode(code);
            var usr = _actorRepository.ByUserId(transction.UserId);
            var config = _configRepository.Config();

            var amount = transction.Amount.ToString(CultureInfo.InvariantCulture);
            var hashString = config.Payment.Key + "|" + transction.Code + "|" + amount + "|" + transction.Name + "|" + usr.FullName + "|" + usr.Email + "|||||||||||" + config.Payment.Salt;
            var opt = new NameValueCollection
            {
                {"key", config.Payment.Key},
                {"txnid", transction.Code},
                {"amount", amount},
                {"productinfo", transction.Name},
                {"firstname", usr.FullName},
                {"phone", usr.Mobile},
                {"email", usr.Email},
                {"surl", Url.Action("Paid","Payment",new {},Request.Url.Scheme)},
                {"furl", Url.Action("Paid","Payment",new {},Request.Url.Scheme)},
                {"service_provider", "payu_paisa"},
                {"hash",Transaction.GenerateHash512(hashString)}
            };
            return new FormSubmitResult(opt, config.Payment.Url);
        }

    }
}