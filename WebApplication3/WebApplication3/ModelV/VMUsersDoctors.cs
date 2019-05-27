using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication3.DAL;
using WebApplication3.Models;

namespace WebApplication3.ModelV
{
    public class VMUsersDoctors
    {
        public List<User> users { get; set; }
        public List <Doctor> doctors { get; set; }

        public VMUsersDoctors(List<Doctor> doc1, List<User> usr1)
        {
            doctors = doc1;
            users = usr1;
        }
    }
}