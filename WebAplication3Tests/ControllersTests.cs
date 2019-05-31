using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
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
    public class ControllersTests
    {
        /*
        [TestMethod]
        public void verigyPatient()
        {
            //arrange
            DoctorController controller = new DoctorController();

            //act

            var result = controller.verifyPatient("user1");

            //assert
            //Assert.IsNotNull(result);
            Assert.Fail();

        }
        */
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
