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
    public class tableAproveController : Controller
    {
        private project8Entities1 db = new project8Entities1();

        // GET: tableAprove
        public ActionResult Index(string search)
        {
            if (search == null)
            {

                var data = db.class_Accepted.Where(m => m.Accepted_Acountant == false).ToList();
                ViewBag.data = data;
                return View();
            }
            else
            {

                var data = db.class_Accepted.Where(m => m.Accepted_Acountant == false).Where(m => m.User_name.Contains(search)).ToList();
                ViewBag.data = data;
                return View();
            }
        }

        public ActionResult Paid(string id, int payment, string userName)
        {
            var class2 = db.class_Accepted.SingleOrDefault(m => m.User_id == id);
            class2.Accepted_Acountant = true;
            db.SaveChanges();
            int pay = payment;
            var newOne = new student_payments { User_id = id, User_name = userName, payment = payment, description = "Courses registration fees" };

            db.student_payments.Add(newOne);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}