using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApplication3.Models;

namespace WebApplication3.DAL
{
    public class DoctorDAL : DbContext
    {

        /// <summary>
        /// Create a connection to doctor DB.
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<DoctorDAL>(null);
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Doctor>().ToTable("tblDoctor");
        }
        public DbSet<Doctor> Doctors { get; set; }
    }
}