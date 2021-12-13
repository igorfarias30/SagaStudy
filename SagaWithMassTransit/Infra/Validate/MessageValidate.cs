using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SagaWithMassTransit.Domain;

namespace SagaWithMassTransit.Infra.Validate
{
    public class MessageValidate : IMessageValidate
    {
        private readonly ILogger<MessageValidate> _logger;

        public MessageValidate(ILogger<MessageValidate> logger)
        {
            _logger = logger;
        }

        public Task<bool> Validate(EmailMessage request)
        {
            try
            {
                _logger.LogInformation($"Start Validate email message : {request}");

                if (request != null && string.IsNullOrEmpty(request.To))
                {
                    if (!RegexUtilities.IsValidEmail(request.To))
                        return Task.FromResult(false);
                }
                return Task.FromResult(true);
            }
            catch (Exception e)
            {
                _logger.LogError($"{e.Message}");
                return Task.FromResult(false);
            }
        }



    }
}