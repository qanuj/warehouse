using System;
using System.Net.Mail;
using System.Threading.Tasks;
using e10.Shared.Providers;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Logging;
using SendGrid;
using System.Net;
using e10.Shared.Models;

namespace e10.Shared.Security
{
    public class SendGridEmailService : IIdentityEmailMessageService
    {
        private readonly IEmailConfigProvider _config;
        private readonly ILogger _logger;
        public SendGridEmailService(IEmailConfigProvider config, ILogger logger)
        {
            _config = config;
            _logger = logger;
        }

        public Task SendAsync(IdentityMessage message)
        {
            return SendAsync(message, null);
        }

        public Task SendAsync(IdentityMessage message, params MessageAttachement[] attachments)
        {
            var msg = new SendGridMessage { From = new MailAddress(_config.From, _config.Name) };

            msg.AddTo(message.Destination);
            msg.Subject = message.Subject;
            msg.Html = message.Body;

            if (attachments!=null && attachments.Length > 0)
            {
                foreach (var att in attachments)
                {
                    if (att != null)
                    {
                        if (!string.IsNullOrWhiteSpace(att.FilePath))
                        {
                            msg.AddAttachment(att.FilePath);
                        }else if (att.Stream != null)
                        {
                            msg.AddAttachment(att.Stream,att.Name);
                        }
                    }
                }
            }

            var transportWeb = new Web(_config.SendGridApiKey);
            _logger.WriteInformation(string.Format("Email '{0}' Sent to {1}", message.Subject, message.Destination));
            return Task.FromResult(transportWeb.DeliverAsync(msg));
        }
    }
}