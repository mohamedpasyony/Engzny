using AutoMapper;
using AutoMapper.QueryableExtensions;
using Engzny.BL.Interface;
using Engzny.DAL.Database;
using Engzny.Models;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Engzny.BL.Repository
{
    public class ServiceRep : IServiceRep
    {

        private readonly Context _Context;
        private readonly IToastNotification _ToastNotification;
        private readonly IMapper _Mapper;
        public ServiceRep(Context context, IToastNotification toastNotification, IMapper mapper)
        {
            _Context = context;
            _ToastNotification = toastNotification;
            _Mapper = mapper;
        }
        public async Task Add(ServiceVM Service)
        {
            #region old_way_mapping
            //var ServiceDB = new Service
            //{

            //    Name = Service.Name,
            //    Description = Service.Description,
            //    Image = Service.Image
            //};
            #endregion
            var ServiceDB = _Mapper.Map<Service>(Service);
            await _Context.Services.AddAsync(ServiceDB);
           await _Context.SaveChangesAsync();
            _ToastNotification.AddSuccessToastMessage("Service Added successful !");
        }
    
    

        public async Task Delete(byte id)
        {
            var Service = await _Context.Services.FindAsync(id);
            _Context.Services.Remove(Service);
           await  _Context.SaveChangesAsync();
        }

        public async Task Edit(ServiceVM Service)
        {
            var ServiceDB = await _Context.Services.FindAsync(Service.Id);
            ServiceDB.Name = Service.Name;
            ServiceDB.Description = Service.Description;
            if (Service.Image != null)
            {
                ServiceDB.Image = Service.Image;
            }
            await _Context.SaveChangesAsync();
            _ToastNotification.AddSuccessToastMessage("Service Added successful !");


        }

        public async Task<IQueryable<ServiceVM>> Get()
        {
            var services = await _Context.Services
                .ProjectTo<ServiceVM>(_Mapper.ConfigurationProvider)
                .ToListAsync();

            return services.AsQueryable();
        }


        public async Task<ServiceVM>GetById(int id)
        {
            var ServiceDB = await _Context.Services.SingleOrDefaultAsync(a=>a.Id==id);
            var Service = _Mapper.Map<ServiceVM>(ServiceDB);
            return Service;
        }
    }
}
