using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Classes;
using WebApplication3.DAL;
using WebApplication3.Models;
using WebApplication3.ModelV;
using CaptchaMvc.HtmlHelpers;

namespace WebApplication3.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        /// <summary>
        ///This part open the connection to DB and insert+save a new user in dbo.tblLoggers DB
        /// </summary>
        /// <param name="usr"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Submit(User usr)//signin
        {
            
            if (ModelState.IsValid && this.IsCaptchaValid("Validate your captcha"))
            {
                
                UserDAL usrDal = new UserDAL();
                Encryption enc = new Encryption();
                string hashPassword = enc.CreateHash(usr.Password);
                usr.Password = hashPassword;
                //usr.Password = "5YBBjK9M17yegWleKmAYm09bMUYvzL50:LTdfwWw4hLGrvENTcLnajhGhrnMyHz3V";
                try
                {
                    usrDal.Users.Add(usr);
                    usrDal.SaveChanges();
                }
                catch (Exception)
                {
                    //Failed insertion,already exist  
                    
                    usrDal.Users.Remove(usr);
                    ViewBag.DuplicateUser = true;
                    return View("Enter",usr);
                }
                ///Create sessions for further use
                Session["UserLoggedIn"] = usr.ID;
                usr = new User();
                return View("SignInSucc",usr);
            }
            ViewBag.FormValidation = false;
            return View("Enter",usr);
        }
        
        public ActionResult Enter()
        {

            return View(new User());
        }

        /* public ActionResult ShowAppointment()
         {
             QueueDAL que = new QueueDAL();
             List<Queue> ques = que.Queues.ToList<Queue>();
             VMQueue vmQue = new VMQueue();
             vmQue.queues = ques;
             //ViewBag.QueueList = ques;
             return View(vmQue);
         }*/

        /// <summary>
        /// Method to find appointment
        /// </summary>
        public ActionResult FindAppointment()
        {
            if (verifyUser() == false)
            {
                TempData["WarningMessage"] = "Don't go to places you are not allowed.";
                return RedirectToAction("LogOut", "Home");
            }
            return View(getQueueVM());
        }

        /// <summary>
        /// method to show appointments for user
        /// </summary>
        /// <returns></returns>
        public ActionResult ViewAppointments()
        {
            if (verifyUser() == false)
            {
                TempData["WarningMessage"] = "Don't go to places you are not allowed.";
                return RedirectToAction("LogOut", "Home");
            }
            //QueueDAL que = new QueueDAL();
            //List<Queue> ques = que.Queues.ToList<Queue>();
            //VMQueue vmQue = new VMQueue();
            // vmQue.queues = ques;
            //ViewBag.QueueList = ques;
            return View(getQueueVM());
        }
        public VMQueue getQueueVM()
        {
            QueueDAL que = new QueueDAL();
            List<Queue> ques = que.Queues.ToList<Queue>();
            VMQueue vmQue = new VMQueue();
            vmQue.queues = ques;
            //ViewBag.QueueList = ques;
            return vmQue;
        }
        /// <summary>
        /// Add appointment to queue list
        /// </summary>
        /// <param name="date"></param>
        /// <param name="did"></param>
        /// <returns></returns>
        public ActionResult QueueSelect(DateTime date,string did) // add the patient (update) turn to queue
        {
            //Session["UserLoggedID"]
            if (verifyUser() == false)
            {
                TempData["WarningMessage"] = "Don't go to places you are not allowed.";
                return RedirectToAction("LogOut", "Home");
            }
            Queue queTemp = new Queue();
            queTemp.date = date;
            queTemp.DID = did;
            queTemp.PID = Session["UserLoggedIn"].ToString();
            queTemp.mode = false;

            using(var db=new QueueDAL())
            {
                db.Queues.Attach(queTemp);
                db.Entry(queTemp).Property(x => x.PID).IsModified = true;
                db.Entry(queTemp).Property(x => x.mode).IsModified = true;
                db.SaveChanges();

            }


            //que.Queues.Add(queTemp);
            //que.SaveChanges();


            return RedirectToAction("ViewAppointments", "User");
        }


        /// <summary>
        /// Personal info.
        /// </summary>
        /// <returns></returns>
        public ActionResult MyInfo()
        {
            if (verifyUser() == false)
            {
                TempData["WarningMessage"] = "Don't go to places you are not allowed.";
                return RedirectToAction("LogOut", "Home");
            }
            UserDAL queDAL = new UserDAL();
            var tempID = Session["UserLoggedIn"].ToString();
            List<User> usr = (from x in queDAL.Users where x.ID.Equals(tempID) select x).ToList<User>();
            ViewBag.TempUser = usr;
            return View();
        }

        /// <summary>
        /// Method to verify user.
        /// </summary>
        /// <returns></returns>
        public bool verifyUser()
        {
            if (Session["UserLoggedIn"] != null)
                return true;
            return false;
        }
    }

    
}