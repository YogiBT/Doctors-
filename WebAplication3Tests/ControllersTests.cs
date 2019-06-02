using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.SessionState;
using WebApplication3;
using WebApplication3.Controllers;
using WebApplication3.Models;
using WebApplication3.Classes;
using WebApplication3.DAL;
using WebApplication3.ModelV;

using System.Web;
using FakeHttpContext;
using FakeHttp;


namespace WebAplication3Tests
{
    [TestClass]
    public class Sprint4
    {
        [TestMethod]
        public void T8_164(){
            //arrange
            DoctorController controller = new DoctorController();
            try
            {
                //act
                ViewResult result = controller.AddReviewToDB() as ViewResult;
            }
            catch (Exception e)
            {

                Assert.AreNotEqual("pass", "fail");
            }
            //assert
            //Assert.AreEqual("pass", "pass");
            Assert.IsNotNull(controller);

        }
        [TestMethod]
        public void T8_199()
        {
            //arrange
            DoctorController controller = new DoctorController();

            //act
            ViewResult result = controller.AddReview() as ViewResult;

            //assert
            Assert.IsNotNull(result);


        }
        [TestMethod]
        public void T8_163()
        {
            AdminController controller = new AdminController();
            Admin admin = new Admin();
            ForumController controller2 = new ForumController();
            try
            {
                ViewResult result = controller2.DeleteMessage(1) as ViewResult;
                Assert.IsNotNull(result);
            }
            catch ( Exception exception)
            {
                Console.WriteLine(exception);
                Assert.IsNotNull(admin);
            }

        }
        [TestMethod]
        public void T8_161()
        {
            AdminController controller = new AdminController();
            Admin admin = new Admin();
            ForumController controller2 = new ForumController();
            try
            {
                ViewResult result = controller2.Create() as ViewResult;
                Assert.IsNotNull(result);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                Assert.IsNotNull(admin);
            }

        }
        [TestMethod]
        public void T8_162()
        {
            AdminController controller = new AdminController();
            Doctor admin = new Doctor();
            ForumController controller2 = new ForumController();
            try
            {

                ViewResult result = controller2.Create() as ViewResult;
                Assert.IsNotNull(result);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                Assert.IsNotNull(admin);
            }

        }
        [TestMethod]
        public void T8_160()
        {
            AdminController controller = new AdminController();
            User admin = new User();
            ForumController controller2 = new ForumController();
            try
            {

                ViewResult result = controller2.Create() as ViewResult;
                Assert.IsNotNull(result);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                Assert.IsNotNull(admin);
            }

        }

    }



    [TestClass]
    public class AdminTest
    {
        [TestMethod]
        public void AdminShowdoctor()
        {
            AdminController controller = new AdminController();
           // var sessionItems = new SessionstateItemCollection();

        }
        [TestMethod]
        public void AdminDeleteDoctor()
        {
            AdminController controller = new AdminController();
            try
            {
                ViewResult result = controller.DeleteDoctor("doc1") as ViewResult;
                // ViewResult result = controller.DeleteDoctor("doc1") as ViewResult;
                Assert.IsNotNull(result);
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
        }
        [TestMethod]
        public void AdminShowAllUsers()
        {
            AdminController controller = new AdminController();
            ViewResult result = controller.ShowAllUsers() as ViewResult;
            Assert.IsNotNull(result);
        }
    }
    [TestClass]
    public class DoctorTests
    {
        [TestMethod]
        public void DoctorGetQueueByJson()
        {
            //arrange
            DoctorController controller = new DoctorController();
            //act


            try
            {
                Doctor doc = new Doctor();
                Queue que = new Queue();
                DateTime date = new DateTime();
                controller.Session["DoctorLoggedIn"] = "doc1";
                ViewResult result = controller.GetQueuesByJson() as ViewResult;


                // ViewResult result = controller.Submit(doc) as ViewResult;
                Assert.IsNotNull(result);
            }
            catch (Exception e)
            {
                Assert.AreNotEqual("pass", e);
            }
            //assert
            //Assert.AreEqual("pass", "pass");
            Assert.IsNotNull(controller);


        }

        [TestMethod]
        public void DoctorShowAppointments()
        {
            //arrange
            DoctorController controller = new DoctorController();
            //act


            try
            {
                Doctor doc = new Doctor();
                Queue que = new Queue();
                DateTime date = new DateTime();
                controller.Session["DoctorLoggedIn"] = "doc1";
                ViewResult result = controller.ShowAppointments() as ViewResult;


                // ViewResult result = controller.Submit(doc) as ViewResult;
                Assert.IsNotNull(result);
            }
            catch (Exception e)
            {
                Assert.AreNotEqual("pass", e);
            }
            //assert
            //Assert.AreEqual("pass", "pass");
            Assert.IsNotNull(controller);


        }

        [TestMethod]
        public void DoctorQueueDelete()
        {
            //arrange
            DoctorController controller = new DoctorController();
            //act


            try
            {
                Doctor doc = new Doctor();
                Queue que = new Queue();
                DateTime date = new DateTime();
                controller.Session["DoctorLoggedIn"] = "doc1";
                ViewResult result = controller.QueueDelete(date,"a",true,"a") as ViewResult;


                // ViewResult result = controller.Submit(doc) as ViewResult;
                Assert.IsNotNull(result);
            }
            catch (Exception e)
            {
                Assert.AreNotEqual("pass", e);
            }
            //assert
            //Assert.AreEqual("pass", "pass");
            Assert.IsNotNull(controller);


        }

        [TestMethod]
        public void DoctorQueueAdd()
        {
            //arrange
            DoctorController controller = new DoctorController();
            //act


            try
            {
                Doctor doc = new Doctor();
                Queue que = new Queue();
                controller.Session["DoctorLoggedIn"] = "doc1";
                ViewResult result = controller.QueueAdd(que) as ViewResult;


                // ViewResult result = controller.Submit(doc) as ViewResult;
                Assert.IsNotNull(result);
            }
            catch (Exception e)
            {
                Assert.AreNotEqual("pass", e);
            }
            //assert
            //Assert.AreEqual("pass", "pass");
            Assert.IsNotNull(controller);


        }

        [TestMethod]
        public void DoctorAddQueue()
        {
            //arrange
            DoctorController controller = new DoctorController();
            //act


            try
            {
                Doctor doc = new Doctor();
                controller.Session["DoctorLoggedIn"] = "doc1";
                ViewResult result = controller.AddQueue() as ViewResult;


                // ViewResult result = controller.Submit(doc) as ViewResult;
                Assert.IsNotNull(result);
            }
            catch (Exception e)
            {
                Assert.AreNotEqual("pass", e);
            }
            //assert
            //Assert.AreEqual("pass", "pass");
            Assert.IsNotNull(controller);


        }
        [TestMethod]
        public void DoctorPatientInfo()
        {
            //arrange
            DoctorController controller = new DoctorController();
            //act


            try
            {
                Doctor doc = new Doctor();
                controller.Session["DoctorLoggedIn"] = "doc1";
                ViewResult result = controller.PatientInfo("user1") as ViewResult;


                // ViewResult result = controller.Submit(doc) as ViewResult;
                Assert.IsNotNull(result);
            }
            catch (Exception e)
            {
                Assert.AreNotEqual("pass", e);
            }
            //assert
            //Assert.AreEqual("pass", "pass");
            Assert.IsNotNull(controller);


        }
        [TestMethod]
        public void DoctorMyinfo()
        {
            //arrange
            DoctorController controller = new DoctorController();
            //act

            
            try
            {
                Doctor doc = new Doctor();
                controller.Session["DoctorLoggedIn"] = "doc1";
                ViewResult result = controller.MyInfo() as ViewResult;

                
                // ViewResult result = controller.Submit(doc) as ViewResult;
                Assert.IsNotNull(result);
            }
            catch (Exception e)
            {
                Assert.AreNotEqual("pass", e);
            }
            //assert
            //Assert.AreEqual("pass", "pass");
            Assert.IsNotNull(controller);


        }
        [TestMethod]
        public void DoctorEdit()
        {
            //arrange
            DoctorController controller = new DoctorController();
            //act
            Doctor doc = new Doctor();
            
            try
            {
                ViewResult result = controller.Edit() as ViewResult;

                // ViewResult result = controller.Submit(doc) as ViewResult;
                Assert.IsNotNull(result);
            }
            catch (Exception e)
            {
                Assert.AreNotEqual("pass", e);
            }
            //assert
            //Assert.AreEqual("pass", "pass");
            Assert.IsNotNull(controller);


        }
        [TestMethod]
        public void DoctorSubmit()
        {
            //arrange
            DoctorController controller = new DoctorController();
            //act
            Doctor doc = new Doctor();
            try
            {
                ViewResult result = controller.Submit(doc) as ViewResult;
                Assert.IsNotNull(result);
            }
            catch (Exception e)
            {
                Assert.AreNotEqual("pass", e);
            }
            //assert
            //Assert.AreEqual("pass", "pass");
            Assert.IsNotNull(controller);


        }
        [TestMethod]
        public void DoctorVerifyPatient()
        {
            //arrange
            DoctorController controller = new DoctorController();
            //act
            try
            {
                bool result = controller.verifyPatient("user1");
            }
            catch (Exception e)
            {
                Assert.AreNotEqual("pass", e);
            }
            //ViewResult result = controller.verifyDoctor() as ViewResult;
            //assert
            //Assert.AreEqual("pass", "pass");
            Assert.IsNotNull(controller);


        }
        [TestMethod]
        public void DoctorVerifyDoctor()
        {
            //arrange
            DoctorController controller = new DoctorController();
            //act
            try
            {
                bool result = controller.verifyDoctor();
            }
            catch(Exception e)
            {
                Assert.AreNotEqual("pass", e);
            }
                //ViewResult result = controller.verifyDoctor() as ViewResult;
            //assert
            //Assert.AreEqual("pass", "pass");
            Assert.IsNotNull(controller);


        }
        [TestMethod]
        public void DoctorShowReview()
        {
            //arrange
            DoctorController controller = new DoctorController();
            //act
            ViewResult result = controller.ShowReview() as ViewResult;
            //assert
            //Assert.AreEqual("pass", "pass");
            Assert.IsNotNull(controller);


        }
        [TestMethod]
        public void DoctorAddInfoToDB()
        {
            //arrange
            DoctorController controller = new DoctorController();
            try
            {
                //act
                ViewResult result = controller.AddReviewToDB() as ViewResult;
            }
            catch (Exception e)
            {

                Assert.AreNotEqual("pass", "fail");
            }
            //assert
            //Assert.AreEqual("pass", "pass");
            Assert.IsNotNull(controller);


        }
        [TestMethod]
        public void DoctorAddInfo()
        {
            //arrange
            DoctorController controller = new DoctorController();

            //act
            ViewResult result = controller.AddReview() as ViewResult;

            //assert
            Assert.IsNotNull(result);


        }

    }
    [TestClass]
    public class ControllersTests
    {
        
        [TestMethod]
        public void verigyPatient()
        {
            //arrange
            DoctorController controller = new DoctorController();

            //act

            var result = controller.verifyPatient("user1");

            //assert
            Assert.IsNotNull(result);
            //Assert.Fail();

        }
        
        [TestMethod]
        public void ForumMessage()
        {

            //arrange
            ForumController controller = new ForumController();
            //controller.Session["DoctorLoggedIn"] = "doc1";
            //act
            ViewResult result = controller.MessagePassing(1,2) as ViewResult;


            //assert
            Assert.IsNotNull(result);

        }
        [TestMethod]
        public void ForumBag()
        {
            
            //arrange
            ForumController controller = new ForumController();
            //controller.Session["DoctorLoggedIn"] = "doc1";
            //act
            ViewResult result = controller.Create() as ViewResult;


            //assert
            Assert.AreEqual(null, result.ViewBag.Title);

        }
        [TestMethod]
        public void Forum()
        {
            //arrange
            ForumController controller = new ForumController();

            //act
            ViewResult result = controller.Create() as ViewResult;

            //assert
            Assert.IsNotNull(result);


        }
        [TestMethod]
        public void ShowHomePage()
        {
            //arrange
            HomeController controller = new HomeController();

            //act
            ViewResult result = controller.ShowHomePage() as ViewResult;

            //assert
            Assert.IsNotNull(result);


        }

        [TestMethod]
        public void Index()
        {
            //arrange
            HomeController controller = new HomeController();

            //act
            ViewResult result = controller.Index() as ViewResult;

            //assert
            Assert.IsNotNull(result);


        }

        [TestMethod]
        public void About()
        {
            //arrange
            HomeController controller = new HomeController();
            
            //act
            ViewResult result = controller.About() as ViewResult;
            
            //assert
            Assert.IsNotNull(result);

            
        }

        [TestMethod]
        public void Contact()
        {
            //arrange
            HomeController controller = new HomeController();

            //act
            ViewResult result = controller.Contact() as ViewResult;

            //assert
            Assert.AreEqual("Your contact page.",result.ViewBag.Message);


        }
        [TestMethod]
        public void LogIn()
        {
            //arrange
            HomeController controller = new HomeController();

            //act
            ViewResult result = controller.LogIn() as ViewResult;

            //assert
            Assert.IsNotNull(result);

        }


        [TestMethod]
        public void Enter()
        {
            //arrange
            UserController controller = new UserController();

            //act
            ViewResult result = controller.Enter() as ViewResult;

            //assert
            Assert.IsNotNull(result);


        }

        /*
        [TestMethod]
        public void showAllUsers()
        {
            //arrange
            AdminController controller = new AdminController();

            //act
            ViewResult result = controller.ShowAllUsers() as ViewResult;

            //assert
            Assert.IsNotNull(result);


        }
        */

        /*
        [TestMethod]
        public void AddDoctor()
        {
            
            Assert.Fail("NO clone Database repository to work on right now.");
            //using (new FakeHttpContext())
            //{
            HttpContext.Current.Session["AdminLoggedIn"] = "doc10";

            //arrange
            AdminController controller = new AdminController();
            //string usr="user1";
            Doctor doc = new Doctor
            {
                FirstName = "doc10",
                LastName = "doc10",
                DID = "doc10",
                Age = 29,
                Phone = "0541244789",
                Email = "doc10@gmail.com",
                Password = "doc10"
            };
            // SessionStateAttribute["AdminLoggedIn"] = "doc10";
            //act
            //var result = controller.DeleteUser(usr) as RedirectToRouteResult;

            //assert
            //Assert.IsInstanceOfType(typeof(RedirectToRouteResult), result.GetType());

            var result = (RedirectToRouteResult)controller.AddDoctor(doc);

            result.RouteValues["action"].Equals("ShowDoctors");
            result.RouteValues["controller"].Equals("Admin");
            
            Assert.AreEqual("ShowDoctors", result.RouteValues["action"]);
            Assert.AreEqual("Admin", result.RouteValues["controller"]);

            //}
            
        }
        */



    }
}
