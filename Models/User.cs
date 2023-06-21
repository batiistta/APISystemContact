using APISystemContact.Enums;
using APISystemContact.Helper;

namespace APISystemContact.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public ProfileStatus Profile { get; set; }
        public bool ValidPassword(string senha)
        {
            return Password == Password.GenerateHash();
        }

        public void SetPasswordHash()
        {
            Password = Password.GenerateHash();
        }
    }
}
