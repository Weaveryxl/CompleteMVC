using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CompleteMVC.DAL;
using CompleteMVC.Infrastructure;
using CompleteMVC.Models;
using System.Security.Principal;

namespace CompleteMVC.Controllers
{
    [SchoolAuthorize(Roles = "Student,Admin")]
    public class StudentsController : Controller
    {
        private SchoolContext db = new SchoolContext();

        // GET: Students
        public ActionResult Index()
        {
            var students = db.User.ToList();
            return View(students);
        }

        // GET: Students/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User student = db.User.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        //[ActionName("Personal Profile")]
        public ActionResult Personal_profile()
        {
            var user = this.HttpContext.User; // This is the KEY!!!
            if (user == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //var claimIdentity = user.Identity as ClaimsIdentity;
            //var userIdClaim = claimIdentity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            //if (userIdClaim == null)
            //{
            //    return HttpNotFound();
            //}
            //var userID = userIdClaim.Value;

            var student = db.User.Where(c => c.Username == user.Identity.Name).FirstOrDefault() as Student;
            //Customer customer = db.Customers.Find(userID); //I don't know how to do this
            return View(student);
        }

        // GET: Students/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserId,Username,Password,IsActive,CreateDate,LastName,FirstMidName,Age,StudentId,EnrollmentDate")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.User.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(student);
        }

        // GET: Students/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserId,Username,Password,IsActive,CreateDate,LastName,FirstMidName,Age,StudentId,EnrollmentDate")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(student);
        }

        // GET: Students/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = db.Students.Find(id);
            db.User.Remove(student);
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
