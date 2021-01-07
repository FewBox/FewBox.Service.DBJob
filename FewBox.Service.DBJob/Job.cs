using System.Data;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;

namespace FewBox.Service.DBJob
{
    public class Job : IJob
    {
        private ConnectionStrings ConnectionStrings { get; set; }
        private SqlScript SqlScript { get; set; }
        private ILogger Logger { get; set; }
        public Job(ConnectionStrings connectionStrings, SqlScript sqlScript, ILogger<Job> logger)
        {
            this.ConnectionStrings = connectionStrings;
            this.SqlScript = sqlScript;
            this.Logger = logger;
        }
        public void Execute()
        {
            try
            {
                using (IDbConnection connection = new MySqlConnection(this.ConnectionStrings.DefaultConnection))
                {
                    connection.Open();
                    IDbCommand command = connection.CreateCommand();
                    command.CommandText = this.SqlScript.Script;
                    int effectRows = command.ExecuteNonQuery();
                }
            }
            catch (MySqlException mySqlException)
            {
                this.Logger.LogError(mySqlException.Message);
            }
        }
    }
}