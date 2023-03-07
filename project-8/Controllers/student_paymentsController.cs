using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using project_8.Models;

namespace project_8.Controllers
{
    public class student_paymentsController : Controller
    {
        private project8Entities1 db = new project8Entities1();

        // GET: student_payments
        public ActionResult Index()
        {
            var idd = User.Identity.GetUserId();
            var student_payments = db.student_payments.Include(s => s.AspNetUser).Where(m=>m.User_id==idd);
            return View(student_payments.ToList());
        }

        // GET: student_payments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            student_payments student_payments = db.student_payments.Find(id);
            if (student_payments == null)
            {
                return HttpNotFound();
            }
            return View(student_payments);
        }

        // GET: student_payments/Create
        public ActionResult Create()
        {
            ViewBag.User_id = new SelectList(db.AspNetUsers, "Id", "Email");
            return View();
        }

        // POST: student_payments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,User_id,User_name,payment,description")] student_payments student_payments)
        {
            if (ModelState.IsValid)
            {
                db.student_payments.Add(student_payments);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.User_id = new SelectList(db.AspNetUsers, "Id", "Email", student_payments.User_id);
            return View(student_payments);
        }

        // GET: student_payments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            student_payments student_payments = db.student_payments.Find(id);
            if (student_payments == null)
            {
                return HttpNotFound();
            }
            ViewBag.User_id = new SelectList(db.AspNetUsers, "Id", "Email", student_payments.User_id);
            return View(student_payments);
        }

        // POST: student_payments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,User_id,User_name,payment,description")] student_payments student_payments)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student_payments).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.User_id = new SelectList(db.AspNetUsers, "Id", "Email", student_payments.User_id);
            return View(student_payments);
        }

        // GET: student_payments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            student_payments student_payments = db.student_payments.Find(id);
            if (student_payments == null)
            {
                return HttpNotFound();
            }
            return View(student_payments);
        }

        // POST: student_payments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            student_payments student_payments = db.student_payments.Find(id);
            db.student_payments.Remove(student_payments);
            db.SaveChanges();
            return RedirectToAction("Index");
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
