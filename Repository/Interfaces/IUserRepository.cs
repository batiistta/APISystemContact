using APISystemContact.Models;

namespace APISystemContact.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllUsers();
        List<String> GetAllUsersNames();
        Task<User> GetById(int id);
        Task<User> Create(User user);
        Task<User> Update(User user, int id);
        Task<bool> Delete(int id/*, string password*/);


    }
}
