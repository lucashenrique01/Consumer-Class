using System;
using MysqlContext;
using EventNamespace;
using MySqlConnector;

namespace EventDaoNamespace{
    class EventDAO{
        public static async void insertEvent(Event novoEvento){
            var builder = Mysql.conection();
            using (var conn = new MySqlConnection(builder.ConnectionString))
            {
                Console.WriteLine("Opening connection");
                await conn.OpenAsync();

                 using (var command = conn.CreateCommand())
                {
                    command.CommandText = @"INSERT INTO event (IdEvent, SpecVersion, Source, Type, Subject, Time, CorrelationID, DataContentType, Data) VALUES 
                    (@IdEvent, @SpecVersion, @Source, @Type, @Subject, @Time, @CorrelationID, @DataContentType, @Data);";
                    command.Parameters.AddWithValue("@IdEvent", novoEvento.Id);
                    command.Parameters.AddWithValue("@SpecVersion", novoEvento.SpecVersion);
                    command.Parameters.AddWithValue("@Source", novoEvento.Source);
                    command.Parameters.AddWithValue("@Type", novoEvento.Type);
                    command.Parameters.AddWithValue("@Subject", novoEvento.Subject);
                    command.Parameters.AddWithValue("@Time", novoEvento.Time);
                    command.Parameters.AddWithValue("@CorrelationID", novoEvento.CorrelationID);
                    command.Parameters.AddWithValue("@DataContentType", novoEvento.DataContentType);
                    command.Parameters.AddWithValue("@Data", novoEvento.Data);           
                    await command.ExecuteNonQueryAsync();                           
                }
                Console.WriteLine("Closing connection");
            }
            Console.ReadLine();
        }

        public static async void existById(string id, Event novoEvento){
            int count = 0;
            var builder = Mysql.conection();
            using (var conn = new MySqlConnection(builder.ConnectionString))
            {
                Console.WriteLine("Opening connection");
                await conn.OpenAsync();

                 using (var command = conn.CreateCommand())
                {
                    command.CommandText = @"SELECT * FROM event where IdEvent = @id";
                    command.Parameters.AddWithValue("@id", id);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Console.WriteLine(string.Format(
                                "Reading from table=({0})",
                                reader.GetInt32(0)));
                                count++;
                        }
                    }

                }
                Console.WriteLine("Closing connection");
                if(!exists(count)){
                    insertEvent(novoEvento);
                }else {
                    Console.WriteLine("Evento já consumido");
                }
            }
            Console.ReadLine();
        }

        public static Boolean exists(int count){
            if(count > 0){
                return true;
            } else
            {
                return false;
            }
        }        
    }
}
