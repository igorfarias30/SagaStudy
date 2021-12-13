using System.Threading.Tasks;
using SagaWithMassTransit.Domain;

namespace SagaWithMassTransit
{
    public interface IMessageValidate
    {
        Task<bool> Validate(EmailMessage message);
    }
}