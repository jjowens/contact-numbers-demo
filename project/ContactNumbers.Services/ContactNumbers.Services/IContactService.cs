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
        ServiceResponse SaveCustomer(CustomerDetailsDTO contact);
        ServiceResponse SaveContactNumber(CustomerContactDTO contactDTO);
        ServiceResponse DeleteCustomer(CustomerDetailsDTO contact);
        ServiceResponse DeleteContactNumber(CustomerContactDTO contactDTO);
        ServiceResponse SaveContactType(ContactTypeDTO contactType);
        ServiceResponse DeleteContactType(ContactTypeDTO contactType);
    }
}
