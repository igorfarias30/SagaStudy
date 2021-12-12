using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Saga.Consumer.Infra.Service;
using SagaWithMassTransit.Shared.Settings;

namespace Saga.Consumer
{
    public class Startup
    {
        private IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var queueSettings = Configuration
                .GetSection("RabbitMQ:QueueSettings")
                .Get<QueueSettings>();

            services.Configure<QueueSettings>(options =>
                Configuration.GetSection("RabbitMQ:QueueSettings").Bind(options));

            services.AddMassTransit(config =>
            {
                config.AddConsumer<MessageRequestConsumerService, MessageRequestClaimSubmission>();
                config.UsingRabbitMq((context, rabbitConfig) =>
                {
                    rabbitConfig.Host(queueSettings.HostName, queueSettings.Port, queueSettings.VirtualHost, host =>
                    {
                        host.Username(queueSettings.UserName);
                        host.Password(queueSettings.Password);
                    });
                    rabbitConfig.ReceiveEndpoint("Appointment-Create", endpointConfiguration =>
                    {
                        endpointConfiguration.ConfigureConsumer<>(context);
                    });
                });
                services.AddMassTransitHostedService();
            });

            services.AddTransient<IEmailService, EmailService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
        }
    }
}