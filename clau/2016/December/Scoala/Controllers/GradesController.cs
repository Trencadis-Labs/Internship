using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Scoala.Models;
using System.Text;

namespace Scoala.Controllers
{
    public class GradesController : Controller
    {
        
        // GET: Grades
        public ActionResult Index()
        {
            ScoalaDB db = new ScoalaDB();
            var viewModel = (from g in db.Grades
                             join s in db.Students on g.id_student equals s.id_student
                             join c in db.Courses on g.id_cours equals c.id_cours
                             select new
                             {
                                 id_grade = g.id_grade,
                                 studentName = s.name + " " + s.surname,
                                 coursName = c.name,
                                 grade = g.grade,
                                 date_of_registration = g.date_of_registration
                             }).ToList()
                                   .Select(x => new GradeCourseStudent
                                   {
                                       id_grade = x.id_grade,
                                       studentName = x.studentName,
                                       coursName = x.coursName,
                                       grade = x.grade,
                                       date_of_registration = x.date_of_registration
                                   });

            return View(viewModel);
        }

        // GET: Grades/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ScoalaDB db = new ScoalaDB();
            var viewModel = (from g in db.Grades
                             join s in db.Students on g.id_student equals s.id_student
                             join c in db.Courses on g.id_cours equals c.id_cours
                             where g.id_grade == id
                             select new
                             {
                                 id_grade = g.id_grade,
                                 studentName =  s.name +" "+ s.surname,
                                 coursName = c.name,
                                 grade = g.grade,
                                 date_of_registration = g.date_of_registration
                             }).ToList()
                             .Select(x => new GradeCourseStudent
                             {
                                 id_grade = x.id_grade,
                                 studentName = x.studentName,
                                 coursName = x.coursName,
                                 grade = x.grade,
                                 date_of_registration = x.date_of_registration
                             }).FirstOrDefault();
                         

            if (viewModel == null)
            {
                return HttpNotFound();
            }

            return View(viewModel);
        }

        // GET: Grades/Create
        public ActionResult Create()
        {
            ScoalaDB db = new ScoalaDB();
            ViewBag.students = db.Students.ToList();
            ViewBag.courses = db.Courses.ToList();
            return View();
        }

        // POST: Grades/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_grade,id_student,id_cours,grade,date_of_registration")] Grade gradeCreate)
        {
            if (ModelState.IsValid)
            {
                ScoalaDB db = new ScoalaDB();
                gradeCreate.date_of_registration = DateTime.Now;
                db.Grades.Add(gradeCreate);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(gradeCreate);
        }

        // GET: Grades/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ScoalaDB db = new ScoalaDB();
            Grade gradeEdit = db.Grades.Find(id);
            if (gradeEdit == null)
            {
                return HttpNotFound();
            }

            ViewBag.students = db.Students.ToList();
            ViewBag.courses = db.Courses.ToList();

            return View(gradeEdit);
        }

        // POST: Grades/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_grade,id_student,id_cours,grade,date_of_registration")] Grade gradeEdit)
        {
            if (ModelState.IsValid)
            {
                ScoalaDB db = new ScoalaDB();
                db.Entry(gradeEdit).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(gradeEdit);
        }

        // GET: Grades/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ScoalaDB db = new ScoalaDB();
            Grade gradeDelete = db.Grades.Find(id);
            if (gradeDelete == null)
            {
                return HttpNotFound();
            }
            return View(gradeDelete);
        }

        // POST: Grades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ScoalaDB db = new ScoalaDB();
            Grade gradeDelete = db.Grades.Find(id);
            db.Grades.Remove(gradeDelete);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ScoalaDB db = new ScoalaDB();
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
