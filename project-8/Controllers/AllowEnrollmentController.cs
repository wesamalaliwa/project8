using Microsoft.AspNet.Identity;
using project_8.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace project_8.Controllers
{
    [Authorize(Roles = "Accountant")]
    public class AllowEnrollmentController : Controller
    {
        private project8Entities1 db = new project8Entities1();
        // GET: AllowEnrollment
        public ActionResult Index(string search)
        {
            if (search == null)
            {
                var data = db.AspNetUsers.Where(m => m.Acceptance == true && m.Accountant == false).ToList();
                ViewBag.data = data;
                return View();
            }
            else
            {
                var data = db.AspNetUsers.Where(m => m.Acceptance == true && m.Accountant == false).Where(m => m.UserName.Contains(search)).ToList();
                ViewBag.data = data;
                return View();
            }
        }

        public ActionResult Paid(string id, string userName)
        {
            var class2 = db.AspNetUsers.SingleOrDefault(m => m.Id == id);
            class2.Accountant = true;
            db.SaveChanges();
            //int pay = payment;
            var newOne = new student_payments { User_id = id, User_name = userName, payment = 250, description = "Semester fees" };

            db.student_payments.Add(newOne);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}