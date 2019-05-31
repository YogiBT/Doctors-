using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApplication3.Models;
using WebApplication3.ModelV;


namespace WebApplication3.DAL
{
    public class ReplyDAL : DbContext
    {
        /// <summary>
        /// Create a connection to queue DB.
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<ReplyDAL>(null);
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Reply>().ToTable("tblReply");
        }
        public DbSet<Reply> Replies { get; set; }
    }
   
}