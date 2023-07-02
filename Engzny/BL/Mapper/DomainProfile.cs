using AutoMapper;
using Engzny.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Engzny.BL.Mapper
{
    public class DomainProfile:Profile
    {

        public DomainProfile()
        {
            CreateMap<Service, ServiceVM>();
            CreateMap<ServiceVM, Service>();

            CreateMap<Craftman, CraftmanVM>();
            CreateMap<CraftmanVM, Craftman>();

            CreateMap<Contact, ContactVM>();
            CreateMap<ContactVM, Contact>();

            CreateMap<Order, OrderVM>();
            CreateMap<OrderVM, Order>();

           




        }
    }
}
