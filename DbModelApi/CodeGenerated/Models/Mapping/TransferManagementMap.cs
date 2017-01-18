using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace CodeGenerated.Models.Mapping
{
    public class TransferManagementMap : EntityTypeConfiguration<TransferManagement>
    {
        public TransferManagementMap()
        {
            // Primary Key
            this.HasKey(t => t.ApplyId);

            // Properties
            this.Property(t => t.Topic)
                .HasMaxLength(300);

            this.Property(t => t.CompanyName)
                .HasMaxLength(300);

            this.Property(t => t.DeptCode)
                .HasMaxLength(300);

            this.Property(t => t.JobCode)
                .HasMaxLength(300);

            this.Property(t => t.TransferType)
                .HasMaxLength(300);

            this.Property(t => t.ProcessStatus)
                .HasMaxLength(300);

            this.Property(t => t.ProcessId)
                .HasMaxLength(300);

            this.Property(t => t.Attribute1)
                .HasMaxLength(300);

            this.Property(t => t.Attribute2)
                .HasMaxLength(300);

            this.Property(t => t.Attribute3)
                .HasMaxLength(300);

            this.Property(t => t.Attribute4)
                .HasMaxLength(300);

            this.Property(t => t.Attribute5)
                .HasMaxLength(300);

            this.Property(t => t.Attribute6)
                .HasMaxLength(300);

            this.Property(t => t.Attribute7)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("TransferManagement");
            this.Property(t => t.ApplyId).HasColumnName("ApplyId");
            this.Property(t => t.FactoringId).HasColumnName("FactoringId");
            this.Property(t => t.Topic).HasColumnName("Topic");
            this.Property(t => t.CompanyName).HasColumnName("CompanyName");
            this.Property(t => t.DeptCode).HasColumnName("DeptCode");
            this.Property(t => t.JobCode).HasColumnName("JobCode");
            this.Property(t => t.TransferType).HasColumnName("TransferType");
            this.Property(t => t.ProcessStatus).HasColumnName("ProcessStatus");
            this.Property(t => t.ProcessId).HasColumnName("ProcessId");
            this.Property(t => t.PassTime).HasColumnName("PassTime");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.Created).HasColumnName("Created");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.Modified).HasColumnName("Modified");
            this.Property(t => t.Attribute1).HasColumnName("Attribute1");
            this.Property(t => t.Attribute2).HasColumnName("Attribute2");
            this.Property(t => t.Attribute3).HasColumnName("Attribute3");
            this.Property(t => t.Attribute4).HasColumnName("Attribute4");
            this.Property(t => t.Attribute5).HasColumnName("Attribute5");
            this.Property(t => t.Attribute6).HasColumnName("Attribute6");
            this.Property(t => t.Attribute7).HasColumnName("Attribute7");
        }
    }
}
