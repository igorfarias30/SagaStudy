using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Saga.Publisher.Infra.Contract;
using SagaWithMassTransit.Domain;

namespace Saga.Publisher.Services
{
    public class ProducerService : IProducerService
    {
        private readonly ILogger<ProducerService> _logger;
        private readonly IPublishEndpoint _endpoint;

        public ProducerService(ILogger<ProducerService> logger, IPublishEndpoint endpoint)
        {
            _logger = logger;
            _endpoint = endpoint;
        }

        public async Task Publish(EmailMessage message, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Start publish message: {message}");
                await _endpoint.Publish<EmailMessage>(message, cancellationToken);
                _logger.LogInformation($"End publish");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
    }
}