using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using project_8.Models;

namespace project_8.Controllers
{
    
    public class MajorsController : Controller
    {

        private project8Entities1 db = new project8Entities1();

        // GET: Majors
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var majors = db.Majors.Include(m => m.Faculity);
            return View(majors.ToList());
        }

        public ActionResult AllMajors()
        {
            var majors = db.Majors.Include(m => m.Faculity);
            var data = db.Majors.ToList();
            ViewBag.Majors = data;
            return View(majors.ToList());
        }



        public ActionResult Majors(int id)
        {

            var majors = db.Majors.Where(m => m.Faculity_id == id).Include(m => m.Faculity);
            return View(majors.ToList());


        }

        // GET: Majors/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Major major = db.Majors.Find(id);
            if (major == null)
            {
                return HttpNotFound();
            }
            return View(major);
        }

        // GET: Majors/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create(string search)
        {
            if (search == null)
            {
                var data = db.Majors.ToList();
                ViewBag.Major = data;
                

            }
            else
            {
                var data = db.Majors.Where(x => x.Name.Contains(search)).ToList();
                ViewBag.Major = data;

            }


            ViewBag.Faculity_id = new SelectList(db.Faculities, "Id", "Name");
            return View();
        }

        // POST: Majors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Faculity_id,img,Price,Description")] Major major, HttpPostedFileBase img)
        {
            if (ModelState.IsValid)
            {
                if (img != null)
                {
                    if (!img.ContentType.ToLower().StartsWith("image/"))
                    {
                        ModelState.AddModelError("", "file uploaded is not an image");
                        return View();
                    }
                    string folderPath = Server.MapPath("~/Content/Images");
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }
                    string fileName = Path.GetFileName(img.FileName);
                    string path = Path.Combine(folderPath, fileName);
                    img.SaveAs(path);
                    major.img = "../Content/Images/" + fileName;
                }
                else
                {
                    ModelState.AddModelError("", "Please upload an image.");
                    return View();
                }
                db.Majors.Add(major);
                db.SaveChanges();
                return RedirectToAction("Create");
            }

            ViewBag.Faculity_id = new SelectList(db.Faculities, "Id", "Name", major.Faculity_id);
            return View(major);
        }

        // GET: Majors/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Major major = db.Majors.Find(id);
            if (major == null)
            {
                return HttpNotFound();
            }
            ViewBag.Faculity_id = new SelectList(db.Faculities, "Id", "Name", major.Faculity_id);
            return View(major);
        }

        // POST: Majors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Faculity_id,img,Price,Description")] Major major, HttpPostedFileBase img)
        {
            if (ModelState.IsValid)
            {
                if (img != null)
                {
                    if (!img.ContentType.ToLower().StartsWith("image/"))
                    {
                        ModelState.AddModelError("", "file uploaded is not an image");
                        return View();
                    }
                    string folderPath = Server.MapPath("~/Content/Images");
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }
                    string fileName = Path.GetFileName(img.FileName);
                    string path = Path.Combine(folderPath, fileName);
                    img.SaveAs(path);
                    major.img = "../Content/Images/" + fileName;
                }
              
                
                db.Entry(major).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Create");
            }
            ViewBag.Faculity_id = new SelectList(db.Faculities, "Id", "Name", major.Faculity_id);
            return View(major);
        }

        // GET: Majors/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Major major = db.Majors.Find(id);
            if (major == null)
            {
                return HttpNotFound();
            }
            return View(major);
        }

        // POST: Majors/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Major major = db.Majors.Find(id);
            db.Majors.Remove(major);
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
