using Crud.Core.Entities;
namespace Crud.Application.Interface
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAll();
        Task<User> GetById(int id);
        Task Add(User newUser);
        Task Edit(User newUser);
        Task RemoveByEmail(string email);
    }
}
