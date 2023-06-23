using APISystemContact.DTOs;
using APISystemContact.Models;
using APISystemContact.Repository;
using APISystemContact.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IO;

namespace APISystemContact.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet("getAllUsers")]
        public async Task<ActionResult> GetAllUsers()
        {
            List<User> users = await _userRepository.GetAllUsers();

            var usersDTO = new List<UserDTO>();
            foreach (var user in users)
            {
                UserDTO userDTO = new UserDTO();
                userDTO.Name = user.Name;
                userDTO.Email = user.Email;
                userDTO.Profile = user.Profile;

                usersDTO.Add(userDTO);
            }
            
            string serializado = JsonConvert.SerializeObject(users, Formatting.Indented);
                        
            System.IO.File.WriteAllText("Data/users.json", serializado);

            return Ok(usersDTO);
        }

        [HttpGet("usersNames")]
        public ActionResult GetAllUsersNames()
        {

            List<String> usersName = _userRepository.GetAllUsersNames();

            return Ok(usersName);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            User user = await _userRepository.GetById(id);
            return Ok(user);
        }

        /*
         [HttpPost]
         public async Task<ActionResult> Create([FromBody] User userModel)
         {            
             User user = await _userRepository.Create(userModel);
             return Ok(user);
         }
        */

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] IEnumerable<User> userModels)
        {
            IEnumerable<User> users = await _userRepository.Create(userModels);
            return Ok(users);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult> Update([FromBody] User userModel, int id)
        {
            userModel.Id = id;
            User user = await _userRepository.Update(userModel, id);
            return Ok(user);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id/*, string password*/)
        {

            bool delete = await _userRepository.Delete(id/*,password*/);
            return Ok(delete);
        }

    }
}
