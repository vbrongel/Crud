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
        public async Task Add(User newUser)
        {
            if (await CheckIfEmailExist(newUser.Email))
                Error("Email já cadastrado!");
            await _repository.Insert(newUser);

        }
        public async Task Edit(User user)
        {
            if (await CheckIfEmailExist(user.Email))
                Error("Email já cadastrado!");
            await _repository.Update(user);

        }

        public async Task<User> GetById(int id)
        {
            return await _repository.SelectById(id);
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _repository.SelectAll();
        }

        public async Task RemoveByEmail(string email)
        {
            await _repository.DeleteByEmail(email);
        }

        private async Task<bool> CheckIfEmailExist(string email) => 
            await _repository.SelectByEmail(email) != null ? true : false;
        
        private string Error(string message) => throw new Exception(message);
    }
}
