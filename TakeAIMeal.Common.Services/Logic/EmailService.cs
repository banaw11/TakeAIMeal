using Azure;
using Azure.Communication.Email;
using Microsoft.Extensions.Options;
using TakeAIMeal.Common.Options;
using TakeAIMeal.Common.Services.Interfaces;

namespace TakeAIMeal.Common.Services.Logic
{
    public class EmailService : IEmailService
    {
        private readonly EmailClient _emailClient;
        private readonly string _emailFrom;
        public EmailService(IOptions<EmailClientOption> options)
        {
            var option = options.Value;
            _emailClient = new EmailClient(new Uri(option.Url), new AzureKeyCredential(option.Key));
            _emailFrom = option.EmailFrom;
        }

        public async Task SendEmail(string to, string subject, string body)
        {
            try
            {
                await _emailClient.SendAsync(
                    wait: Azure.WaitUntil.Completed,
                    senderAddress: _emailFrom,
                    recipientAddress: to,
                    subject: subject,
                    htmlContent: body).ConfigureAwait(false);
            }
            catch(Exception ex)
            {

            }
        }
    }
}
