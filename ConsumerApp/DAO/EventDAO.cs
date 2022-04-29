using System;
using MysqlContext;
using EventNamespace;
using MySqlConnector;

namespace EventDaoNamespace{
    class EventDAO{
        private int count = 0;
        Mysql mysql = new Mysql();
        public async void insertEvent(Event newEvent){            
            var builder = mysql.conection();
            using (var conn = new MySqlConnection(builder.ConnectionString))
            {
                Console.WriteLine("Opening connection");
                try
                {    
                    await conn.OpenAsync();
                    using (var command = conn.CreateCommand())
                {
                    command.CommandText = @"INSERT INTO event (IdEvent, SpecVersion, Source, Type, Subject, Time, CorrelationID, DataContentType, Data) VALUES 
                    (@IdEvent, @SpecVersion, @Source, @Type, @Subject, @Time, @CorrelationID, @DataContentType, @Data);";
                    command.Parameters.AddWithValue("@IdEvent", newEvent.getId());
                    command.Parameters.AddWithValue("@SpecVersion", newEvent.getSpecVersion());
                    command.Parameters.AddWithValue("@Source", newEvent.getSource());
                    command.Parameters.AddWithValue("@Type", newEvent.getType());
                    command.Parameters.AddWithValue("@Subject", newEvent.getSubject());
                    command.Parameters.AddWithValue("@Time", newEvent.getTime());
                    command.Parameters.AddWithValue("@CorrelationID", newEvent.getCorrelationID());
                    command.Parameters.AddWithValue("@DataContentType", newEvent.getDataContentType());
                    command.Parameters.AddWithValue("@Data", newEvent.getData());           
                    await command.ExecuteNonQueryAsync();
                    Console.WriteLine("Um evento gravado");                           
                }
               }catch(Exception e){
                   Console.Write("Erro ao tentar inserir um evento: "+ e);
               }
                Console.WriteLine("Closing connection");
            }
            Console.ReadLine();
        }

        public async void existById(string id, Event newEvent){            
            var builder = mysql.conection();
            using (var conn = new MySqlConnection(builder.ConnectionString))
            {
                Console.WriteLine("Opening connection");
                try
                {
                    await conn.OpenAsync();
                    using (var command = conn.CreateCommand())
                    {
                        command.CommandText = "DROP TABLE IF EXISTS event;";
                        await command.ExecuteNonQueryAsync();
                        Console.WriteLine("Finished dropping table (if existed)");

                        command.CommandText = "CREATE TABLE event (Id serial PRIMARY KEY auto_increment, IdEvent VARCHAR(50), SpecVersion VARCHAR(50), Source VARCHAR(50), Type VARCHAR(50), Subject VARCHAR(50), Time VARCHAR(50), CorrelationID VARCHAR(50), DataContentType VARCHAR(50), Data VARCHAR(50));";
                        await command.ExecuteNonQueryAsync();
                        Console.WriteLine("Finished creating table");

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
                }catch(Exception e)
                {
                    Console.WriteLine("Erro ao tentar dar um select na tabela event: "+ e);
                }
                Console.WriteLine("Closing connection");                
            }
            Console.ReadLine();
        }

        public Boolean exists(int count){
            if(count > 0)
            {
                return true;
            } else
            {
                return false;
            }
        }

        public int getCount(){
            return this.count;
        }        
    }
}
