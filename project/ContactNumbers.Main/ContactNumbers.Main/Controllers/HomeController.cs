using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ContactNumbers.Services;
using ContactNumbers.Models;

namespace ContactNumbers.Main.Controllers
{
    public class HomeController : Controller
    {
        private IContactService _contactService;

        public HomeController(IContactService contactService)
        {
            _contactService = contactService;
        }

        // GET: Home
        public ActionResult Index()
        {
            var listOfCustomers = _contactService.GetAllCustomers();

            return View(listOfCustomers.CustomersDTO);
        }

        public ActionResult NewCustomer(CustomerDTO customer)
        {
            var customerObj = _contactService.SaveCustomer(customer);

            return RedirectToAction("Index");
        }

        public ActionResult EditCustomer(int id)
        {
            var customerDetails = _contactService.GetCustomer(id);

            return View(customerDetails.CustomerDetailDTO);
        }

        public ActionResult SaveCustomer(CustomerDTO customer)
        {
            var customerObj = _contactService.SaveCustomer(customer);

            return RedirectToAction("EditCustomer", new { id = customer.CustomerID });
        }

        public ActionResult SaveContactNumber(ContactNumberDTO contactNumberDTO)
        {
            var customerObj = _contactService.SaveContactNumber(contactNumberDTO);

            return RedirectToAction("EditCustomer", new { id = contactNumberDTO.CustomerID });
        }

        public ActionResult DeleteContactNumber(int contactNumberID, int customerID)
        {
            var customerObj = _contactService.DeleteContactNumberByID(contactNumberID);

            return RedirectToAction("EditCustomer", new { id = customerID });
        }

        public ActionResult DeleteCustomer(int id)
        {
            var customerObj = _contactService.DeleteCustomerByID(id);

            return RedirectToAction("Index");
        }
    }
}