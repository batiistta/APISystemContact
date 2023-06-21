using APISystemContact.Models;

namespace APISystemContact.Repository.Interfaces
{
    public interface IContactRepository
    {
        Task<List<Contact>> GetAllContacts();    
        List<String> GetAllContactsNames();
        Task<Contact> GetById(int id);
        Task<Contact> Create(Contact contatc);
        Task<Contact> Update(Contact contact, int id);
        Task<bool> Delete(int id);


    }
}
