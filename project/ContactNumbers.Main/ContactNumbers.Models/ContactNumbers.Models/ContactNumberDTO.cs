using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactNumbers.Models
{
    public class ContactNumberDTO
    {
        public int ContactNumberID { get; set; }
        public string ContactNumber { get; set; }
        public int CustomerID { get; set; }
        public int ContactNumberTypeID { get; set; }
        public string ContactTypeName { get; set; }
    }
}
