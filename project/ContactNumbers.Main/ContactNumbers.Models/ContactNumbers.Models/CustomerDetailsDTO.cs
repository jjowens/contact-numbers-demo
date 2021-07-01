using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactNumbers.Models
{
    public class CustomerDetailsDTO
    {
        public CustomerDTO Customer { get; set; }
        public IEnumerable<ContactNumberDTO> ContactNumbers { get; set; }
    }
}
