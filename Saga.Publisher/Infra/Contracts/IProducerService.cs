using System.Threading;
using System.Threading.Tasks;
using SagaWithMassTransit.Domain;

namespace Saga.Publisher.Infra.Contract
{
    public interface IProducerService
    {
        Task Publish(EmailMessage message, CancellationToken cancellationToken);
    }
}