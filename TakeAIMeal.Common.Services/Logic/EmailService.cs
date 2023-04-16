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

        public async Task<bool> SendEmail(string to, string subject, string body)
        {
            try
            {
                var operation = await _emailClient.SendAsync(
                    wait: Azure.WaitUntil.Completed,
                    senderAddress: _emailFrom,
                    recipientAddress: to,
                    subject: subject,
                    htmlContent: body).ConfigureAwait(false);

                var result = await operation.WaitForCompletionAsync().ConfigureAwait(false);

                return result?.Value.Status == EmailSendStatus.Succeeded;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}
