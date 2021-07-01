using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactNumbers.Engine;

namespace ContactNumbers.Services
{
    public class DatabaseService : IDatabaseService
    {
        public DatabaseService()
        {
            dbContext = new ContactNumbersDB();
        }

        public ContactNumbersDB dbContext { get; set; }
    }
}
