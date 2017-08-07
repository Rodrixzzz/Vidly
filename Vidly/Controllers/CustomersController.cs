using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using System.Data.Entity;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        private ApplicationDbContext _contex;
        public CustomersController()
        {
            _contex = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _contex.Dispose();
        }
        // GET: Customers
        public ViewResult index()
        {
            var customers = _contex.Customers.Include(c => c.MembershipType).ToList();
            return View(customers);
        }
         public ActionResult Details(int id)
        {
            var customer = _contex.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return HttpNotFound();

            return View(customer);
        }
         public ActionResult New()
         {
             var membershipTypes = _contex.MembershipTypes.ToList();
             var viewModel = new CustomerFormViewModel
             {
                 MembershipTypes = membershipTypes
             };
             return View("CustomerForm",viewModel);
         }
        [HttpPost]
        [ValidateAntiForgeryToken]
         public ActionResult Save(Customer customer)
         {
            if (!ModelState.IsValid)
            {
                var viewModel = new CustomerFormViewModel
                {
                    Customer=customer,
                    MembershipTypes=_contex.MembershipTypes.ToList()
                };
                return View("CustomerForm",viewModel);
            }
            if (customer.Id==0)
            {
                _contex.Customers.Add(customer);
            }
            else
            {
                var customerInDb = _contex.Customers.Single(c => c.Id == customer.Id);
                customerInDb.Name = customer.Name;
                customerInDb.BirthDate = customer.BirthDate;
                customerInDb.IsSuscribedToNewsletter = customer.IsSuscribedToNewsletter;
                customerInDb.MembershipTypeId = customer.MembershipTypeId;
            }
             
             _contex.SaveChanges();
             return RedirectToAction("Index", "Customers");
         }
        public ActionResult Edit(int id)
        {
            var customer = _contex.Customers.SingleOrDefault(c => c.Id == id);
         
            if (customer == null)
                return HttpNotFound();

            var viewModel = new CustomerFormViewModel
            {
                Customer=customer,
                MembershipTypes=_contex.MembershipTypes.ToList()
            };
            return View("CustomerForm",viewModel);
        }
        
    }

}