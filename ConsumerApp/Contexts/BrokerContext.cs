using System;
using Confluent.Kafka;

namespace BrokerContextNamespace
{
    class BrokerContext
    {
        public string getTopic()
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

        public string getBroker()
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
        public ConsumerConfig ConfigConsumer()
        {   
            BrokerContext brokerContext = new BrokerContext();
            string topicName = brokerContext.getTopic();
            string bootstrapServers = brokerContext.getBroker();
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