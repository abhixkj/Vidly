using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Dtos;
using Vidly.Models;
using System.Data.Entity;


namespace Vidly.Controllers.Api
{
    public class CustomersController : ApiController
    {
        private ApplicationDbContext _context = null;

        public CustomersController()
        {
            _context = new Models.ApplicationDbContext();
        }

        //GET /api/Customers
        public IHttpActionResult GetCustomers()
        {
            var customers =  _context.Customers
                .Include(c=>c.MembershipType)
                .ToList()
                .Select(Mapper.Map<Customer, CustomerDto>);

            return Ok(customers);
        }

        //GET /api/Customers
        public IHttpActionResult GetCustomer(int id)
        {
            var customerInDB = _context.Customers.SingleOrDefault(m => m.Id == id);
            if (customerInDB == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<Customer, CustomerDto>(customerInDB));
            //return Mapper.Map<Customer, CustomerDto>(customerInDB);
            //return customerInDB;
        }

        //POST /api/Customer
        [HttpPost]
        public IHttpActionResult CreateCustomer(CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var customer = Mapper.Map<CustomerDto, Customer>(customerDto);
            _context.Customers.Add(customer);
            _context.SaveChanges();

            customerDto.Id = customer.Id;

            return Created(new Uri(Request.RequestUri + "//" + customerDto.Id), customerDto);
        }

        //PUT /api/Customer/id
        [HttpPut]
        public void UpdateCustomers(int id, CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var customerInDb = _context.Customers.SingleOrDefault(m => m.Id == id);
            if (customerInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            Mapper.Map(customerDto, customerInDb);


            _context.SaveChanges();

        }

        //Delete /api/Customer/id
        [HttpDelete]
        public void DeleteCustomers(int id)
        {

            var customerInDb = _context.Customers.SingleOrDefault(m => m.Id == id);
            if (customerInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            _context.Customers.Remove(customerInDb);
            _context.SaveChanges();

        }

    }
}
