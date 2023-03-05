using Crud.Core.Entities;
using Crud.Infraestructure.Data;
using Crud.Infraestructure.Interface;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace Crud.Infraestructure.Repository
{
    public class UserRepository : ContextDataBase, IUserRepository
    {
        public UserRepository(IConfiguration configuration) : base(configuration) { }
        public async Task DeleteByEmail(string email)
        {
            await Connection.ExecuteAsync(WriteQuery("delete from users where email = @Email"), new { email });
        }

        public async Task Insert(User user)
        {
            await  Connection.ExecuteAsync(WriteQuery(@"insert into users(name, email, date_of_birth) values(@Name, @Email, 
                                                           @DateOfBirth)"), user);
        }

        public async Task<User> SelectById(int id)
        {
            return await Connection.QueryFirstOrDefaultAsync(WriteQuery(@"select id as Id, name as Name, email as Email, 
                                    date_of_birth as DateOfBirth from users where id = @id"), new { id });
        }

        public async Task<IEnumerable<User>> SelectAll()
        {
            return await Connection.QueryAsync<User>(WriteQuery(@"select id as Id, name as Name, email as Email, 
                        date_of_birth as DateOfBirth from users"));
        }



        public async Task Update(User user)
        {
            await Connection.ExecuteAsync(WriteQuery(@"update users set name=@Name, email=@Email, 
                                        date_of_birth = @DateOfBirth where id = @Id"), user);
        }


        public async Task<User> SelectByEmail(string email)
        {
           return await Connection.QueryFirstOrDefaultAsync<User>(WriteQuery("select * from users where email = @email"), 
               new { email });
        }
        private string WriteQuery(string query) => query;
    }
}
