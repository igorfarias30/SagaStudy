using System;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SagaWithMassTransit.Domain;
using SagaWithMassTransit.Infra.Contract;
using SagaWithMassTransit.Shared.InputModels;

namespace SagaWithMassTransit.Infra.Gateway.Email
{
    public class EmailService : IEmailService
    {
        private readonly ILogger<EmailService> _logger;

        private SmtpProviderInputModel _smtpProviderInputModel { get; }

        public EmailService(ILogger<EmailService> logger, IOptions<SmtpProviderInputModel> smtpProviderInputModel)
        {
            _logger = logger;
            _smtpProviderInputModel = smtpProviderInputModel.Value;
        }

        public async Task<bool> SendMail(EmailMessage request)
        {
            try
            {
                _logger.LogInformation($"Start SendMail,request:{JsonConvert.SerializeObject(request)}");

                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient(_smtpProviderInputModel.SmtpClientHost);

                mail.From = new MailAddress(_smtpProviderInputModel.From);
                mail.To.Add(request.To);
                mail.Subject = " Mr Darshana - :" + request.Subject;
                mail.Body = "This is sample body  :" + request.Body;

                SmtpServer.Port = _smtpProviderInputModel.Port;
                SmtpServer.Credentials = new System.Net.NetworkCredential(_smtpProviderInputModel.UserName, _smtpProviderInputModel.Password);
                SmtpServer.EnableSsl = true;
                SmtpServer.Send(mail);
                _logger.LogInformation($"End SendMail statues: " + true);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return false;
        }
    }
}