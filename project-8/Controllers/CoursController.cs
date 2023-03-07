using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using project_8.Models;

namespace project_8.Controllers
{
    public class CourceViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Major { get; set; }
        public string Classification { get; set; }
    }
    public class CoursController : Controller
    {
        private project8Entities1 db = new project8Entities1();

        // GET: Cours
        public ActionResult Index()
        {
            var courses = db.Courses.Include(c => c.Classification).Include(c => c.Major);
            return View(courses.ToList());
        }

        // GET: Cours/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cours cours = db.Courses.Find(id);
            if (cours == null)
            {
                return HttpNotFound();
            }
            return View(cours);
        }

        // GET: Cours/Create
        public ActionResult Create(string search)
        {
            ViewBag.Classification_id = new SelectList(db.Classifications, "Id", "Classification_Name");
            ViewBag.Major_id = new SelectList(db.Majors, "Id", "Name");

            if (search == null)
            {
                var result = from sc in db.Courses
                             select new CourceViewModel
                             {
                                 Id = sc.Id,
                                 Name = sc.Name,
                                 Major = sc.Major.Name,
                                 Classification = sc.Classification.Classification_Name
                             };
                var dataaa = result.ToList();
                ViewBag.Cours = dataaa;

            }
            else
            {
                var result = from sc in db.Courses
                             where sc.Name.Contains(search)
                             select new CourceViewModel
                             {
                                 Id = sc.Id,
                                 Name = sc.Name,
                                 Major = sc.Major.Name,
                                 Classification = sc.Classification.Classification_Name
                             };
                var dataaa = result.ToList();
                ViewBag.Cours = dataaa;



            }
           


            var data = db.Courses.ToList();
            ViewBag.Courses = data;
            return View();
        }

        // POST: Cours/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Major_id,Classification_id")] Cours cours)
        {
            if (ModelState.IsValid)
            {
                db.Courses.Add(cours);
                db.SaveChanges();
                return RedirectToAction("Create");
            }

            ViewBag.Classification_id = new SelectList(db.Classifications, "Id", "Classification_Name", cours.Classification_id);
            ViewBag.Major_id = new SelectList(db.Majors, "Id", "Name", cours.Major_id);
            return View(cours);
        }

        // GET: Cours/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cours cours = db.Courses.Find(id);
            if (cours == null)
            {
                return HttpNotFound();
            }
            ViewBag.Classification_id = new SelectList(db.Classifications, "Id", "Classification_Name", cours.Classification_id);
            ViewBag.Major_id = new SelectList(db.Majors, "Id", "Name", cours.Major_id);
            return View(cours);
        }

        // POST: Cours/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Major_id,Classification_id")] Cours cours)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cours).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Create");
            }
            ViewBag.Classification_id = new SelectList(db.Classifications, "Id", "Classification_Name", cours.Classification_id);
            ViewBag.Major_id = new SelectList(db.Majors, "Id", "Name", cours.Major_id);
            return View(cours);
        }

        // GET: Cours/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cours cours = db.Courses.Find(id);
            if (cours == null)
            {
                return HttpNotFound();
            }
            return View(cours);
        }

        // POST: Cours/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cours cours = db.Courses.Find(id);
            db.Courses.Remove(cours);
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
