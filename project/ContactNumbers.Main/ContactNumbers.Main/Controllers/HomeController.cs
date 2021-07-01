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
        //public HomeController(IContactService contactService)
        //{
        //    _contactService = contactService;
        //}

        private IContactService _contactService;

        // GET: Home
        public ActionResult Index()
        {


            return View();
        }

        public ActionResult NewCustomer(CustomerDTO customer)
        {

            return View();
        }

        public ActionResult EditCustomer(int id)
        {
            var customerDetails = _contactService.GetCustomer(id);

            return View(customerDetails);
        }
    }
}