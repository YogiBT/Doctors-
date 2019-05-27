using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApplication3.Models;

namespace WebApplication3.DAL
{
    public class TermsDAL : DbContext
    {
        /// <summary>
        /// Create a connection to admin DB.
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<TermsDAL>(null);
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<uTerms>().ToTable("tblTerms");
        }
        public DbSet<uTerms> theTerms { get; set; }
    }
}