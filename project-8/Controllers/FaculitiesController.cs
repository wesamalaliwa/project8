using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using project_8.Models;


namespace project_8.Controllers
{
    [Authorize(Roles = "Admin")] 
    public class FaculitiesController : Controller
    {
        private project8Entities1 db = new project8Entities1();

        // GET: Faculities
        public ActionResult Index()
        {
          return View(db.Faculities.ToList());
        }

        // GET: Faculities/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Faculity faculity = db.Faculities.Find(id);
            if (faculity == null)
            {
                return HttpNotFound();
            }
            return View(faculity);
        }

        // GET: Faculities/Create
        public ActionResult Create(string search)
        {
            
            if (search == null)
            {
                var data = db.Faculities.ToList();
                ViewBag.Faculity = data;
                return View();

            }
            else
            {
                var data = db.Faculities.Where(x => x.Name.Contains(search)).ToList();
                ViewBag.Faculity = data;
                return View();


              
            }
            //return View();

        }

        // POST: Faculities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,description,img")] Faculity faculity, HttpPostedFileBase img)
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
                    faculity.img = "../Content/Images/" + fileName;
                }
                else
                {
                    ModelState.AddModelError("", "Please upload an image.");
                    return View();
                }
                db.Faculities.Add(faculity);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(faculity);
        }

        // GET: Faculities/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Faculity faculity = db.Faculities.Find(id);
            if (faculity == null)
            {
                return HttpNotFound();
            }
            return View(faculity);
        }

        // POST: Faculities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,description,img")] Faculity faculity)
        {
            if (ModelState.IsValid)
            {
                db.Entry(faculity).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(faculity);
        }

        // GET: Faculities/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Faculity faculity = db.Faculities.Find(id);
            if (faculity == null)
            {
                return HttpNotFound();
            }
            return View(faculity);
        }

        // POST: Faculities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Faculity faculity = db.Faculities.Find(id);
                db.Faculities.Remove(faculity);
                db.SaveChanges();
                
            } 
            catch (SqlException ex)
            {
                if (ex.Number == 547)
                {
                    
                    return RedirectToAction("Error");
                }
               
            }
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
