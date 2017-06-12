﻿using StudentLeaf.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentLeaf.Controllers
{
    public class StudentController : Controller
    {
        private ApplicationDbContext _context;

        public StudentController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: Student
        public ActionResult Index()
        {
           /* var students = _context.Student
                .Include(t => t.ActiveLessons)
                .Include(t => t.History)
                .ToList();
            */     
            return View();
        }
        public JsonResult List()
        {
            var students = _context.Student.Include(n => n.ActiveLessons).Include(n => n.History).ToList();
            return Json(students, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Edit(int id)
        {
            var student = _context.Student
                .Include(t => t.ActiveLessons)
                .SingleOrDefault(t => t.Id == id);

            if(!ModelState.IsValid)
            {
                return HttpNotFound();
            }

            return View(student);
        }

        public ActionResult Delete(int id)
        {
            var student = _context.Student
                .Include(n => n.ActiveLessons)
                .SingleOrDefault(n => n.Id == id);

            if (student.ActiveLessons.Count > 1)
                return HttpNotFound("This student has more than one Active Lessons");

            _context.Student.Remove(student);

            _context.SaveChanges();

            return View("Index");
        }
        //Selecting the info for one individual lesson plan
        /*public ActionResult ActiveLesson(int id, string lessonType)
        {
            var active = _context.Student
                .Include(n => n.ActiveLessons)
                .SingleOrDefault(n => n.Id == id)
                .ActiveLessons
                .Where(n => n.LessonPlan == lessonType);

            return View("Index", active);
        }
        */
        public ActionResult History(int id)
        {
            var history = _context.Student
                .SingleOrDefault(n => n.Id == id)
                .History.ToList();
                     
            return View(history);
                
        }
     }

}