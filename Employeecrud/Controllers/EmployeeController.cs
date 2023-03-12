using Antlr.Runtime;
using Employeecrud.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Employeecrud.Controllers
{
    public class EmployeeController : Controller
    {
        praticesEntities db = new praticesEntities(); //connectionstring
        // GET: Employee
        public ActionResult Index()
        {
            var data = db.employeedatas.ToList(); //get data in the form of list
            return View(data);
        }
        [HttpGet]//get view from server
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost] // insert employee record
        public ActionResult Create(employeedata emp)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    employeedata e = new employeedata();
                    e.EmployeeName = emp.EmployeeName;
                    e.EmployeeAddress= emp.EmployeeAddress;
                    e.Employeemail = emp.Employeemail;
                    e.EmployeePhoneno = emp.EmployeePhoneno;
                    e.EmployeeQualification = emp.EmployeeQualification;
                    var result = db.employeedatas.Add(e);
                    db.SaveChanges();
                    if(result!= null)
                    {
                        ViewBag.Message = "Employee data created successfully done..";
                        ModelState.Clear();
                    }
                    else
                    {
                        ViewBag.Message = "Failed to create employee deatils..";
                    }
                }
            } 
            catch(Exception ex)
            {
                ViewBag.Message = ex.Message;
            }
            return View();
        }
        [HttpGet]//getting data from server
        public ActionResult Edit(int id)
        {
            employeedata ed = db.employeedatas.Single(X=>X.Eid== id);
            return View(ed);
        }
        [HttpPost]//editing data
        public ActionResult Edit(employeedata edit)
        {
            db.Entry(edit).State = EntityState.Modified;
            int a = db.SaveChanges();
            if (a > 0)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Message = "Failed to edit data";
            }
            return View();
        }
        [HttpGet]//getting details of emp
        public ActionResult Details(int id)
        {
            employeedata em = db.employeedatas.Single(x=> x.Eid == id);
            return View(em);
        }
        [HttpGet]//deleting employee details
        public ActionResult Delete(int id)
        {
            employeedata em = db.employeedatas.Single(x => x.Eid == id);
            db.employeedatas.Remove(em);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}