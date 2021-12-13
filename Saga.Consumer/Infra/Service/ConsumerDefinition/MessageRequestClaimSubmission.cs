using Microsoft.Extensions.Options;
using SagaWithMassTransit.Shared.Settings;
using SagaWithMassTransit.Shared.Submissions;
using Saga.Consumer.Infra.Service.ConsumerService;
using MassTransit.Definition;
using MassTransit;
using System;
using GreenPipes;

namespace Saga.Consumer.Infra.Service.ConsumerDefinition
{
    public class MessageRequestClaimSubmission : ConsumerDefinition<MessageRequestConsumerService> 
    {
        private ClaimSubmission _claimSubmission { get; }

        public MessageRequestClaimSubmission(IOptions<QueueSettings> queueSettings)
            => _claimSubmission = queueSettings.Value.ClaimSubmission;
        
        protected void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfiguration, IConsumerRegistrationConfigurator<MessageRequestConsumerService> consumerConfiguration)
        {
            endpointConfiguration.UseMessageRetry(retry =>
            {
                retry.Ignore<ArgumentNullException>();
                retry.Interval(_claimSubmission.RetryCount, _claimSubmission.Interval);
            });
        }
    }
}