using System;
using Confluent.Kafka;

namespace BrokerContextNamespace
{
    class BrokerContext
    {
        public static string getTopic()
        {
            string topicName;

            if (Environment.GetEnvironmentVariable("TOPIC_NAME") != null)
            {
                topicName = Environment.GetEnvironmentVariable("TOPIC_NAME");
            }
            else
            {
                topicName = "br.com.example.correctTopic";
            }
            return topicName;
        }

        public static string getBroker()
        {
            string bootstrapServers;

            if (Environment.GetEnvironmentVariable("BROKER_HOST") != null)
            {
                bootstrapServers = Environment.GetEnvironmentVariable("BROKER_HOST");
            }
            else
            {
                bootstrapServers = "localhost:9092";
            }

            return bootstrapServers;
        }
        public static ConsumerConfig ConfigConsumer()
        {
            String topicName = getTopic();
            String bootstrapServers = getBroker();
            var config = new ConsumerConfig
            {
                BootstrapServers = bootstrapServers,
                GroupId = $"{topicName}-group-0",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };
            return config;
        }
    }    
}