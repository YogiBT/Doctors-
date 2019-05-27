using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApplication3.Models;

namespace WebApplication3.DAL
{
    public class UserDAL : DbContext
    {
        /// <summary>
        /// Create a connection to users DB.
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<UserDAL>(null);
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().ToTable("tblUsers");
        }
        public DbSet<User> Users { get; set; }
    }
}