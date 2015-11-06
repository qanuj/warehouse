using System.Collections.Generic;
using System.Threading.Tasks;
using e10.Shared.Models;
using e10.Shared.Security;
using e10.Shared.Services.Abstraction;
using Microsoft.AspNet.Identity;
using Mvc.Mailer;

namespace Warehouse.Web.Mailers
{
    public sealed class Notifications : MailerBase, INotificationService
    {
        private readonly IIdentityEmailMessageService _emailService;

        public Notifications(IIdentityEmailMessageService emailService) :
            base()
        {
            _emailService = emailService;
            MasterName = "_Layout";
        }

        private void Send(MvcMailMessage msg, string to)
        {
            _emailService.SendAsync(new IdentityMessage()
            {
                Subject = msg.Subject,
                Destination = to,
                Body = msg.Body
            });
        }

        public void Welcome(string toEmail, string url)
        {
            var mvcMailMessage = new MvcMailMessage { Subject = "Welcome  to " + Product.Name };
            ViewBag.Url = url;
            ViewBag.UserName = toEmail;
            ViewBag.Email = toEmail;
            PopulateBody(mvcMailMessage, "Welcome");
            Send(mvcMailMessage, toEmail);
        }

        public void PasswordRecovery(string toEmail, string resetUrl)
        {
            var mvcMailMessage = new MvcMailMessage { Subject = "Reset Your password : " + Product.Name };
            ViewBag.ResetUrl = resetUrl;
            ViewBag.UserName = toEmail;

            ViewBag.Email = toEmail;
            PopulateBody(mvcMailMessage, "PasswordRecovery");
            Send(mvcMailMessage, toEmail);
        }

        public void Invite(IEnumerable<InviteCodeViewModel> invitees, string by)
        {
            Parallel.ForEach(invitees, inx =>
            {
                var msg2 = new MvcMailMessage { Subject = string.Format("{0} has invited you to {1}", by, Product.Name) };
                ViewBag.By = by;
                ViewBag.Email = inx.Email;
                ViewBag.Invite = inx;
                PopulateBody(msg2, "Invite");
                Send(msg2, inx.Email);
            });
        }
    }
}