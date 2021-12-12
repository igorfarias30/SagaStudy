using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Saga.Publisher.Infra.Contract;
using SagaWithMassTransit.Domain;

namespace SagaWithMassTransit.Controllers
{
    [Route("api/[controller]")]
    public class PublisherController : Controller
    {
        private readonly IProducerService _producerService;
        private readonly ILogger<PublisherController> _logger;

        public PublisherController(IProducerService producerService, ILogger<PublisherController> logger)
        {
            _producerService = producerService;
            _logger = logger;
        }
        
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] EmailMessage message, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Publishing message {message}");
                await _producerService.Publish(message, cancellationToken);
                return Ok("Send to the queue successfully");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}