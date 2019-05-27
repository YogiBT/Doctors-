using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication3.DAL;
using WebApplication3.Models;
using WebApplication3.ModelV;
using WebApplication3.Classes;

/*Integrated Security=True*/
namespace WebApplication3.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        /// <summary>
        /// Method to show all current doctors & users
        /// </summary>
        /// <returns></returns>
        public ActionResult ShowAllUsers()
        {
            //if (verifyAdmin() == false)
            //{
            //    TempData["WarningMessage"] = "Don't go to places you are not allowed.";
                
            //    return RedirectToAction("LogOut", "Home");
            //}
            UserDAL dalUser = new UserDAL();
            DoctorDAL dalDoc = new DoctorDAL();
            VMUsersDoctors vm = new VMUsersDoctors(dalDoc.Doctors.ToList<Doctor>(), dalUser.Users.ToList<User>());

            return View(vm);

        }
        /// <summary>
        /// Method to delete doctor from DB.
        /// </summary>
        /// <param name="docID"></param>
        /// <returns></returns>
        public ActionResult DeleteDoctor(string docID)
        {
            if (verifyAdmin() == false)
            {
                Session["TempData"] = "Don't go to places you are not allowed.";
                return RedirectToAction("LogOut", "Home");
            }
            DoctorDAL docDAL = new DoctorDAL();
            List<Doctor> doc = (from x in docDAL.Doctors where x.DID.Equals(docID) select x).ToList<Doctor>();
            Doctor docToDelete = doc[0];
            docDAL.Doctors.Attach(docToDelete);
            docDAL.Doctors.Remove(docToDelete);
            docDAL.SaveChanges();
            return RedirectToAction("ShowDoctors", "Admin");
        }
        /// <summary>
        /// Method to show doctors
        /// </summary>
        /// <returns></returns>
        public ActionResult ShowDoctors()
        {
            if (verifyAdmin() == false)
            {
                TempData["WarningMessage"] = "Don't go to places you are not allowed.";
                return RedirectToAction("LogOut", "Home");
            }
            //UserDAL dalUser = new UserDAL();
            DoctorDAL dalDoc = new DoctorDAL();
            List<Doctor> docs = dalDoc.Doctors.ToList<Doctor>();
            ViewBag.DoctorsList = docs;
            //VMUsersDoctors vm = new VMUsersDoctors(dalDoc.Doctors.ToList<Doctor>(), dalUser.Users.ToList<User>());


            return View();
        }
        /// <summary>
        /// Method to add new doctor to the DB.
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        public ActionResult AddDoctor(Doctor doc)
        {
            if (verifyAdmin() == false)
            {
                TempData["WarningMessage"] = "Don't go to places you are not allowed.";
                return RedirectToAction("LogOut", "Home");
            }
            if (ModelState.IsValid)
            {
                DoctorDAL docDAL = new DoctorDAL();
                Encryption enc = new Encryption();
                string hashPassword = enc.CreateHash(doc.Password);
                doc.Password = hashPassword;
                try
                {
                    docDAL.Doctors.Attach(doc);
                    docDAL.Doctors.Add(doc);
                    docDAL.SaveChanges();
                    TempData["DuplicateDoctor"] = null;
                    return RedirectToAction("ShowDoctors", "Admin");
                }
                catch (Exception)
                {
                    docDAL.Doctors.Remove(doc);
                    TempData["DuplicateDoctor"] = true;
                    return RedirectToAction("ShowDoctors", "Admin");
                }

            }
            TempData["DuplicateDoctor"] = null;
            return RedirectToAction("ShowDoctors", "Admin");
            
        }
        /// <summary>
        /// Method to show current doctors.
        /// </summary>
        /// <returns></returns>
        public ActionResult ShowQueues()
        {
            if (verifyAdmin() == false)
            {
                TempData["WarningMessage"] = "Don't go to places you are not allowed.";
                return RedirectToAction("LogOut", "Home");
            }
            QueueDAL queDAL = new QueueDAL();
            VMQueue queVM = new VMQueue();
            queVM.queues = queDAL.Queues.ToList<Queue>();
            return View(queVM);
        }


        /// <summary>
        /// Method to delete a user from DB.
        /// </summary>
        /// <param name="usrID"></param>
        /// <returns></returns>
        public ActionResult DeleteUser(string usrID)
        {
            if (verifyAdmin() == false)
            {
                TempData["WarningMessage"] = "Don't go to places you are not allowed.";
                return RedirectToAction("LogOut", "Home");
            }
            UserDAL usrDAL = new UserDAL();
            List<User> usr = (from x in usrDAL.Users where x.ID.Equals(usrID) select x).ToList<User>();
            User usrToDelete = usr[0];
            usrDAL.Users.Attach(usrToDelete);
            usrDAL.Users.Remove(usrToDelete);
            usrDAL.SaveChanges();
            return RedirectToAction("ShowUsers", "Admin");
        }

        public ActionResult ShowUsers()
        {
            if (verifyAdmin() == false)
            {
                TempData["WarningMessage"] = "Don't go to places you are not allowed.";
                return RedirectToAction("LogOut", "Home");
            }
            //UserDAL dalUser = new UserDAL();
            UserDAL usrDoc = new UserDAL();
            List<User> usrs = usrDoc.Users.ToList<User>();
            ViewBag.UsersList = usrs;
            //VMUsersDoctors vm = new VMUsersDoctors(dalDoc.Doctors.ToList<Doctor>(), dalUser.Users.ToList<User>());


            return View();
        }
        /// <summary>
        /// add new user to DB.
        /// </summary>
        /// <param name="usr"></param>
        /// <returns></returns>
        public ActionResult AddUser(User usr)
        {
            if (verifyAdmin() == false)
            {
                TempData["WarningMessage"] = "Don't go to places you are not allowed.";
                return RedirectToAction("LogOut", "Home");
            }
            if (ModelState.IsValid)
            {
                UserDAL usrDAL = new UserDAL();
                Encryption enc = new Encryption();
                string hashPassword = enc.CreateHash(usr.Password);
                usr.Password = hashPassword;
                try
                {
                    usrDAL.Users.Attach(usr);
                    usrDAL.Users.Add(usr);
                    usrDAL.SaveChanges();
                    TempData["DuplicateUser"] = null;
                    return RedirectToAction("ShowUsers", "Admin");
                }
                catch (Exception)
                {
                    usrDAL.Users.Remove(usr);
                    TempData["DuplicateUser"] = true;
                    return RedirectToAction("ShowUsers", "Admin");
                }

            }
            TempData["DuplicateUser"] = null;
            return RedirectToAction("ShowUsers", "Admin");

        }
        public ActionResult MyInfo()
        {
            if (verifyAdmin() == false)
            {
                TempData["WarningMessage"] = "Don't go to places you are not allowed.";
                return RedirectToAction("LogOut", "Home");
            }
            AdminDAL admDAL = new AdminDAL();
            var tempID = Session["AdminLoggedIn"].ToString();
            List<Admin> admins = (from x in admDAL.Admins where x.userName.Equals(tempID) select x).ToList<Admin>();
            VMAdmin myAdmins = new VMAdmin();
            myAdmins.Admins = admins;
            //ViewBag.TempDoc = doc;
            return View(myAdmins);
        }
        public bool verifyAdmin()
        {
            if (Session["AdminLoggedIn"] != null)
                return true;
            return false;
        }
    }
}