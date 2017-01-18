using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using CodeGenerated.Models.Mapping;

namespace CodeGenerated.Models
{
    public partial class DbCodeFirstApiContext : DbContext
    {
        static DbCodeFirstApiContext()
        {
            Database.SetInitializer<DbCodeFirstApiContext>(null);
        }

        public DbCodeFirstApiContext()
            : base("Name=DbCodeFirstApiContext")
        {
        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Score> Scores { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<TransferDetail> TransferDetails { get; set; }
        public DbSet<TransferManagement> TransferManagements { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new DepartmentMap());
            modelBuilder.Configurations.Add(new ScoreMap());
            modelBuilder.Configurations.Add(new StudentMap());
            modelBuilder.Configurations.Add(new TransferDetailMap());
            modelBuilder.Configurations.Add(new TransferManagementMap());
            modelBuilder.Configurations.Add(new UserMap());
        }
    }
}
