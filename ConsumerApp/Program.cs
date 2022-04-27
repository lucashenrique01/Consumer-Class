using BrokerContextNamespace;
using ConsumerControllerNamespace;

namespace ConsumerApp
{
    class Program
    { 
        static void Main(string[] args)
        {
            string topicName = BrokerContext.getTopic();
            ConsumerController.ConsumerTopic(topicName);
        }
    }
}