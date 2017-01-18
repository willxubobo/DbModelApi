using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace CodeGenerated.Models.Mapping
{
    public class ScoreMap : EntityTypeConfiguration<Score>
    {
        public ScoreMap()
        {
            // Primary Key
            this.HasKey(t => new { t.stuID, t.category, t.score1 });

            // Properties
            this.Property(t => t.stuID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.category)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.score1)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("Score");
            this.Property(t => t.stuID).HasColumnName("stuID");
            this.Property(t => t.category).HasColumnName("category");
            this.Property(t => t.score1).HasColumnName("score");
        }
    }
}
