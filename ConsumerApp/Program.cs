using System;
using System.Threading;
using Confluent.Kafka;
using Serilog;


namespace ConsumerApp
{
    class Program
    {
        public static ConsumerConfig ConfigConsumer(){
            string topicName = getTopic();
            string bootstrapServers = getBroker();

            var config = new ConsumerConfig
            {
                BootstrapServers = bootstrapServers,
                GroupId = $"{topicName}-group-0",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };
            return config;
        }

        public static String getTopic(){
            string topicName;

            if(Environment.GetEnvironmentVariable("TOPIC_NAME") != null)
            {
                topicName = Environment.GetEnvironmentVariable("TOPIC_NAME");
            } else {
                topicName = "br.com.example.correctTopic";
            }
            return topicName;
        }
        public static String getBroker(){
            string bootstrapServers;
            
            if(Environment.GetEnvironmentVariable("BROKER_HOST") != null)
            {
                bootstrapServers = Environment.GetEnvironmentVariable("BROKER_HOST");
            } else {
                bootstrapServers = "localhost:9092";
            }
            
            return bootstrapServers;
        }
        static void Main(string[] args)
        {

            string topicName = getTopic();
            string bootstrapServers = getBroker();

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
                using (var consumer = new ConsumerBuilder<Ignore, string>(ConfigConsumer()).Build())
                {
                    consumer.Subscribe(topicName);

                    try
                    {
                        while (true)
                        {
                            var cr = consumer.Consume(cts.Token);
                            logger.Information(
                                $"Mensagem lida: {cr.Message.Value}");
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