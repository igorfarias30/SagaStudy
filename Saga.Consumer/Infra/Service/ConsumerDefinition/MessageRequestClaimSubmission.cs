using MassTransit.Configuration;
using Microsoft.Extensions.Options;
using SagaWithMassTransit.Shared.Settings;
using SagaWithMassTransit.Shared.Submissions;
using Saga.Consumer.Infra.Service.ConsumerService;

namespace Saga.Consumer.Infra.Service.ConsumerDefinition
{
    public class MessageRequestClaimSubmission : ConsumerDefinition<MessageRequestConsumerService> 
    {
        private ClaimSubmission _claimSubmission { get; }

        public MessageRequestClaimSubmission(IOptions<QueueSettings> queueSettings)
            => _claimSubmission = queueSettings.Value.ClaimSubmission;
    }
}