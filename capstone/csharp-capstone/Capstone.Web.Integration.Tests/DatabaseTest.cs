using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.SqlClient;
using System.Transactions;

namespace Capstone.Test.Dal
{
    public abstract class DatabaseTest
    {
        private TransactionScope _transaction;

        [TestInitialize]
        public void DatabaseSetup()
        {
            _transaction = new TransactionScope();
            Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.Development.json", optional: false)
                .Build();

            using (var connection = new SqlConnection(NpGeekDbConnectionString))
            {
                var command = connection.CreateCommand();
                command.CommandText =
                    @"DELETE FROM weather; 
                      DELETE FROM survey_result; 
                      DELETE FROM park;";

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        [TestCleanup]
        public void DatabaseCleanup()
        {
            _transaction.Dispose();
        }

        protected IConfiguration Configuration { get; private set; }

        protected string NpGeekDbConnectionString
        {
            get
            {
                return Configuration["ConnectionStrings:NpGeekDb"];
            }
        }
    }
}