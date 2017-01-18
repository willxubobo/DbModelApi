using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace CodeGenerated.Models.Mapping
{
    public class StudentMap : EntityTypeConfiguration<Student>
    {
        public StudentMap()
        {
            // Primary Key
            this.HasKey(t => t.stuID);

            // Properties
            this.Property(t => t.stuName)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Student");
            this.Property(t => t.stuID).HasColumnName("stuID");
            this.Property(t => t.stuName).HasColumnName("stuName");
            this.Property(t => t.deptID).HasColumnName("deptID");

            // Relationships
            this.HasRequired(t => t.Department)
                .WithMany(t => t.Students)
                .HasForeignKey(d => d.deptID);

        }
    }
}
