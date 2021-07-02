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

        public ServiceResponse DeleteContactNumberByID(int id)
        {
            string logMessage = string.Empty;
            bool success = true;

            ServiceResponse serviceResponse = new ServiceResponse();

            serviceResponse.Success = true;
            var dbContext = _databaseService.dbContext;

            var record = dbContext.ContactNumbers.FirstOrDefault(con => con.ContactNumberID == id);

            if (record != null)
            {
                dbContext.ContactNumbers.Remove(record);

                try
                {
                    dbContext.SaveChanges();
                    logMessage = "Deleted Contact Number";
                    success = false;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                logMessage = "Not found";
                success = false;
            }

            serviceResponse.LogMessage = logMessage;
            serviceResponse.Success = success;

            return serviceResponse;
        }

        public ServiceResponse DeleteCustomerByID(int id)
        {
            string logMessage = string.Empty;
            bool success = true;

            ServiceResponse serviceResponse = new ServiceResponse();

            serviceResponse.Success = true;
            var dbContext = _databaseService.dbContext;

            var contactNumbers = dbContext.ContactNumbers.Where(con => con.CustomerID == id).ToList();
            var customer = dbContext.Customers.FirstOrDefault(cus => cus.CustomerID == id);

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
                logMessage = "Customer and/or contact numbers not found";
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

            var contactTypes = dbContext.ContactTypes.ToList();

            // RETURN RECORDS
            var results = new CustomerDetailsDTO
                            {
                                Customer = _mapperService.mapper.Map<Customer, CustomerDTO>(customerObj),
                                ContactNumbers = contactNumbers,
                                ContactTypes = _mapperService.mapper.Map<List<ContactType>, List<ContactTypeDTO>>(contactTypes)
                            };

            serviceResponse.CustomerDetailDTO = results;
            serviceResponse.LogMessage = logMessage;
            serviceResponse.Success = success;

            return serviceResponse;
        }

        public ServiceResponse SaveContactNumber(ContactNumberDTO customerDTO)
        {
            ServiceResponse serviceResponse = new ServiceResponse();
            string logMessage = string.Empty;
            bool success = true;

            // CHECK CONTACT NUMBER.
            if (string.IsNullOrEmpty(customerDTO.ContactNumber))
            {
                success = false;
                logMessage = "No Contact Number not provided";
                return serviceResponse;
            }

            serviceResponse.Success = true;
            var dbContext = _databaseService.dbContext;

            // CHECK IF CONTACT NUMBER ALREADY EXISTS
            if (customerDTO.ContactNumberID == 0)
            {
                var existObj = dbContext.ContactNumbers.FirstOrDefault(con => con.ContactNumber1 == customerDTO.ContactNumber && con.CustomerID == customerDTO.CustomerID);

                if (existObj != null)
                {
                    success = false;
                    logMessage = "Contact number already exists";
                }
                else
                {
                    var newContactNumber = new ContactNumber()
                    {
                        ContactNumber1 = customerDTO.ContactNumber,
                        ContactNumberID = 0,
                        CustomerID = customerDTO.CustomerID,
                        ContactNumberTypeID = (customerDTO.ContactNumberTypeID == 0) ? 1 : customerDTO.ContactNumberTypeID
                    };

                    dbContext.ContactNumbers.Add(newContactNumber);

                    dbContext.SaveChanges();

                    success = true;
                    logMessage = "Added new Contact Number";
                }
            }
            else
            {
                var currentObj = dbContext.ContactNumbers.FirstOrDefault(con => con.ContactNumberID == customerDTO.ContactNumberID);

                if (currentObj == null)
                {
                    success = false;
                    logMessage = "Contact number does not exists";
                }
                else
                {
                    currentObj.ContactNumber1 = customerDTO.ContactNumber;
                    currentObj.ContactNumberTypeID = customerDTO.ContactNumberTypeID;

                    dbContext.SaveChanges();

                    success = true;
                    logMessage = "Updated contact number";
                }
            }

            serviceResponse.Success = success;

            return serviceResponse;
        }

        public ServiceResponse SaveCustomer(CustomerDTO customerDTO)
        {
            string logMessage = string.Empty;
            bool success = true;

            ServiceResponse serviceResponse = new ServiceResponse();

            serviceResponse.Success = true;
            var dbContext = _databaseService.dbContext;

            // CHECK IF CUSTOMER ALREADY EXISTS
            if (customerDTO.CustomerID == 0)
            {
                var existObj = dbContext.Customers.FirstOrDefault(cus => cus.FirstName == customerDTO.FirstName && cus.LastName == customerDTO.LastName);

                if (existObj != null)
                {
                    success = false;
                    logMessage = "Customer already exists";
                }
                else
                {
                    var newObj = _mapperService.mapper.Map<CustomerDTO, Customer>(customerDTO);

                    dbContext.Customers.Add(newObj);

                    dbContext.SaveChanges();

                    success = true;
                    logMessage = "Added new Customer";
                }
            } else
            {
                var currentObj = dbContext.Customers.FirstOrDefault(cus => cus.CustomerID == customerDTO.CustomerID);

                if (currentObj == null)
                {
                    success = false;
                    logMessage = "Customer does not exists";
                }
                else
                {
                    currentObj.FirstName = customerDTO.FirstName;
                    currentObj.LastName = customerDTO.LastName;

                    dbContext.SaveChanges();

                    success = true;
                    logMessage = "Updated new Customer";
                }
            }

            serviceResponse.Success = success;
            serviceResponse.LogMessage = logMessage;

            return serviceResponse;
        }
    }
}
