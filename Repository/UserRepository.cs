using APISystemContact.Data;
using APISystemContact.Models;
using APISystemContact.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using APISystemContact.Helper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using APISystemContact.DTOs;

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
            var users = await _dbContext.Users.ToListAsync();
            return users;

            
        }

        public List<String> GetAllUsersNames()
        {
            List<string> usersName = _dbContext.Users.Select(x => x.Name).ToList();

            string serializado = JsonConvert.SerializeObject(usersName, Formatting.Indented);

            File.WriteAllText("Data/nomesDeUsuarios.json", serializado);

            return usersName;
        }

        public async Task<User> GetById(int id)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
            {
                throw new Exception($"User with ID: {id} was not found in the database.");
            }
            return user;
        }

        /*public async Task<User> GetByName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new Exception("Nome não pode ser nulo ou vazio");
            }           
            
        }*/

        /*
        public async Task<User> Create(User user)
        {
            user.SetPasswordHash();
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
            return user;
        }         
        */

        public async Task<IEnumerable<User>> Create(IEnumerable<User> users)
        {
            foreach (var user in users)
            {
                user.SetPasswordHash();
                await _dbContext.Users.AddAsync(user);
            }

            await _dbContext.SaveChangesAsync();

            return users;
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
        
        public async Task<bool> Delete(int id/*, string password*/)
        {

            /* if (UserById == null)
             {
                 throw new Exception($"User with ID: {id} was not found in the database.");
             }else if(password.GenerateHash() == UserById.Password)
             {
                 _dbContext.Users.Remove(UserById);
                 await _dbContext.SaveChangesAsync();
                 return true;
             }*/
            User UserById = await GetById(id);
            if (UserById == null)
            {
                throw new Exception($"Contact with ID: {id} was not found in the database.");
            }
            _dbContext.Users.Remove(UserById);
            await _dbContext.SaveChangesAsync();
            return true;

        }
        
    }
}
