using APISystemContact.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APISystemContact.Data.Map
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(255);
            builder.Property(x => x.Email).IsRequired().HasMaxLength(150);
            builder.Property(x => x.Password).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Profile).IsRequired();
        }
    }
}
