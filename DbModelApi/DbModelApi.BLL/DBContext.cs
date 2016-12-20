﻿using System;
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
        public DbSet<TransferDetail> TransferDetails { get; set; }
        public DbSet<TransferManagement> TransferManagements { get; set; }
    }
}
