using System.ComponentModel;

namespace APISystemContact.Enums
{
    public enum ProfileStatus
    {
        [Description("Default profile")]
        DefaultProfile= 0,
        [Description("Admin profile")]
        AdminProfile = 1
    }
}
