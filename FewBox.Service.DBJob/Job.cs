using System;
using System.Data;
using System.Text;
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
                StringBuilder messageBuilder = new StringBuilder();
                this.BuildExpressionString(messageBuilder, mySqlException);
                this.Logger.LogError(messageBuilder.ToString());
            }
        }

        private void BuildExpressionString(StringBuilder messageBuilder, Exception exception)
        {
            messageBuilder.AppendFormat("{0} - {1}\r\n", exception.Message, exception.StackTrace);
            if(exception.InnerException!=null){
                this.BuildExpressionString(messageBuilder, exception.InnerException);
            }
        }
    }
}