using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactNumbers.Models
{
    public class CustomerContactDTO
    {
        public CustomerDTO Customer { get; set; }
        public ContactNumberDTO ContactNumber { get; set; }
        public ContactTypeDTO ContactType { get; set; }
    }
}
