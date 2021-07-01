using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace ContactNumbers.Services
{
    public interface IMapperService
    {
        Mapper mapper { get; set; }
    }
}
