using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
using Npgsql;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace Crud.Infraestructure.Data
{
    public abstract class ContextDataBase
    {
        private string _stringConnection;
        protected IDbConnection Connection { get; }
        

        private readonly IConfiguration _configuration;
        protected ContextDataBase(IConfiguration configuration)
        {
            _configuration = configuration;
            _stringConnection = _configuration.GetConnectionString("DefaultConnection");
            var connection = _configuration.GetSection("ConnectionStrings").GetSection("Connection").Value;
            Connection = GetConnection(connection);
            try
            {
                Connection.Open();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private IDbConnection GetConnection(string connection)
        {
            switch(connection)
            {
                case "SqlServer": return new SqlConnection(_stringConnection);
                case "Oracle" : return new OracleConnection(_stringConnection);
                case "Postgre": return new NpgsqlConnection(_stringConnection);
                default: return new MySqlConnection(_stringConnection);
            }
        }

    }
}


