using APISystemContact.Models;
using APISystemContact.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


namespace APISystemContact.Controllers
{
    [Route("api/contact")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactRepository _contactRepository;
        public ContactController(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }
        [HttpGet]
        public async Task<ActionResult<List<Contact>>> GetAllContacts()
        {
            List<Contact> contacts = await _contactRepository.GetAllContacts();
            return Ok(contacts);
        }

        [HttpGet("contactNames")]
        public ActionResult<List<String>> GetAllContactsNames()
        {

            List<String> contactsName = _contactRepository.GetAllContactsNames();            

            return Ok(contactsName);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Contact>> GetById(int id)
        {
            Contact contact = await _contactRepository.GetById(id);
            return Ok(contact);
        }



        [HttpPost]
        public async Task<ActionResult<Contact>> Create([FromBody] Contact contactModel)
        {
            Contact contact = await _contactRepository.Create(contactModel);
            return Ok(contact);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Contact>> Update([FromBody] Contact contactModel, int id)
        {
            contactModel.Id = id;
            Contact contact = await _contactRepository.Update(contactModel, id);
            return Ok(contact);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            bool delete = await _contactRepository.Delete(id);
            return Ok(delete);
        }

    }
}
