using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TrashCollector.Models;

namespace TrashCollector.Controllers
{
    public class PickUpsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PickUps
        public ActionResult Index()
        {
            Employee employee = db.Employees.Where(e => e.UserName == User.Identity.Name).Single();
            var test = db.PickUps.Select(p => p.PickUpId).Distinct().ToList();
            var pickUps = db.Customers.Include(p=>p.Address).Include(p=>p.PickUps).Where(p => test.Contains(p.PickId)).ToList();
            var results = pickUps.Where(p => p.Address.Zipcode == employee.Zipcode);
            return View(results);
        }
        [HttpPost]
        public ActionResult Index(string filterDay)
        {
            Employee employee = db.Employees.Where(e => e.UserName == User.Identity.Name).Single();
            var test = db.PickUps.Select(p => p.PickCustomerId).Distinct().ToList();
            var pickUps = db.Customers.Include(p => p.Address).Include(p => p.PickUps).Where(p => test.Contains(p.Id)).ToList();
            var results = pickUps.Where(p => p.Address.Zipcode == employee.Zipcode && p.PickUps.DayOfWeek == filterDay);
            return View(results);
        }
        // GET: PickUps/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PickUps pickUps = db.PickUps.Find(id);
            if (pickUps == null)
            {
                return HttpNotFound();
            }
            return View(pickUps);
        }

        // GET: PickUps/Create
        public ActionResult Create()
        {
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "FirstName");
            return View();
        }

        // POST: PickUps/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PickUps pickUps, string Month, string Date, string DayOfWeek)
        {
            var customer = db.Customers.Where(c => c.UserName == User.Identity.Name).SingleOrDefault();
            var pickupObject = db.PickUps.Where(p => p.PickUpId == customer.PickId).Single();
            pickupObject.PickUpDate = new DateTime(2018, int.Parse(Month), int.Parse(Date));
            pickupObject.Cost += 25;
            //pickUps.PickUpDate = new DateTime(2018, int.Parse(Month), int.Parse(Date));
            //pickUps.PickCustomerId = customer.Id;
            //pickUps.Cost = 25;
            //pickUps.Zipcode = customer.Address.Zipcode;
            if (ModelState.IsValid)
            {
                db.SaveChanges();
                return RedirectToAction("Details", "Customers", new { id = customer.Id });
            }
            //ViewBag.CustomerId = new SelectList(db.Customers, "Id", "FirstName", pickUps.CustomerId);
            return View(pickUps);
        }

        // GET: PickUps/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PickUps pickUps = db.PickUps.Find(id);
            if (pickUps == null)
            {
                return HttpNotFound();
            }
           // ViewBag.CustomerId = new SelectList(db.Customers, "Id", "FirstName", pickUps.CustomerId);
            return View(pickUps);
        }

        // POST: PickUps/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PickUpId,PickUpDate,Cost,CustomerId")] PickUps pickUps)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pickUps).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "FirstName", pickUps.PickUpId);
            return View(pickUps);
        }

        // GET: PickUps/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PickUps pickUps = db.PickUps.Find(id);
            if (pickUps == null)
            {
                return HttpNotFound();
            }
            return View(pickUps);
        }

        // POST: PickUps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PickUps pickUps = db.PickUps.Find(id);
            Customer customer = db.Customers.Where(c => c.PickId == pickUps.PickUpId).Single();
            customer.AccountBalance = pickUps.Cost;
            pickUps.Cost = 75;
            pickUps.PickUpDate = null;
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
