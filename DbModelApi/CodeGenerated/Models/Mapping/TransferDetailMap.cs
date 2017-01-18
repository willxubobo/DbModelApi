using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace CodeGenerated.Models.Mapping
{
    public class TransferDetailMap : EntityTypeConfiguration<TransferDetail>
    {
        public TransferDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("TransferDetail");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ApplyId).HasColumnName("ApplyId");
            this.Property(t => t.InvoiceNo).HasColumnName("InvoiceNo");
            this.Property(t => t.Amount).HasColumnName("Amount");
            this.Property(t => t.Currency).HasColumnName("Currency");
            this.Property(t => t.Summary).HasColumnName("Summary");
            this.Property(t => t.ReceiveAccountName).HasColumnName("ReceiveAccountName");
            this.Property(t => t.ReceiveAccount).HasColumnName("ReceiveAccount");
            this.Property(t => t.ReceiveOpeningBank).HasColumnName("ReceiveOpeningBank");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.Created).HasColumnName("Created");
            this.Property(t => t.Attribute1).HasColumnName("Attribute1");
            this.Property(t => t.Attribute2).HasColumnName("Attribute2");
            this.Property(t => t.Attribute3).HasColumnName("Attribute3");

            // Relationships
            this.HasOptional(t => t.TransferManagement)
                .WithMany(t => t.TransferDetails)
                .HasForeignKey(d => d.ApplyId);

        }
    }
}
