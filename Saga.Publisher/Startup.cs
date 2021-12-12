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
using RabbitMQ.Client;
using Saga.Publisher.Infra.Contract;
using Saga.Publisher.Services;
using SagaWithMassTransit.Shared.Settings;

namespace Saga.Publisher
{
    public class Startup
    {
        private IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) => Configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            var queueSettings = Configuration
                .GetSection("RabbitMQ:QueueSettings")
                .Get<QueueSettings>();
            
            services.AddControllers();
            services.AddMassTransit(config =>
            {
                config.UsingRabbitMq((context, rabbitConfig) =>
                {
                    rabbitConfig.Host(queueSettings.HostName, queueSettings.Port, queueSettings.VirtualHost,
                        hostConfig =>
                        {
                            hostConfig.Username(queueSettings.UserName);
                            hostConfig.Password(queueSettings.Password);
                        });
                    rabbitConfig.ExchangeType = ExchangeType.Direct;
                });
                services.AddMassTransitHostedService();
            });

            services.AddTransient<IProducerService, ProducerService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}