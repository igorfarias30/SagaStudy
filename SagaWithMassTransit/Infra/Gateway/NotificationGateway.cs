using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SagaWithMassTransit.Domain;
using SagaWithMassTransit.Infra.Contract;

namespace SagaWithMassTransit.Infra.Gateway
{
    public class NotificationGateway: INotificationGateway
    {
        private readonly ILogger<NotificationGateway> _logger;
        private readonly IEmailService _emailService;

        public NotificationGateway(ILogger<NotificationGateway> logger, IEmailService emailService)
        {
            _logger = logger;
            _emailService = emailService;
        }

        public async Task<bool> SendMail(EmailMessage message)
        {
            try
            {
                _logger.LogInformation($"Start NotificationGateway,request:{JsonConvert.SerializeObject(message)}");
                var result = await _emailService.SendMail(message);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return false;
        }
    }    
}