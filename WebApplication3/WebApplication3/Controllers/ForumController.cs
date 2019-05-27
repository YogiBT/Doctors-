using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Models;
using WebApplication3.ModelV;
using WebApplication3.DAL;
using PagedList;
using System.Net.Mail;
using System.Net.Http;
using SendGrid.Helpers.Mail;
using SendGrid;
using System.Net;

namespace WebApplication3.Controllers
{
    public class ForumController : Controller
    {
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (User != null)
            {
                var context = new UserDAL();
                /*var username = User.Identity.Name;*/
                var username = (string)Session["UserLoggedIn"];

                if (!string.IsNullOrEmpty(username))
                {
                    var user = context.Users.SingleOrDefault(u => u.FirstName == username);
                    string fullName = string.Concat(new string[] { user.FirstName, " ", user.LastName });
                    ViewData.Add("FullName", fullName);
                }
            }
            base.OnActionExecuted(filterContext);
        }

       


        public ActionResult MessagePassing(int? Id, int? page)
        {
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            MessageReplyViewModel vm = new MessageReplyViewModel();
            ReplyDAL replyDAL = new ReplyDAL();
            MessageDAL queDAL = new MessageDAL();
            var count = queDAL.MessagesDal.Count();
            decimal totalPages = count / (decimal)pageSize;
            ViewBag.TotalPages = Math.Ceiling(totalPages);
            vm.Messages = queDAL.MessagesDal
                            .OrderBy(x => x.DatePosted).ToPagedList(pageNumber, pageSize);
            ViewBag.MessagesInOnePage = vm.Messages;
            ViewBag.PageNumber = pageNumber;

            if (Id != null)
            {

                var replies = replyDAL.Replies.Where(x => x.MessageId == Id.Value).OrderByDescending(x => x.ReplyDateTime);
                if (replies != null)
                {
                    foreach (var rep in replies)
                    {
                        MessageReplyViewModel.MessageReply reply = new MessageReplyViewModel.MessageReply();
                        reply.MessageId = rep.MessageId;
                        reply.Id = rep.Id;
                        reply.ReplyMessage = rep.ReplyMessage;
                        reply.ReplyDateTime = rep.ReplyDateTime;
                        reply.MessageDetails = vm.Messages.Where(x => x.Id == rep.MessageId).Select(s => s.MessageToPost).FirstOrDefault();
                        reply.ReplyFrom = rep.ReplyFrom;
                        vm.Replies.Add(reply);
                    }

                }
                else
                {
                    vm.Replies.Add(null);
                }

                ViewBag.MessageId = Id.Value;
            }
            return View(vm);
        }

        public ActionResult Create()
        {
            MessageReplyViewModel vm = new MessageReplyViewModel();

            return View(vm);
        }

        [HttpPost]
        public ActionResult PostMessage(MessageReplyViewModel vm)
        {
            Console.WriteLine("Hello i am here ---------------------------------------\n");
            UserDAL usrDAL = new UserDAL();
            MessageDAL msgDAL = new MessageDAL();
            /*var username = User.Identity.Name;*/
            var username = (string)Session["UserLoggedIn"];
            string fullName = "";
            int msgid = 0;
            if (!string.IsNullOrEmpty(username))
            {
                var user = usrDAL.Users.SingleOrDefault(u => u.FirstName == username);
                fullName = string.Concat(new string[] { user.FirstName, " ", user.LastName });
            }
            Message messagetoPost = new Message();
            if (vm.Message.Subject != string.Empty && vm.Message.MessageToPost != string.Empty)
            {
                messagetoPost.DatePosted = DateTime.Now;
                messagetoPost.Subject = vm.Message.Subject;
                messagetoPost.MessageToPost = vm.Message.MessageToPost;
                messagetoPost.From = fullName;

                msgDAL.MessagesDal.Add(messagetoPost);
                msgDAL.SaveChanges();
                msgid = messagetoPost.Id;
            }

            return RedirectToAction("MessagePassing", "Forum", new { Id = msgid });
        }
        [HttpPost]
        /*[Authorize]*/
        public ActionResult ReplyMessage(MessageReplyViewModel vm, int messageId)
        {
            System.Diagnostics.Debug.WriteLine("Hello i am here ----%d-----------------------------------\n",messageId);

            /*var username = User.Identity.Name;*/
            var username = (string)Session["UserLoggedIn"];
            UserDAL usrDAL = new UserDAL();
            ReplyDAL replyDAL = new ReplyDAL();
            MessageDAL msgDAL = new MessageDAL();
            string fullName = "";
            if (!string.IsNullOrEmpty(username))
            {
                var user = usrDAL.Users.SingleOrDefault(u => u.FirstName == username);
                fullName = string.Concat(new string[] { user.FirstName, " ", user.LastName });
            }
            if (vm.Reply.ReplyMessage != null)
            {
                Reply _reply = new Reply();
                _reply.ReplyDateTime = DateTime.Now;
                /*_reply.Id = messageId;*/
                _reply.MessageId = messageId;
                _reply.ReplyFrom = fullName;
                _reply.ReplyMessage = vm.Reply.ReplyMessage;
                replyDAL.Replies.Add(_reply);
                replyDAL.SaveChanges();
            }
            //reply to the message owner          - using email template
            /*
            var messageOwner = msgDAL.MessagesDal.Where(x => x.Id == messageId).Select(s => s.From).FirstOrDefault();
            var users = from user in usrDAL.Users
                        orderby user.FirstName
                        select new
                        {
                            FullName = user.FirstName + " " + user.LastName,
                            UserEmail = user.Email
                        };

            var uemail = users.Where(x => x.FullName == messageOwner).Select(s => s.UserEmail).FirstOrDefault();
            SendGridMessage replyMessage = new SendGridMessage();
            replyMessage.From = new EmailAddress(username);
            replyMessage.Subject = "Reply for your message :" + msgDAL.MessagesDal.Where(i => i.Id == messageId).Select(s => s.Subject).FirstOrDefault();
            replyMessage.PlainTextContent = vm.Reply.ReplyMessage;
            


            replyMessage.AddTo(uemail);
            */
            /*
            var credentials = new NetworkCredential("Team8_1337", "Team81337");
            CredentialCache myCache = new CredentialCache();
            myCache.Add(new Uri("www.contoso.com"), "Basic", myCred);
            var transportweb = new WebRequest(credentials);
            transportweb.DeliverAsync(replyMessage);*/
            return RedirectToAction("MessagePassing", "Forum", new { Id = messageId });

        }
        public ActionResult Test() { return View(); }
    }
}

