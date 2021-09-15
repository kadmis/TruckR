using RawRabbit.Configuration;
using RawRabbit.Configuration.Exchange;
using System;
using System.Collections.Generic;

namespace BuildingBlocks.EventBus.Internals
{
    internal class Configuration
    {
        private Configuration()
        {

        }

        public static RawRabbitConfiguration Create()
        {
            return new RawRabbitConfiguration
            {
                Username = "guest",
                Password = "guest",
                VirtualHost = "/",
                Port = 5672,
                Hostnames = new List<string> { "localhost" },
                RequestTimeout = TimeSpan.FromSeconds(10),
                PublishConfirmTimeout = TimeSpan.FromSeconds(30),
                RecoveryInterval = TimeSpan.FromSeconds(10),
                PersistentDeliveryMode = true,
                AutoCloseConnection = true,
                AutomaticRecovery = true,
                TopologyRecovery = true,
                Exchange = new GeneralExchangeConfiguration
                {
                    Durable = true,
                    AutoDelete = true,
                    Type = ExchangeType.Topic
                },
                Queue = new GeneralQueueConfiguration
                {
                    AutoDelete = true,
                    Durable = true,
                    Exclusive = true
                }
            };
        }
    }
}
