using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{

    public class CustomersController : Controller
    {
        private ApplicationDbContext _context = null;
        public CustomersController()
        {
            _context = new Models.ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ViewResult Index()
        {
            var customers = _context.Customers.Include(c => c.MembershipType).ToList();

            return View(customers);
        }

        public ActionResult Details(int id)
        {
            var customer = _context.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return HttpNotFound();

            return View(customer);
        }
        public ActionResult New()
        {
            var membershiptypes = _context.MembershipType.ToList();

            var viewmodel = new CustomerFormViewModel()
            {
                MembershipTypes = membershiptypes,
                Customer = new Customer()
            };

            return View("CustomerForm", viewmodel);
        }

        [HttpPost]
        public ActionResult Save(Customer customer)
        {
            if (customer.Id == 0)
            {
                _context.Customers.Add(customer);
            }
            else
            {
                var custInDb = _context.Customers.Single(c => c.Id == customer.Id);
                //TryUpdateModel(custInDb);
                custInDb.Name = customer.Name;
                custInDb.BirthDate = customer.BirthDate;
                custInDb.MembershipTypeId = customer.MembershipTypeId;
                custInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
            }


            _context.SaveChanges();
            return RedirectToAction("Index", "Customers");
        }

        public ActionResult Edit(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
            {
                return HttpNotFound();
            }
            else
            {
                var viewmodel = new CustomerFormViewModel()
                {
                    MembershipTypes = _context.MembershipType.ToList(),
                    Customer = customer
                };

                return View("CustomerForm", viewmodel);
            }

        }
    }



}