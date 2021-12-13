using System;
using System.Threading.Tasks;
using GreenPipes;
using MassTransit;
using Newtonsoft.Json.Linq;
using SagaWithMassTransit.Domain;
using SagaWithMassTransit.Infra.Validate;

namespace Saga.Consumer.Infra.Filter
{
    public class MessageValidateFilter<TConsumer>
        : IFilter<ConsumerConsumeContext<TConsumer>> where TConsumer : class
    {
        public void Probe(ProbeContext context)
        {
            throw new System.NotImplementedException();
        }

        public async Task Send(ConsumerConsumeContext<TConsumer> context, IPipe<ConsumerConsumeContext<TConsumer>> next)
        {
            try
            {
                ConsumeContext<JToken> jsonContext;
                if (context.TryGetMessage(out jsonContext))
                {
                    var message = jsonContext.Message;
                    var messageRequest = message.ToObject<EmailMessage>();

                    if (!RegexUtilities.IsValidEmail(messageRequest.To))
                        throw new Exception("Exception occour :Email address is invalid");
                }
                await next.Send(context);
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync($"An exception occurred: {ex.Message}");
            }
        }
    }
}