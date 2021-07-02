using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactNumbers.Models;

namespace ContactNumbers.Services
{
    public interface IContactService
    {
        ServiceResponse GetAllCustomers();
        ServiceResponse GetCustomer(int customerID);
        ServiceResponse SaveContactNumber(ContactNumberDTO customerDTO);
        ServiceResponse DeleteCustomerByID(int id);
        ServiceResponse DeleteContactNumberByID(int id);
        ServiceResponse SaveCustomer(CustomerDTO customerDTO);
    }
}
