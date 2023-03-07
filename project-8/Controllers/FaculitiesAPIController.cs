using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using project_8.Models;

namespace project_8.Controllers
{
    public class FaculitiesAPIController : ApiController
    {
        private project8Entities1 db = new project8Entities1();

        // GET: api/FaculitiesAPI
        public IQueryable<Faculity> GetFaculities()
        {
            return db.Faculities;
        }

        // GET: api/FaculitiesAPI/5
        [ResponseType(typeof(Faculity))]
        public IHttpActionResult GetFaculity(int id)
        {
            Faculity faculity = db.Faculities.Find(id);
            if (faculity == null)
            {
                return NotFound();
            }

            return Ok(faculity);
        }

        // PUT: api/FaculitiesAPI/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutFaculity(int id, Faculity faculity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != faculity.Id)
            {
                return BadRequest();
            }

            db.Entry(faculity).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FaculityExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/FaculitiesAPI
        [ResponseType(typeof(Faculity))]
        public IHttpActionResult PostFaculity(Faculity faculity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Faculities.Add(faculity);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = faculity.Id }, faculity);
        }

        // DELETE: api/FaculitiesAPI/5
        [ResponseType(typeof(Faculity))]
        public IHttpActionResult DeleteFaculity(int id)
        {
            Faculity faculity = db.Faculities.Find(id);
            if (faculity == null)
            {
                return NotFound();
            }

            db.Faculities.Remove(faculity);
            db.SaveChanges();

            return Ok(faculity);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FaculityExists(int id)
        {
            return db.Faculities.Count(e => e.Id == id) > 0;
        }
    }
}