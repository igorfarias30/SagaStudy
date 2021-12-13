using System.Threading.Tasks;
using SagaWithMassTransit.Domain;

namespace SagaWithMassTransit.Infra.Contract
{
    public interface INotificationGateway
    {
        Task<bool> SendMail(EmailMessage message);
    }
}