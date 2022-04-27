using System;
using System.Threading;
using Serilog;
using Confluent.Kafka;
using BrokerContextNamespace;
using ConsumerServiceNamespace;

namespace ConsumerControllerNamespace
{   
    class ConsumerController {
        public static void ConsumerTopic(string topicName){

            string bootstrapServers = BrokerContext.getBroker();

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
                using (var consumer = new ConsumerBuilder<Ignore, string>(BrokerContext.ConfigConsumer()).Build())
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
                            EventService.convertEvent(eventoString);
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
