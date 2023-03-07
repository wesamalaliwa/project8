using project_8.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace project_8.Controllers
{
    public class HomeController : Controller
    {
        private project8Entities1 db = new project8Entities1();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Search(string search)
        {
            ViewBag.Message = "This Major Is Not Found";
            ViewBag.Search = db.Majors.Where(p => p.Name.Contains(search)).Count();
            return View("Index", db.Majors.Where(p => p.Name.Contains(search)).ToList());

        }

        public ActionResult SatisfiedStudents()
        {                      
            var count = db.AspNetUsers.Where(s => s.Acceptance == true).Count();
            return Content(count.ToString());
        }

        public ActionResult Majors()
        {
            var count = db.Majors.Count();
            return Content(count.ToString());
        }
        public ActionResult Course()
        {
            var count = db.Courses.Count();
            return Content(count.ToString());
        }
        public ActionResult Faculty()
        {
            var count = db.Faculities.Count();
            return Content(count.ToString());
        }




        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        [HttpPost]
        public ActionResult SendEmail(string Name, string Email, string Subject, string Message)
        {
            try
            {
                // Create a MailMessage object with the sender, recipient, subject, and body
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(Email);
                mail.To.Add("ayahizzatalzyout@gmail.com");
                mail.Subject = Subject;
                mail.Body = string.Format("Name: {0}\n\nEmail: {1}\n\nMessage: {2}", Name, Email, Message);

                // Create a SmtpClient object to send the message
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
                smtpClient.Port = 587; // Use the appropriate port number for your SMTP server
                smtpClient.Credentials = new System.Net.NetworkCredential("ayahizzatalzyout@gmail.com", "bpvxecfcoyngwhas");
                smtpClient.EnableSsl = true;

                // Send the message
                smtpClient.Send(mail);

               
                // Redirect the user to a "Thank you" page
                return View("Contact");
            }
            catch (Exception ex)
            {
                // Handle any errors that occur
                ViewBag.Error = "There was an error sending your message: " + ex.Message;
                return View("Contact");
            }
        }


        public ActionResult Courses()
        {

            return View();
        }

        public ActionResult Events()
        {

            return View();
        }

        public ActionResult Blogs()
        {

            return View();
        }
        public ActionResult blogSingle()
        {

            return View();
        }
        public ActionResult error()
        {
            return View();
        }
    }
}