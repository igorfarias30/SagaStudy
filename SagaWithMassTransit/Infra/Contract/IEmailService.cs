using System.Threading.Tasks;
using SagaWithMassTransit.Domain;

namespace SagaWithMassTransit.Infra.Contract
{
    public interface IEmailService
    {
        Task<bool> SendMail(EmailMessage message);
    }
}