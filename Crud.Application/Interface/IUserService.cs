using Crud.Core.Entities;
namespace Crud.Application.Interface
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAll();
        Task<User> GetByEmail(string email);
        Task<string> Add(User newUser);
        Task<string> Edit(User newUser);
        Task RemoveByEmail(string email);
    }
}
