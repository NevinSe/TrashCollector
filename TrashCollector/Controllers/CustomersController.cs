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
    public class CustomersController : Controller
    {
        
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Customers
        public ActionResult Index()
        {
            var customers = db.Customers.Include(c => c.Address);
            return View(customers.ToList());
        }
        public ActionResult SuspendPickUps(int? id)
        {
            var customer = db.Customers.Where(c => c.Id == id).SingleOrDefault();
            return View(customer);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SuspendPickUps(PickUps pickUps)
        {
            return View(pickUps);
        }
        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            CustomerAddressViewModel customerAddressViewModel = new CustomerAddressViewModel();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            customerAddressViewModel.customer = db.Customers.Find(id);
            customerAddressViewModel.address = db.Addresses.Find(customerAddressViewModel.customer.AddressId);
            //customerAddressViewModel.PickUps = db.PickUps.Find(customerAddressViewModel.)

            if (customerAddressViewModel.customer == null)
            {
                return HttpNotFound();
            }
            return View(customerAddressViewModel);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            CustomerAddressViewModel customerAddressViewModel = new CustomerAddressViewModel()
            {
                address = new Address(),
                customer = new Customer(),
            };
            return View(customerAddressViewModel);
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CustomerAddressViewModel customerAddressViewModel, string DayOfWeek)
        {
            customerAddressViewModel.PickUps = new PickUps();
            //customerAddressViewModel.PickUps.CustomerId = customerAddressViewModel.customer.Id;
            customerAddressViewModel.PickUps.DayOfWeek = DayOfWeek;
            customerAddressViewModel.PickUps.Cost = 75;
            customerAddressViewModel.customer.UserName = User.Identity.Name;
            if (ModelState.IsValid)
            {
                db.PickUps.Add(customerAddressViewModel.PickUps);
                db.Addresses.Add(customerAddressViewModel.address);
                db.Customers.Add(customerAddressViewModel.customer);
                db.SaveChanges();
                return RedirectToAction("Index", "Customers");
            }

            ViewBag.AddressID = new SelectList(db.Addresses, "Id", "Address", customerAddressViewModel.customer.AddressId);
            return View(customerAddressViewModel);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            CustomerAddressViewModel customerAddressViewModel = new CustomerAddressViewModel();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            customerAddressViewModel.customer = db.Customers.Find(id);
            customerAddressViewModel.address = db.Addresses.Find(customerAddressViewModel.customer.AddressId);

            if (customerAddressViewModel.customer == null)
            {
                return HttpNotFound();
            }
            //ViewBag.AddressID = new SelectList(db.Addresses, "Id", "Address", customerAddressViewModel.customer.AddressId);
            return View(customerAddressViewModel);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CustomerAddressViewModel customerAddressViewModel, string dayofweek)
        {
            
            if (ModelState.IsValid)
            {
                var editCustomer = db.Customers.Find(customerAddressViewModel.customer.Id);
                var editAddress = db.Addresses.Where(e => e.CustomerId == customerAddressViewModel.address.CustomerId).Single();
                //var editPickUp = db.PickUps.Where(p => p.CustomerId == customerAddressViewModel.customer.Id).Single();
                editCustomer.FirstName = customerAddressViewModel.customer.FirstName;
                editCustomer.LastName = customerAddressViewModel.customer.LastName;
                editCustomer.UserName = customerAddressViewModel.customer.UserName;
                editCustomer.PhoneNumber = customerAddressViewModel.customer.PhoneNumber;
                editAddress.StreetNumber = customerAddressViewModel.address.StreetNumber;
                editAddress.StreetName = customerAddressViewModel.address.StreetName;
                editAddress.City = customerAddressViewModel.address.City;
                editAddress.State = customerAddressViewModel.address.State;
                editAddress.Zipcode = customerAddressViewModel.address.Zipcode;
                //editPickUp.DayOfWeek = dayofweek;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AddressID = new SelectList(db.Addresses, "Id", "Address", customerAddressViewModel.customer.AddressId);
            return View(customerAddressViewModel);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
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