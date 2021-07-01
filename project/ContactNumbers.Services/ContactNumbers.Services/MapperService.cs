using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactNumbers.Engine;
using ContactNumbers.Models;

namespace ContactNumbers.Services
{
    public class MapperService : IMapperService
    {
        public MapperService()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AllowNullCollections = true;
                cfg.CreateMap<Customer, CustomerDTO>();
                cfg.CreateMap<ContactType, ContactTypeDTO>();
                cfg.CreateMap<ContactNumber, ContactNumberDTO>();
                cfg.CreateMap<Customer, CustomerDetailsDTO>()
                 .ForMember(a => a.Customer, b => b.MapFrom(cus => cus));

            });

            this.mapper = new Mapper(config);
        }

        public Mapper mapper { get; set; }
    }
}
