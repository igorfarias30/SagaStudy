using System.Threading.Tasks;
using GreenPipes;
using MassTransit;

namespace Saga.Consumer.Infra.Filter
{
    public class MessageValidateFilter<TConsumer>
        : IFilter<ConsumerConsumeContext<TConsumer>> where TConsumer : class
    {
        public void Probe(ProbeContext context)
        {
            throw new System.NotImplementedException();
        }

        public Task Send(ConsumerConsumeContext<TConsumer> context, IPipe<ConsumerConsumeContext<TConsumer>> next)
        {
            throw new System.NotImplementedException();
        }
    }
}