using Crud.Infraestructure.Data;
using Crud.Infraestructure.Interface;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace Crud.Infraestructure.Repository
{
    public class LogRepository : ContextDataBase, ILogRepository
    {
        public LogRepository(IConfiguration configuration) : base(configuration) { }

        public async Task Add(string message)
        {
            var date = DateTime.Now;
            var query = "insert into logs(message, created_date) values(@message, @date)";
            await Connection.ExecuteAsync(query, new {message, date});
        }
    }
}
