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
    public class CraftmanRep : ICraftmanRep
    {
        private readonly Context _Context;
        private readonly IToastNotification _ToastNotification;
        private readonly IMapper _Mapper;
        public CraftmanRep(Context context, IToastNotification toastNotification, IMapper mapper)
        {
            _Context = context;
            _ToastNotification = toastNotification;
            _Mapper = mapper;
        }

        public async Task Add(CraftmanVM Craftman)
        {
            #region Mapping old way
            //var CarftmanDB = new Craftman
            //{

            //    Name = Craftman.Name,
            //    Age = Craftman.Age,
            //    Address = Craftman.Address,
            //    Email = Craftman.Email,
            //    Phone = Craftman.Phone,
            //    Notes = Craftman.Notes,
            //    ServiceId = Craftman.ServiceId,
            //    CityId = Craftman.CityId,

            //};
            #endregion

            var CarftmanDB = _Mapper.Map<Craftman>(Craftman);
            _Context.Craftmens.Add(CarftmanDB);
            await _Context.SaveChangesAsync();
            _ToastNotification.AddSuccessToastMessage("Craftman Added successful !");
            
        }

        public async Task Delete(int id)
        {
            var Craftman =  await _Context.Craftmens.FindAsync(id);
            _Context.Craftmens.Remove(Craftman);
            await _Context.SaveChangesAsync();
           
        }

        public async Task Edit(CraftmanVM Craftman)
        {
            #region Mapping old way
            //var CraftmanDB = _Context.Craftmens.Find(Craftman.Id);
            //CraftmanDB.Name = Craftman.Name;
            //CraftmanDB.Age = Craftman.Age;
            //CraftmanDB.Email = Craftman.Email;
            //CraftmanDB.Phone = Craftman.Phone;
            //CraftmanDB.Address = Craftman.Address;
            //CraftmanDB.ServiceId = Craftman.ServiceId;
            //CraftmanDB.CityId = Craftman.CityId;
            //CraftmanDB.Notes = Craftman.Notes;
            #endregion
            var CarftmanDB = _Mapper.Map<Craftman>(Craftman);
            _Context.Entry(CarftmanDB).State = EntityState.Modified;
            await _Context.SaveChangesAsync();
            _ToastNotification.AddSuccessToastMessage("Craftman Updated successful !");



        }

        public async Task<List<CraftmanVM>> Get()
        {
            var Craftmens = await _Context.Craftmens
                            .Include(c => c.Service)
                            .Include(c => c.City)
                            .AsNoTracking()
                            .ProjectTo<CraftmanVM>(_Mapper.ConfigurationProvider)
                            .ToListAsync();

            return Craftmens;
        }


        public async Task<CraftmanVM> GetById(int id)
        {
            var CraftmanDB = await _Context.Craftmens
                            .Include(c => c.Service)
                            .Include(c => c.City)
                            .AsNoTracking()
                            .FirstOrDefaultAsync(c => c.Id == id);

            if (CraftmanDB == null)
                return null;

            var Craftman = _Mapper.Map<CraftmanVM>(CraftmanDB);

            Craftman.Services = await _Context.Services.ToListAsync();
            Craftman.Cities = await _Context.Cities.ToListAsync();

            return Craftman;
        }


        public async Task< CraftmanVM> IncludeLists()
        {
            var Lists = new CraftmanVM
            {
                Services = await _Context.Services.ToListAsync(),
                Cities = await _Context.Cities.ToListAsync()

            };
            return Lists;
        }
    }
}
