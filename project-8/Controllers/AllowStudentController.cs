using project_8.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Mail;
using System.Security.Permissions;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;

namespace project_8.Controllers
{
    public class StudientTest
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string ID_number { get; set; }

        public string img { get; set; }

        public string ID_img { get; set; }

        public string certificate_img { get; set; }

        public string AVG { get; set;}

        public string major { get; set; }
    }
    public class AllowStudentController : Controller
    {
        private project8Entities1 db = new project8Entities1();
        // GET: AllowEnrollment
        public ActionResult Index(string search)
        {
            if (search == null)
            {
                var data = db.AspNetUsers.Where(m => m.Acceptance == false).Include(m => m.Major).ToList();
                var result2 = db.AspNetUsers
                  .Where(m => m.Acceptance == false) // <-- add where clause
                  .Select(ss => new StudientTest
                  {
                      Id = ss.Id,
                      UserName = ss.UserName,
                      PhoneNumber = ss.PhoneNumber,
                      ID_number = ss.ID_number.ToString(),
                      img = ss.img,
                      ID_img = ss.ID_img.ToString(),
                      certificate_img = ss.certificate_img,
                      AVG = ss.AVG.ToString(),
                      major = ss.Major.Name,
                  })
                  .ToList();
                var dataaa = result2;
                ViewBag.data = dataaa;
                return View();
            }
            else
            {
                var data = db.AspNetUsers.Where(m => m.Acceptance == false).Include(m => m.Major).ToList();
                var result2 = db.AspNetUsers
                  .Where(m => m.Acceptance == false)  // <-- add where clause
                  .Where(m => m.UserName.Contains(search))
                  .Select(ss => new StudientTest
                  {
                      Id = ss.Id,
                      UserName = ss.UserName,
                      PhoneNumber = ss.PhoneNumber,
                      ID_number = ss.ID_number.ToString(),
                      img = ss.img,
                      ID_img = ss.ID_img.ToString(),
                      certificate_img = ss.certificate_img,
                      AVG = ss.AVG.ToString(),
                      major = ss.Major.Name,
                  })
                  .ToList();
                var dataaa = result2;
                ViewBag.data = dataaa;
                return View();


            }

        }


        public ActionResult Paid(string id)
        {
            var class2 = db.AspNetUsers.SingleOrDefault(m => m.Id == id);
            class2.Acceptance = true;
            db.SaveChanges();

            MailMessage mail = new MailMessage();
            mail.To.Add(class2.Email);
            mail.From = new MailAddress("ayahizzatalzyout@gmail.com");
            mail.Subject = "Accept";

            mail.Body = "Wellcom";
            mail.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient();
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Host = "smtp.gmail.com";
            smtp.Credentials = new System.Net.NetworkCredential("ayahizzatalzyout", "bpvxecfcoyngwhas");
            smtp.Send(mail);


            //int pay = payment;
            //var newOne = new student_payments { User_id = id, User_name = userName, payment = 250, description = "Semester fees" };

            //db.student_payments.Add(newOne);
            //db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}