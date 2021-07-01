using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactNumbers.Models
{
    public class ServiceResponse
    {
        public bool Success { get; set; }
        public string LogMessage { get; set; }

        public List<CustomerDetailsDTO> ListOfCustomerDetails { get; set; }
        public List<ContactTypeDTO> ContactTypes { get; set; }
        public List<ContactNumberDTO> ContactNumbers { get; set; }
        public List<CustomerDTO> CustomersDTO { get; set; }
        public CustomerDetailsDTO CustomerDetailDTO { get; set; }
    }
}
