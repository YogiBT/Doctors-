using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication3.DAL;
using WebApplication3.Models;
using WebApplication3.ModelV;

namespace WebApplication3.Controllers
{
    public class DoctorController : Controller
    {
        // GET: Doctor
        /// <summary>
        /// Method to show the doctor all the appointments
        /// </summary>
        /// <returns></returns>
        public ActionResult ShowAppointments()
        {
            if (verifyDoctor() == false)
            {
                TempData["WarningMessage"] = "Don't go to places you are not allowed.";
                return RedirectToAction("LogOut", "Home");
            }
            QueueDAL que = new QueueDAL();


            string docID = Session["DoctorLoggedIn"].ToString();
            List<Queue> queueOBJ = (from x in que.Queues where x.DID.Equals(docID) select x).ToList<Queue>();
            ViewBag.QueueList = queueOBJ;

            //return Json(queueOBJ, JsonRequestBehavior.AllowGet);
            return View();

        }
        /// <summary>
        /// Method to delete a queue.
        /// </summary>
        /// <param name="date"></param>
        /// <param name="did"></param>
        /// <param name="mode"></param>
        /// <param name="pid"></param>
        /// <returns></returns>
        public ActionResult QueueDelete(DateTime date, string did, bool mode, string pid)
        {
            //verify the doctor
            if (verifyDoctor() == false)
            {
                TempData["WarningMessage"] = "Don't go to places you are not allowed.";
                return RedirectToAction("LogOut", "Home");
            }
            //open connection to dbo.tblDoctors DB.
            QueueDAL que = new QueueDAL();


            Queue queTemp = new Queue();
            queTemp.date = date;
            queTemp.DID = did;
            queTemp.mode = mode;
            queTemp.PID = pid;
            que.Queues.Attach(queTemp);
            que.Queues.Remove(queTemp);
            que.SaveChanges();
            return RedirectToAction("ShowAppointments", "Doctor");
        }
        /// <summary>
        /// Method to add a queue.
        /// </summary>
        /// <param name="que"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult QueueAdd(Queue que)
        {
            //verify 
            if (Session["DoctorLoggedIn"].ToString() != que.DID)
            {
                TempData["UnknownUser"] = "Wrong DID";
                return RedirectToAction("ShowAppointments", "Doctor");
            }
            if (verifyDoctor() == false)
            {
                TempData["WarningMessage"] = "Don't go to places you are not allowed.";
                return RedirectToAction("LogOut", "Home");
            }
            if (que.PID != null)
            {
                if (verifyPatient(que.PID.ToString()) == false)
                {
                    TempData["UnknownUser"] = "This user is not registerd to the system.";
                    return RedirectToAction("ShowAppointments", "Doctor");
                }
            }
            if (ModelState.IsValid)
            {

                QueueDAL queDAL = new QueueDAL();

                try
                {
                    //Add and save queue.
                    queDAL.Queues.Attach(que);
                    queDAL.Queues.Add(que);
                    queDAL.SaveChanges();
                    Session["DuplicateQueue"] = null;
                    List<Queue> objQueues = queDAL.Queues.ToList<Queue>();
                    //que = new Queue();
                    //return Json(Url.Action("ShowAppointments", "Doctor"));
                    return Json(objQueues, JsonRequestBehavior.AllowGet);
                    //return RedirectToAction("ShowAppointments", "Doctor");
                }
                catch (Exception)
                {
                    queDAL.Queues.Remove(que);
                    Session["DuplicateQueue"] = true;
                    List<Queue> objQueues = queDAL.Queues.ToList<Queue>();
                    //que = new Queue();
                    return Json(objQueues, JsonRequestBehavior.AllowGet);

                    //return RedirectToAction("ShowAppointments", "Doctor");
                }

            }
            Session["DuplicateQueue"] = null;
            QueueDAL queDAL2 = new QueueDAL();
            List<Queue> objQueues2 = queDAL2.Queues.ToList<Queue>();
            //return RedirectToAction("ShowAppointments", "Doctor");
            //que = new Queue();
            return Json(objQueues2, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddQueue()
        {
            if (verifyDoctor() == false)
            {
                TempData["WarningMessage"] = "Don't go to places you are not allowed.";
                return RedirectToAction("LogOut", "Home");
            }
            return View();
        }

        /// <summary>
        /// Method to show user info from queue table.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public ActionResult PatientInfo(string id)
        {
            if (verifyDoctor() == false)
            {
                TempData["WarningMessage"] = "Don't go to places you are not allowed.";
                return RedirectToAction("LogOut", "Home");
            }
            UserDAL queDAL = new UserDAL();
            List<User> usr = (from x in queDAL.Users where x.ID.Equals(id) select x).ToList<User>();
            ViewBag.TempUser = usr;
            return View();
        }

        public ActionResult GetQueuesByJson()
        {
            if (verifyDoctor() == false)
            {
                TempData["WarningMessage"] = "Don't go to places you are not allowed.";
                return RedirectToAction("LogOut", "Home");
            }
            QueueDAL dal = new QueueDAL();
            List<Queue> objQueues = dal.Queues.ToList<Queue>();
            return Json(objQueues, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Method to show current doctor information.
        /// </summary>
        /// <returns></returns>
        public ActionResult MyInfo()
        {
            if (verifyDoctor() == false)
            {
                TempData["WarningMessage"] = "Don't go to places you are not allowed.";
                return RedirectToAction("LogOut", "Home");
            }
            DoctorDAL queDAL = new DoctorDAL();
            var tempID = Session["DoctorLoggedIn"].ToString();
            List<Doctor> doc = (from x in queDAL.Doctors where x.DID.Equals(tempID) select x).ToList<Doctor>();

            ViewBag.TempDoc = doc;
            return View();
        }
        /// <summary>
        /// Method to edit the doctor information.
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit()
        {
            if (verifyDoctor() == false)
            {
                TempData["WarningMessage"] = "Don't go to places you are not allowed.";
                return RedirectToAction("LogOut", "Home");
            }
            DoctorDAL queDAL = new DoctorDAL();
            var tempID = Session["DoctorLoggedIn"].ToString();
            List<Doctor> doc = (from x in queDAL.Doctors where x.DID.Equals(tempID) select x).ToList<Doctor>();
            Doctor thisDoc = doc[0];
            return View(thisDoc);
        }
        public ActionResult Submit(Doctor doc)
        {
            if (verifyDoctor() == false)
            {
                TempData["WarningMessage"] = "Don't go to places you are not allowed.";
                return RedirectToAction("LogOut", "Home");
            }
            if (ModelState.IsValid)
            {
                using (var db = new DoctorDAL())
                {
                    db.Doctors.Attach(doc);
                    db.Entry(doc).Property(x => x.DID).IsModified = true;
                    db.Entry(doc).Property(x => x.FirstName).IsModified = true;
                    db.Entry(doc).Property(x => x.LastName).IsModified = true;
                    db.Entry(doc).Property(x => x.Age).IsModified = true;
                    db.Entry(doc).Property(x => x.Email).IsModified = true;
                    db.Entry(doc).Property(x => x.Phone).IsModified = true;
                    db.Entry(doc).Property(x => x.Password).IsModified = true;
                    db.SaveChanges();
                    return RedirectToAction("MyInfo", "Doctor");
                }

            }
            return RedirectToAction("MyInfo", "Doctor");
        }
        public bool verifyDoctor()
        {
            if (Session["DoctorLoggedIn"] != null)
                return true;
            return false;
        }
        public bool verifyPatient(string pid)
        {
            UserDAL usrDAL = new UserDAL();
            List<User> usr = (from x in usrDAL.Users where x.ID.Equals(pid) select x).ToList<User>();

            if (usr.Count == 0)
            {
                return false;
            }
            return true;
        }
        public ActionResult AddReview()
        {
            UserReviewDAL urDAL = new UserReviewDAL();
            List <UserReview> ur = urDAL.reviews.ToList<UserReview>();
            ViewBag.Reviews = ur;
            return View();

        }
        [HttpPost]
        public ActionResult AddReviewToDB()
        {
            string PID = Request.Form["PID"].ToString();
            string Review = Request.Form["Review"].ToString();
            string DID = Session["DoctorLoggedIn"].ToString();
            UserDAL usrDal = new UserDAL();
            List<User> usrList = (from x in usrDal.Users where x.ID == PID select x).ToList<User>();
            if (usrList.Count >=1)
            {
                UserReview ur = new UserReview();
                ur.UserId = PID;
                ur.Message = Review;
                ur.From = DID;
                UserReviewDAL urDAL = new UserReviewDAL();

                try
                {
                    urDAL.reviews.Add(ur);
                    urDAL.SaveChanges();
                }
                catch (Exception)
                {
                    //Failed insertion,already exist  

                    urDAL.reviews.Remove(ur);
                    //ViewBag.DuplicateUser = true;
                    return RedirectToAction("AddReview", "Doctor");
                }
            }
            else{
                TempData["nonUser"] = "User not in the system";
            }
           
            return RedirectToAction("AddReview", "Doctor");

        }
        public ActionResult ShowReview()
        {
            UserReviewDAL dal = new UserReviewDAL();
            List<UserReview> objReview = dal.reviews.ToList<UserReview>();
            return View(objReview);
        }

    }
}
    

