using APISystemContact.Enums;

namespace APISystemContact.DTOs
{
    public class UserDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public ProfileStatus Profile { get; set; }
    }
}
