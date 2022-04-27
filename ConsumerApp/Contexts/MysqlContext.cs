using MySqlConnector;

namespace MysqlContext
{
    class Mysql {
        public static MySqlConnectionStringBuilder conection()
        {   
            var builder = new MySqlConnectionStringBuilder
            {
                Server = "localhost",
                Database = "teste",
                UserID = "root",
                Password = "5623",
                SslMode = MySqlSslMode.Required,
            };
            return builder;
        }
    }
}
