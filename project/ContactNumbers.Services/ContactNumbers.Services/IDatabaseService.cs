using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactNumbers.Engine;

namespace ContactNumbers.Services
{
    public interface IDatabaseService
    {
        ContactNumbersDB dbContext { get; set; }
    }
}
