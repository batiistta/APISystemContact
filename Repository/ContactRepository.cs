using APISystemContact.Data;
using APISystemContact.Models;
using APISystemContact.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace APISystemContact.Repository
{
    public class ContactRepository : IContactRepository
    {
        //"readonly significa que seu valor só pode ser atribuído uma vez durante
        //a inicialização e não pode ser alterado posteriormente.
        private readonly APISystemContactDBContext _dbContext;
        public ContactRepository(APISystemContactDBContext aPISystemContactDBContext)
        {
            _dbContext = aPISystemContactDBContext;
        }

        public async Task<List<Contact>> GetAllContacts()
        {
            return await _dbContext.Contacts
                .Include(x => x.User)
                .ToListAsync();
        }

        public List<String> GetAllContactsNames()
        {            
            List<string> contactsName = _dbContext.Contacts.Select(x => x.Name).ToList();

            string serializado = JsonConvert.SerializeObject(contactsName, Formatting.Indented);

            File.WriteAllText("Data/nomes.json", serializado);

            return contactsName;
        }     

        public async Task<Contact> GetById(int id)
        {
            return await _dbContext.Contacts
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Contact> Create(Contact contact)
        {
            await _dbContext.Contacts.AddAsync(contact);
            await _dbContext.SaveChangesAsync();
            return contact;
        }

        public async Task<Contact> Update(Contact contact, int id)
        {
            Contact ContactById = await GetById(id);
            if(ContactById == null)
            {
                throw new Exception($"Contact with ID: {id} was not found in the database.");
            }
            ContactById.Name = contact.Name;
            ContactById.Email = contact.Email;
            ContactById.Phone = contact.Phone;
            ContactById.UserId = contact.Id;

            _dbContext.Contacts.Update(ContactById);
            await _dbContext.SaveChangesAsync();
            return ContactById;
        }        
        
        public async Task<bool> Delete(int id)
        {
            Contact ContactById = await GetById(id);
            if (ContactById == null)
            {
                throw new Exception($"Contact with ID: {id} was not found in the database.");
            }
            _dbContext.Contacts.Remove(ContactById);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        
    }
}
