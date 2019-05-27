using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Classes;
using WebApplication3.DAL;
using WebApplication3.Models;
using WebApplication3.ModelV;


namespace WebApplication3.Controllers
{

    public class HomeController : Controller
    {
        public void PieChartInsert()
        {
            UserDAL usr = new UserDAL();
            DoctorDAL doc = new DoctorDAL();
            var countUsers = (from x in usr.Users where x.ID != null select x).Count();
            var countDoctors = (from x in doc.Doctors where x.DID != null select x).Count();
            float temp1 = (float)countUsers;
            float temp2 = (float)countDoctors;
            float sum = temp1 + temp2;
            temp1 = (temp1 / sum) * 100;
            temp2 = (temp2 / sum) * 100;

            Session["countUsers"] = countUsers;
            Session["countDoctors"] = countDoctors;


        }

        /// <summary>
        /// Method to logot and return homepage.
        /// </summary>
        /// <returns></returns>
        public ActionResult LogOut()
        {
            //reset all 'logged' sessions
            Session["UserLoggedIn"] = null;
            Session["AdminLoggedIn"] = null;
            Session["DoctorLoggedIn"] = null;
            Session["DuplicateQueue"] = null;
            Session["DuplicateDoctor"] = null;

            return RedirectToAction("ShowHomePage","Home");
        }
        /// <summary>
        /// Submit method, enter details and validate.
        /// </summary>
        /// <returns></returns>
        public ActionResult Submit()
        {
            string txtID = Request.Form["txtID"].ToString();
            string txtPassword = Request.Form["txtPassword"].ToString();
            Encryption enc = new Encryption();
            if (!ModelState.IsValid)
            {
                Session["WarningMessage"] = "Wrong Username or Password !";
                return View("ShowHomePage", "Home");
            }

            AdminDAL adm = new AdminDAL();
            UserDAL usr = new UserDAL();
            DoctorDAL doc = new DoctorDAL();

            List<Admin> admOBJ = (from x in adm.Admins where x.userName.Equals(txtID) select x).ToList<Admin>();
            //foreach (var item in admOBJ)
            if ( admOBJ.Count != 0)
            {

                //if (item.password.Equals(txtPassword))
                if (enc.ValidatePassword(txtPassword, admOBJ[0].password))
                {
                    Session["AdminLoggedIn"] = txtID;
                    return RedirectToAction("ShowAllUsers", "Admin");
                }
                else
                    Session["WarningMessage"] = "Wrong Username or Password !";
            }

            List<Doctor> docOBJ = (from x in doc.Doctors where x.DID.Equals(txtID) select x).ToList<Doctor>();
            //foreach (var item in docOBJ)
            if (docOBJ.Count != 0)
            {
                //if (item.Password.Equals(txtPassword))
                if (enc.ValidatePassword(txtPassword, docOBJ[0].Password))
                {
                    Session["DoctorLoggedIn"] = txtID;
                    return RedirectToAction("ShowAppointments", "Doctor");
                }
                else
                    Session["WarningMessage"] = "Wrong Username or Password !";
            }

            List<User> usrOBJ = (from x in usr.Users where x.ID.Equals(txtID) select x).ToList<User>();
            //foreach (var item in usrOBJ)
            if (usrOBJ.Count != 0)
            {
                //if (item.Password.Equals(txtPassword))
                if (enc.ValidatePassword(txtPassword, usrOBJ[0].Password))
                {
                    Session["UserLoggedIn"] = txtID;
                    return RedirectToAction("ShowHomePage", "Home");
                }
                else
                    Session["WarningMessage"] = "Wrong Username or Password !";
            }



            return View("ShowHomePage");
        }

        /// <summary>
        /// Redirect to login view.
        /// </summary>
        /// <returns></returns>
        public ActionResult LogIn()
        {
            return View();
        }

        
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// Redirect to homepage view.
        /// </summary>
        /// <returns></returns>
        public ActionResult ShowHomePage()
        {
            return View();
        }

        /// <summary>
        /// Redirect to 'about' view.
        /// </summary>
        /// <returns></returns>
        public ActionResult About()
        {
            //PieChartInsert();
            TermsDAL tdm = new TermsDAL();
            List<uTerms> usr = tdm.theTerms.ToList<uTerms>();
            ViewBag.Terms = usr;
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}