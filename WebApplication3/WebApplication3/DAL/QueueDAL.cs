using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApplication3.Models;

namespace WebApplication3.DAL
{
    public class QueueDAL : DbContext
    {
        /// <summary>
        /// Create a connection to queue DB.
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<QueueDAL>(null);
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Queue>().ToTable("tblQueue");
        }
        public DbSet<Queue> Queues { get; set; }
    }
    
}