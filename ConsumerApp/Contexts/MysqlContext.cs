using System;
using MySqlConnector;

namespace MysqlContext
{
    class Mysql {
        public MySqlConnectionStringBuilder conection()
        {   
            var builder = new MySqlConnectionStringBuilder
            {
                Server = getServer(),
                Database = getDatabase(),
                UserID = getUser(),
                Password = getPassword(),
                SslMode = MySqlSslMode.Required,
            };
            return builder;
        }

        public string getUser(){
            string user;
            if (Environment.GetEnvironmentVariable("MYSQL_USER") != null)
            {
                user = Environment.GetEnvironmentVariable("MYSQL_USER");
            } else {
                return user = "root";
            }
            return user;
        }

        public string getPassword(){
            string password;
            if (Environment.GetEnvironmentVariable("MYSQL_PASSW") != null)
            {
                password = Environment.GetEnvironmentVariable("MYSQL_PASSW");
            } else {
                return password = "admin";
            }
            return password;
        }

        public string getDatabase(){
            string database;
            if (Environment.GetEnvironmentVariable("MYSQL_DATAB") != null)
            {
                database = Environment.GetEnvironmentVariable("MYSQL_DATAB");
            } else {
                return database = "event";
            }
            return database;
        }
        public string getServer(){
            string database;
            if (Environment.GetEnvironmentVariable("MYSQL_SERVER") != null)
            {
                database = Environment.GetEnvironmentVariable("MYSQL_SERVER");
            } else {
                return database = "localhost";
            }
            return database;
        }
    }
}
