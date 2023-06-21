using APISystemContact.Models;
using APISystemContact.Repository;
using APISystemContact.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
        [HttpGet]
        public async Task<ActionResult<List<User>>> GetAllUsers()
        {
            List<User> users = await _userRepository.GetAllUsers();
            return Ok(users);
        }

        [HttpGet("usersNames")]
        public ActionResult<List<String>> GetAllUsersNames()
        {

            List<String> usersName = _userRepository.GetAllUsersNames();

            return Ok(usersName);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetById(int id)
        {
            User user = await _userRepository.GetById(id);
            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<User>> Create([FromBody] User userModel)
        {            
            User user = await _userRepository.Create(userModel);
            return Ok(user);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<User>> Update([FromBody] User userModel, int id)
        {
            userModel.Id = id;
            User user = await _userRepository.Update(userModel, id);
            return Ok(user);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id, string password)
        {

            bool delete = await _userRepository.Delete(id,password);
            return Ok(delete);
        }

    }
}
