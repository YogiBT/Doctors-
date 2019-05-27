using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication3.Models;

namespace WebApplication3.VModel
{
    public class VMAdminUser
    {

        public Admin adm { get; set; }

        public User usr { get; set; }

        public VMAdminUser(Admin adm1 , User usr1)
        {
            adm = adm1;
            usr = usr1;
        }


    }
}