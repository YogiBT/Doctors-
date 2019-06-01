using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApplication3.Models;

namespace WebApplication3.DAL
{
    public class UserReviewDAL : DbContext
    {
        /// <summary>
        /// Create a connection to queue DB.
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<UserReviewDAL>(null);
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserReview>().ToTable("tblUserReview");
        }
        public DbSet<UserReview> reviews { get; set; }
    }
}