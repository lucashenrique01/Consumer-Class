using System;
using System.Threading;
using Serilog;
using Confluent.Kafka;
using BrokerContextNamespace;
using ConsumerServiceNamespace;

namespace ConsumerControllerNamespace
{   
    class ConsumerController {
        
        EventService eventService = new EventService();
        public void ConsumerTopic(string topicName){

            BrokerContext brokerContext = new BrokerContext();
            string bootstrapServers = brokerContext.getBroker();

            var logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();
            logger.Information("Testando o consumo de mensagens com Kafka");  
            logger.Information($"BootstrapServers = {bootstrapServers}");
            logger.Information($"Topic = {topicName}");
            CancellationTokenSource cts = new CancellationTokenSource();
            Console.CancelKeyPress += (_, e) =>
            {
                e.Cancel = true;
                cts.Cancel();
            };

            try
            {
                using (var consumer = new ConsumerBuilder<Ignore, string>(brokerContext.ConfigConsumer()).Build())
                {
                    consumer.Subscribe(topicName);

                    try
                    {
                        while (true)
                        {
                            var cr = consumer.Consume(cts.Token);
                            logger.Information(
                                $"Mensagem lida: {cr.Message.Value}");
                            string eventoString = cr.Message.Value.ToString();                            
                            eventService.convertEvent(eventoString);
                        }
                    }
                    catch (OperationCanceledException)
                    {
                        consumer.Close();
                        logger.Warning("Cancelada a execução do Consumer...");
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error($"Exceção: {ex.GetType().FullName} | " +
                             $"Mensagem: {ex.Message}");
            }
            

        }
    }
}
