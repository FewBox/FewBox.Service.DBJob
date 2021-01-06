using System.Data;
using FewBox.Core.Utility.Formatter;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace FewBox.Service.DBJob
{
    public class Program
    {
        private static IConfigurationRoot Configuration { get; set; }
        public static void Main(string[] args)
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables();
            Configuration = builder.Build();
            var connectionStrings = Configuration.GetSection("ConnectionStrings").Get<ConnectionStrings>();
            var sqlScript = Configuration.GetSection("SqlScript").Get<SqlScript>();
            using (IDbConnection connection = new MySqlConnection(connectionStrings.DefaultConnection))
            {
                connection.Open();
                IDbCommand command = connection.CreateCommand();
                command.CommandText = sqlScript.Script;
                int effectRows = command.ExecuteNonQuery();
            }
        }
    }

    class SqlScript
    {
        public string Value { get; set; }
        public string Script
        {
            get
            {
                return Base64Utility.Deserialize(this.Value);
            }
        }
    }

    class ConnectionStrings
    {
        public string DefaultConnection { get; set; }
    }
}