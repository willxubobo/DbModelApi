using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace CodeGenerated.Models.Mapping
{
    public class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            // Primary Key
            this.HasKey(t => t.UserId);

            // Properties
            this.Property(t => t.UserName)
                .HasMaxLength(100);

            this.Property(t => t.Pwd)
                .HasMaxLength(200);

            this.Property(t => t.NickName)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("User");
            this.Property(t => t.UserId).HasColumnName("UserId");
            this.Property(t => t.UserName).HasColumnName("UserName");
            this.Property(t => t.Pwd).HasColumnName("Pwd");
            this.Property(t => t.NickName).HasColumnName("NickName");
        }
    }
}
