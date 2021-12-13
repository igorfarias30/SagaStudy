using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GreenPipes;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Saga.Consumer.Infra.Filter;
using Saga.Consumer.Infra.Service;
using Saga.Consumer.Infra.Service.ConsumerDefinition;
using Saga.Consumer.Infra.Service.ConsumerService;
using SagaWithMassTransit;
using SagaWithMassTransit.Infra.Contract;
using SagaWithMassTransit.Infra.Gateway;
using SagaWithMassTransit.Infra.Gateway.Email;
using SagaWithMassTransit.Infra.Validate;
using SagaWithMassTransit.Shared.InputModels;
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
                config
                    .AddConsumer<MessageRequestConsumerService, MessageRequestClaimSubmission>(configurator =>
                        configurator.UseFilter(new MessageValidateFilter<MessageRequestConsumerService>())
                    );
                
                services
                .Configure<SmtpProviderInputModel>(options =>
                    Configuration
                        .GetSection("SmtpProviderInputModel")
                        .Bind(options)
                );

                services.AddControllers();

                config.UsingRabbitMq((context, rabbitConfig) =>
                {
                    rabbitConfig.Host(queueSettings.HostName, queueSettings.Port, queueSettings.VirtualHost, host =>
                    {
                        host.Username(queueSettings.UserName);
                        host.Password(queueSettings.Password);
                    });
                    rabbitConfig.ReceiveEndpoint("Appointment-Create", endpointConfiguration =>
                    {
                        endpointConfiguration.ConfigureConsumer<MessageRequestConsumerService>(context);
                    });
                });

                services.AddMassTransitHostedService();
            });

            services.AddTransient<IEmailService, EmailService>();
            services.AddScoped<IMessageValidate, MessageValidate>();
            services.AddScoped<INotificationGateway, NotificationGateway>();
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