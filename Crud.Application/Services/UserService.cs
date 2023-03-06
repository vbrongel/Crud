using Crud.Application.Interface;
using Crud.Core.Entities;
using Crud.Infraestructure.Interface;

namespace Crud.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }
        public async Task<string> Add(User newUser)
        {
            if (await CheckIfEmailExist(newUser.Email))
                return "Já existe um usuário com este e-mail!";
            await _repository.Insert(newUser);
            return "Usuário inserido";
        }
        public async Task<string> Edit(User user)
        {
            if (!await CheckIfUserExist(user.Id))
                return "Usuário não existe!";
            if (await CheckIfEmailExist(user.Email))
                return "Já existe um usuário com este email!";
            await _repository.Update(user);
            return "Usuário atualizado";

        }

        public async Task<User> GetByEmail(string email)
        {
            if (!await CheckIfEmailExist(email))
                return null;
            return await _repository.SelectByEmail(email);
        }

        public async Task<IEnumerable<User>> GetAll() => await _repository.SelectAll();


        public async Task RemoveByEmail(string email) => await _repository.DeleteByEmail(email);



        private async Task<bool> CheckIfEmailExist(string email) =>
            await _repository.SelectByEmail(email) != null ? true : false;
        public async Task<bool> CheckIfUserExist(int id) => await _repository.SelectById(id) != null ? true : false;
        private string Error(string message) => throw new Exception(message);
    }
}
