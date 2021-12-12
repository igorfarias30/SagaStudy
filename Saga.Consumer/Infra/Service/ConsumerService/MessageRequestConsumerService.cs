using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SagaWithMassTransit.Domain;

namespace Saga.Consumer.Infra.Service.ConsumerService
{
    public class MessageRequestConsumerService : IConsumer<EmailMessage>
    {
        private readonly ILogger<MessageRequestConsumerService> _logger;
        private readonly INotificationGateway _notificationGateway;

        public MessageRequestConsumerService(ILogger<MessageRequestConsumerService> logger, INotificationGateway notificationGateway)
        {
            _logger = logger;
            _notificationGateway = notificationGateway;
        }

        public async Task Consume(ConsumeContext<EmailMessage> context)
        {
            try
            {
                _logger.LogInformation($"Start AppointmentCreateConsumerService Consume, request: {JsonConvert.SerializeObject(context.Message)}");
            }
            catch (Exception e)
            { 
                _logger.LogError(e.Message);
            }
        }
    }
}