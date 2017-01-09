using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbModelApi.Model;

namespace DbModelApi.BLL
{
    public class DBContext : DbContext
    {
        static DBContext()
        {
            Database.SetInitializer<DBContext>(null);
        }
        public DBContext()
            : base("Name=DBContext")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }
        public DbSet<TransferDetail> TransferDetails { get; set; }
        public DbSet<TransferManagement> TransferManagements { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
