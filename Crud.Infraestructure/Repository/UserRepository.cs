using Crud.Core.Entities;
using Crud.Infraestructure.Data;
using Crud.Infraestructure.Interface;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace Crud.Infraestructure.Repository
{
    public class UserRepository : ContextDataBase, IUserRepository
    {
        private readonly ILogRepository _log;
        public UserRepository(IConfiguration configuration, ILogRepository log) : base(configuration) 
        {
            _log = log; 
        }
        public async Task DeleteByEmail(string email)
        {
            try
            {
                await Connection.ExecuteAsync(WriteQuery("delete from users where email = @Email"), new { email });
                await _log.Add($"Deletado usuário com o e-mail {email}");
            }
            catch (Exception e) 
            {
                await _log.Add(@$"Ocorreu um erro ao se tentar deletar o usuário com o email {email} 
                           no método DeleteByEmail: {e.Message}, da classe UserRepository");
            }
        }

        public async Task Insert(User user)
        {
            try
            {
                await Connection.ExecuteAsync(WriteQuery(@"insert into users(name, email, date_of_birth, address) values(@Name, @Email, 
                                                           @DateOfBirth, @Address)"), user);
                await _log.Add(@$"Inserido usuário com as seguintes informações: Nome: {user.Name}; Email: {user.Email};
                                Data de Nascimento: {user.DateOfBirth}");
            }
            catch(Exception e)
            {
                await _log.Add(@$"Ocorreu um erro ao se tentar inserir um novo usuário no método Insert: {e.Message}, da 
                                classe UserRepository");
            }
        }

        public async Task<User> SelectById(int id)
        {
            try
            {
                return await Connection.QueryFirstOrDefaultAsync<User>(WriteQuery(@"select id as Id, name as Name, email as Email, 
                                    date_of_birth as DateOfBirth from users where id = @id"), new { id });
            }
            catch(Exception e) 
            {
                await _log.Add(@$"Ocorreu um erro ao se tentar selecionar o usuário com o id {id} no método 
                                SelectById: {e.Message}, da classe UserRepository");
                throw;
            }
        }

        public async Task<IEnumerable<User>> SelectAll()
        {
            try
            {
                return await Connection.QueryAsync<User>(WriteQuery(@"select id as Id, name as Name, email as Email, 
                        date_of_birth as DateOfBirth, address as Address from users"));
            }
            catch (Exception e)
            {
                await _log.Add(@$"Ocorreu um erro ao se tentar selecionar todos os usuários no método SelectAll: 
                                {e.Message}, da classe UserRepository");
                throw;
            }
        }



        public async Task Update(User user)
        {
            try
            {
                var oldInformationUser = await SelectById(user.Id);
                await Connection.ExecuteAsync(WriteQuery(@"update users set name=@Name, email=@Email where id = @Id"), user);
                var newInformationUser = await SelectById(user.Id);
                await _log.Add(@$"Atualizado usuário. Dados antes da atualização: Nome: {oldInformationUser.Name};
                                Email: {oldInformationUser.Email}; Data de Nascimento: {oldInformationUser.DateOfBirth}.
                                Dados depois da atualização: Nome:{newInformationUser}; Email: {newInformationUser.Email}
                                ; Data de Nascimento: {newInformationUser.DateOfBirth}");
            }
            catch(Exception e)
            {
                await _log.Add(@$"Ocorreu um erro ao se tentar atualizar o usuário {user.Name} 
                           no método Update: {e.Message}, da classe UserRepository");
            }
        }


        public async Task<User> SelectByEmail(string email)
        {
            try
            {
                return await Connection.QueryFirstOrDefaultAsync<User>(WriteQuery(@"select name as Name, email as Email, 
                                   date_of_birth as DateOfBirth, address as Address from users where email = @email"),
                   new { email });
            }
            catch(Exception e)
            {
                await _log.Add(@$"Ocorreu um erro ao se tentar deletar o usuário com o email {email} 
                           no método DeleteByEmail: {e.Message}, da classe UserRepository");
                throw;
            }
        }
        private string WriteQuery(string query) => query;
    }
}
