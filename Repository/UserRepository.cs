using APISystemContact.Data;
using APISystemContact.Models;
using APISystemContact.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace APISystemContact.Repository
{
    public class UserRepository : IUserRepository
    {
        //"readonly significa que seu valor só pode ser atribuído uma vez durante
        //a inicialização e não pode ser alterado posteriormente.
        private readonly APISystemContactDBContext _dbContext;
        public UserRepository(APISystemContactDBContext aPISystemContactDBContext)
        {
            _dbContext = aPISystemContactDBContext;
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<User> GetById(int id)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<User> Create(User user)
        {
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
            return user;
        }

        public async Task<User> Update(User user, int id)
        {
            User UserById = await GetById(id);
            if(UserById == null)
            {
                throw new Exception($"User with ID: {id} was not found in the database.");
            }
            UserById.Name = user.Name;
            UserById.Email = user.Email;

            _dbContext.Users.Update(UserById);
            await _dbContext.SaveChangesAsync();
            return UserById;
        }
        
        public async Task<bool> Delete(int id)
        {
            User UserById = await GetById(id);
            if (UserById == null)
            {
                throw new Exception($"User with ID: {id} was not found in the database.");
            }
            _dbContext.Users.Remove(UserById);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
