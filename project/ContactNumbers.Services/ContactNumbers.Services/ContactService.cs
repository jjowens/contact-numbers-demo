using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactNumbers.Models;
using ContactNumbers.Engine;

namespace ContactNumbers.Services
{
    public class ContactService : IContactService
    {
        public ContactService(IMapperService mapperService, IDatabaseService databaseService)
        {
            _databaseService = databaseService;
            _mapperService = mapperService;
        }

        private readonly IDatabaseService _databaseService;
        private readonly IMapperService _mapperService;

        public ServiceResponse DeleteContactNumber(CustomerContactDTO contactDTO)
        {
            string logMessage = string.Empty;
            bool success = true;

            ServiceResponse serviceResponse = new ServiceResponse();

            serviceResponse.Success = true;
            var dbContext = _databaseService.dbContext;

            var record = dbContext.ContactNumbers.FirstOrDefault(con => con.ContactNumberID == contactDTO.ContactNumber.ContactNumberID);

            if (record != null)
            {
                dbContext.ContactNumbers.Remove(record);

                try
                {
                    dbContext.SaveChanges();
                    logMessage = "Deleted Contact Number";
                    success = false;
                } catch (Exception ex)
                {
                    throw ex;
                }
            } else
            {
                logMessage = "Not found";
                success = false;
            }

            serviceResponse.LogMessage = logMessage;
            serviceResponse.Success = success;

            return serviceResponse;
        }

        public ServiceResponse DeleteContactType(ContactTypeDTO contactTypeDTO)
        {
            string logMessage = string.Empty;
            bool success = true;

            ServiceResponse serviceResponse = new ServiceResponse();

            serviceResponse.Success = true;
            var dbContext = _databaseService.dbContext;

            var record = dbContext.ContactTypes.FirstOrDefault(con => con.ContactTypeID == contactTypeDTO.ContactTypeID);

            if (record != null)
            {
                dbContext.ContactTypes.Remove(record);

                try
                {
                    dbContext.SaveChanges();
                    logMessage = "Deleted Contact Number Type";
                    success = false;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                logMessage = "Contact Type Not Found";
                success = false;
            }

            serviceResponse.LogMessage = logMessage;
            serviceResponse.Success = success;

            return serviceResponse;
        }

        public ServiceResponse DeleteCustomer(CustomerDetailsDTO contact)
        {
            string logMessage = string.Empty;
            bool success = true;

            ServiceResponse serviceResponse = new ServiceResponse();

            serviceResponse.Success = true;
            var dbContext = _databaseService.dbContext;

            var contactNumbers = dbContext.ContactNumbers.Where(con => con.CustomerID == contact.Customer.CustomerID).ToList();
            var customer = dbContext.Customers.FirstOrDefault(cus => cus.CustomerID == contact.Customer.CustomerID);

            if (customer != null && contactNumbers != null)
            {
                dbContext.ContactNumbers.RemoveRange(contactNumbers);
                dbContext.Customers.Remove(customer);

                try
                {
                    dbContext.SaveChanges();
                    logMessage = "Deleted Customer and contact numbers";
                    success = false;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                logMessage = "Customer and/or customer's contact numbers not found";
                success = false;
            }

            serviceResponse.LogMessage = logMessage;
            serviceResponse.Success = success;

            return serviceResponse;
        }

        public ServiceResponse GetAllCustomers()
        {
            string logMessage = string.Empty;
            bool success = true;

            ServiceResponse serviceResponse = new ServiceResponse();

            serviceResponse.Success = true;
            var dbContext = _databaseService.dbContext;

            // GET RECORDS
            var customerData = dbContext.Customers.OrderBy(cus => cus.LastName).ToList();

            // MAPS RESULTS TO OBJECT
            var results = _mapperService.mapper.Map<IEnumerable<Customer>, IEnumerable<CustomerDTO>>(customerData).ToList();

            serviceResponse.CustomersDTO = results;
            serviceResponse.LogMessage = logMessage;
            serviceResponse.Success = success;

            return serviceResponse;
        }

        public ServiceResponse GetCustomer(int customerID)
        {
            string logMessage = string.Empty;
            bool success = true;

            ServiceResponse serviceResponse = new ServiceResponse();

            serviceResponse.Success = true;
            var dbContext = _databaseService.dbContext;

            // GTE RECORDS
            var customerObj = (from customer in dbContext.Customers
                         where customer.CustomerID == customerID
                         select customer
                         ).FirstOrDefault();

            var contactNumbers = (from contactNumber in dbContext.ContactNumbers
                                  join contactType in dbContext.ContactTypes on contactNumber.ContactNumberTypeID equals contactType.ContactTypeID
                                  where contactNumber.CustomerID == customerID
                                  select new ContactNumberDTO
                                  {
                                    ContactNumber = contactNumber.ContactNumber1,
                                    ContactNumberID = contactNumber.ContactNumberID,
                                    ContactNumberTypeID = contactNumber.ContactNumberTypeID,
                                    ContactTypeName = contactType.ContactTypeName,
                                    CustomerID = customerID
                                  }).ToList();

            // RETURN RECORDS
            var results = new CustomerDetailsDTO
                            {
                                Customer = _mapperService.mapper.Map<Customer, CustomerDTO>(customerObj),
                                ContactNumbers = contactNumbers
                            };

            serviceResponse.CustomerDetailDTO = results;
            serviceResponse.LogMessage = logMessage;
            serviceResponse.Success = success;

            return serviceResponse;
        }

        public ServiceResponse SaveContactNumber(CustomerContactDTO contactDTO)
        {
            throw new NotImplementedException();
        }

        public ServiceResponse SaveContactType(ContactTypeDTO contactType)
        {
            throw new NotImplementedException();
        }

        public ServiceResponse SaveCustomer(CustomerDetailsDTO contact)
        {
            throw new NotImplementedException();
        }
    }
}
