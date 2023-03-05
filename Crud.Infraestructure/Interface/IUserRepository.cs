using Crud.Core.Entities;

namespace Crud.Infraestructure.Interface
{
    public interface IUserRepository
    {
        Task<User> SelectById(int id);
        Task<IEnumerable<User>> SelectAll();
        Task Insert(User user);
        Task DeleteByEmail(string email);
        Task Update(User user);
        Task<User> SelectByEmail(string email);
    }
}
