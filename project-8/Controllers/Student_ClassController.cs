using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Xml.Linq;
using Microsoft.AspNet.Identity;
using project_8.Models;


namespace project_8.Controllers
{

    public class StudentClassViewModel
    {
        public int Id { get; set; }
        public string CourseName { get; set; }
        public string Time { get; set; }
    }
    [Authorize]
    public class Student_ClassController : Controller
    {
        private project8Entities1 db = new project8Entities1();

        // GET: Student_Class
        public ActionResult Index()
        {
            var student_Class = db.Student_Class.Include(s => s.AspNetUser).Include(s => s.Class);
            return View(student_Class.ToList());
        }
        public ActionResult CreateAddAndDrop()
        {
            if (User.Identity.GetUserId() == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var userId = User.Identity.GetUserId();
            var attempet_user = db.AspNetUsers.SingleOrDefault(m => m.Id == userId)?.Accountant;
            if (attempet_user == false)
            {
                return RedirectToAction("Error");

            }
            var result2 = db.class_Accepted.SingleOrDefault(m => m.User_id == userId);
            if (result2 != null)
            {
                var already_submited = result2.Accepted_Studient;
                if (already_submited == false)
                {
                    return RedirectToAction("Error3");
                }
                // do something with already_submited
            }
            else
            {
                return RedirectToAction("Error3");
            }
            var test = db.Classes.Include(m => m.Cours).ToList();
            ViewBag.ddata = test;
            //string test1 = test.Cours.Name;
            //string test2 = Convert.ToString(test.Time);
            //string final = test1 + " " + test2;

            ViewBag.User_id = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.Class_id = new SelectList(db.Classes, "Id", "Id");



            var result = from sc in db.Student_Class
                         where sc.User_id == userId
                         select new StudentClassViewModel
                         {
                             Id = sc.Id,
                             Time = sc.Class.Time.ToString(),
                             CourseName = sc.Class.Cours.Name
                         };
            var dataaa = result.ToList();
            ViewBag.student = dataaa;

            var student_Class = db.Student_Class.Include(s => s.AspNetUser).Include(s => s.Class).ToList();
            ViewBag.student = dataaa;
            ViewBag.Hello = "Hello";
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAddAndDrop([Bind(Include = "Id,Class_id")] Student_Class student_Class)
        {
            if (ModelState.IsValid)
            {
                student_Class.User_id = User.Identity.GetUserId();
                db.Student_Class.Add(student_Class);
                db.SaveChanges();
                return RedirectToAction("CreateAddAndDrop");
            }

            ViewBag.User_id = new SelectList(db.AspNetUsers, "Id", "Email", student_Class.User_id);
            ViewBag.Class_id = new SelectList(db.Classes, "Id", "Id", student_Class.Class_id);
            return View(student_Class);
        }


        public ActionResult Error()
        {

            return View();
        }
        public ActionResult Errorr()
        {

            return View();
        }
        public ActionResult Error3()
        {

            return View();
        }
        // GET: Student_Class/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student_Class student_Class = db.Student_Class.Find(id);
            if (student_Class == null)
            {
                return HttpNotFound();
            }
            return View(student_Class);
        }

        // GET: Student_Class/Create
        public ActionResult Create()
        {
            if (User.Identity.GetUserId() == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var userId = User.Identity.GetUserId();
            var attempet_user = db.AspNetUsers.SingleOrDefault(m => m.Id == userId)?.Accountant;
            if (attempet_user == false)
            {
                return RedirectToAction("Error");

            }
            var result2 = db.class_Accepted.SingleOrDefault(m => m.User_id == userId);
            if (result2 != null)
            {
                var already_submited = result2.Accepted_Studient;
                if (already_submited == true)
                {
                    return RedirectToAction("Errorr");
                }
                // do something with already_submited
            }
            //var already_submited = db.class_Accepted.SingleOrDefault(m => m.User_id == userId).Accepted_Studient;
           

            var test = db.Classes.Include(m => m.Cours).ToList();
            ViewBag.ddata = test;
            //string test1 = test.Cours.Name;
            //string test2 = Convert.ToString(test.Time);
            //string final = test1 + " " + test2;

            ViewBag.User_id = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.Class_id = new SelectList(db.Classes, "Id", "Id");

            

            var result = from sc in db.Student_Class
                         where sc.User_id == userId
                         select new StudentClassViewModel
                         {
                             Id = sc.Id,
                             Time = sc.Class.Time.ToString(),
                             CourseName = sc.Class.Cours.Name
                         };
            var dataaa = result.ToList();
            ViewBag.student = dataaa;

            var student_Class = db.Student_Class.Include(s => s.AspNetUser).Include(s => s.Class).ToList();
            ViewBag.student = dataaa;
            ViewBag.Hello = "Hello";
            return View();
        }

       
        public ActionResult Confirm()
        {
            string userId = User.Identity.GetUserId();
            var courses_count = db.Student_Class.Where(m => m.User_id == userId).Count();

            var result2 = from ss in db.AspNetUsers
                          where ss.Id == userId 
                          select new
                          {
                              price = ss.Major.Price
                          };

            var price = result2.ToList();
            var price2 = price[0].price;

            var total_price = courses_count * 3 * price2;


            var all_classes = db.class_Accepted.ToList();
            bool alreadyThere = false;
            foreach (var item in all_classes)
            {
                if(item.User_id== User.Identity.GetUserId())
                {
                    alreadyThere = true;
                }
            }

            if (alreadyThere == true)
            {
                var class2 = db.class_Accepted.SingleOrDefault(m=>m.User_id == userId);
                class2.charge = total_price;
                class2.Accepted_Studient = true;
                //class2.Accepted_Studient = true;
                db.SaveChanges();
                return RedirectToAction("Create");
            }

            var newOne = new class_Accepted { User_id = User.Identity.GetUserId(), User_name = User.Identity.GetUserName(), charge = total_price, Accepted_Studient = true, Accepted_Acountant = false };

            db.class_Accepted.Add(newOne);
            db.SaveChanges();
            return RedirectToAction("Create");
        }

        // POST: Student_Class/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Class_id,User_id")] Student_Class student_Class)
        {
            if (ModelState.IsValid)
            {
                string userId = User.Identity.GetUserId();
                student_Class.User_id = userId;
                db.Student_Class.Add(student_Class);
                db.SaveChanges();
                return RedirectToAction("Create");
            }

            ViewBag.User_id = new SelectList(db.AspNetUsers, "Id", "Email", student_Class.User_id);
            ViewBag.Class_id = new SelectList(db.Classes, "Id", "Id", student_Class.Class_id);
            return View(student_Class);
        }

        // GET: Student_Class/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student_Class student_Class = db.Student_Class.Find(id);
            if (student_Class == null)
            {
                return HttpNotFound();
            }
            ViewBag.User_id = new SelectList(db.AspNetUsers, "Id", "Email", student_Class.User_id);
            ViewBag.Class_id = new SelectList(db.Classes, "Id", "Id", student_Class.Class_id);
            return View(student_Class);
        }

        // POST: Student_Class/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Class_id,User_id")] Student_Class student_Class)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student_Class).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.User_id = new SelectList(db.AspNetUsers, "Id", "Email", student_Class.User_id);
            ViewBag.Class_id = new SelectList(db.Classes, "Id", "Id", student_Class.Class_id);
            return View(student_Class);
        }

        // GET: Student_Class/Delete/5
        public ActionResult Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Student_Class student_Class = db.Student_Class.Find(id);
                if (student_Class == null)
                {
                    return HttpNotFound();
                }
                return View(student_Class);
            }
            catch (Exception ex)
            {

                return RedirectToAction("Error");
            }
           
        }

        // POST: Student_Class/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Student_Class student_Class = db.Student_Class.Find(id);
            db.Student_Class.Remove(student_Class);
            db.SaveChanges();
            return RedirectToAction("Create");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
