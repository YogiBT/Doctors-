using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApplication3.Models;

namespace WebApplication3.DAL
{
    public class AdminDAL : DbContext
    {
        /// <summary>
        /// Create a connection to admin DB.
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<AdminDAL>(null);
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Admin>().ToTable("tblAdmins");
        }
        public DbSet<Admin> Admins { get; set; }
    }

    
}