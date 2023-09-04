using EasyNetQ;
using Microsoft.Extensions.Logging;
using Polly;
using RabbitMQ.Client.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace shared.Events
{
    public class Subscriber
    {
        private readonly IBus rabbitMqBus;
        private readonly ILogger<Subscriber> logger;

        public Subscriber(IBus rabbitMqBus, ILogger<Subscriber> logger)
        {
            this.rabbitMqBus = rabbitMqBus;
            this.logger = logger;
        }

        public async Task Subscribe<T>(Action<T> callback, string serviceName)
        {
            int attempt = 0;
            int maxAttempts = 4;
            BrokerUnreachableException exception = null;
            while (attempt < maxAttempts)
            {
                try
                {
                    logger.LogInformation($"Subscribing to event {typeof(T).Name}.");
                    //await rabbitMqBus.PubSub.SubscribeAsync($"{Dns.GetHostName()}_{nameof(T)}", callback, config =>
                    await rabbitMqBus.PubSub.SubscribeAsync($"{serviceName}_{typeof(T).Name}", callback, config =>
                    {
                        config.WithTopic(typeof(T).Name);
                    });
                    return;
                }
                catch (BrokerUnreachableException ex)
                {
                    exception = ex;
                    attempt++;
                    var delay = 1000 * (int)Math.Pow(2, attempt);
                    logger.LogWarning($"Broker is currently unreachable, attempt {attempt}/{maxAttempts}, delaying {delay / 1000}s");
                    await Task.Delay(delay);
                    continue;
                }
            }

            throw exception;
        }
    }
}
