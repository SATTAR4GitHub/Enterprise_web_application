using Fashionplex.Data;
using Fashionplex.IRepository;
using Fashionplex.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fashionplex.Repository
{
    /// <summary>
    /// This class contains all the methods to complete CRUD (Create, read, update, delete) operation for the customer tables.
    /// </summary>
    public class CustomerRepository : ICustomerRepository
    {
        private ApplicationDbContext _context;

        /// <summary>
        /// Initialize DbContext
        /// </summary>
        /// <param name="context"></param>
        public CustomerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Customer FindCustomerById(long id)
        {
            var note = _context.Customers.Find(id);
            return note;
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            var notes = _context.Customers;
            return notes;
        }

        public void SaveCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();
        }

        public void UpdateCustomer(Customer customer)
        {
            _context.Customers.Update(customer);
            _context.SaveChanges();
        }

        public void DeleteCustomer(Customer customer)
        {
            _context.Customers.Remove(customer);
            _context.SaveChanges();
        }
    }
}
