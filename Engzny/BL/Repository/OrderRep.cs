using AutoMapper;
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
    public class OrderRep : IOrderRep
    {
        private readonly Context _Context;
        private readonly IToastNotification _ToastNotification;

        private readonly IMapper _Mapper;

        public OrderRep(Context context, IMapper mapper, IToastNotification toastNotification)
        {
            _Context = context;
            _Mapper = mapper;
            _ToastNotification = toastNotification;
        }


        public async Task Add(OrderVM order)
        {
            var OrderDB = _Mapper.Map<Order>(order);
            await _Context.Orders.AddAsync(OrderDB);
            await  _Context.SaveChangesAsync();

        }

        public async Task Delete(int id)
        {
            var Order = await _Context.Orders.FindAsync(id);
            _Context.Orders.Remove(Order);
           await  _Context.SaveChangesAsync();
        }

        public async Task Edit(OrderVM Order)
        {
            var OrderDB = _Mapper.Map<Order>(Order);
            _Context.Entry(OrderDB).State = EntityState.Modified;
            await _Context.SaveChangesAsync();
            _ToastNotification.AddSuccessToastMessage("Status Updated successful !");
        }

        public async Task<IQueryable<OrderVM>> Get()
        {
            var OrderDB = await _Context.Orders
                .Include(a => a.Service)
                .Include(a => a.Status)
                .AsNoTracking()
                .ToListAsync();

            var Orders = _Mapper.Map<List<OrderVM>>(OrderDB);

            return Orders.AsQueryable();
        }


        public async Task<OrderVM> GetById(int id)
        {
            var OrderDB = await _Context.Orders
                .Include(a => a.Service)
                .Include(a => a.Status)
                .FirstOrDefaultAsync(a => a.Id == id);

            var Order = _Mapper.Map<OrderVM>(OrderDB);

            if (Order != null)
            {
                Order.statuses = await _Context.OrderStatuses.ToListAsync();
            }

            return Order;
        }

        public async Task<IQueryable<OrderVM>> GetOrdersByStatus(int id)
        {
            var OrderDB = await _Context.Orders
                .Where(a=>a.StatusId==id)
               .Include(a => a.Service)
               .Include(a => a.Status)
               .AsNoTracking()
               .ToListAsync();

            var Orders = _Mapper.Map<List<OrderVM>>(OrderDB);

            return Orders.AsQueryable();

        }

        public async Task< OrderVM> IncludeLists()
        {
            var Lists = new OrderVM
            {
                Services = await _Context.Services.ToListAsync(),

            };
            return Lists;
        }

        public async Task<OrderVM> GetRecentOrder()
        {
            var OrderDB = await _Context.Orders
                .Include(a => a.Service)
                .OrderByDescending(a=>a.OrderDateTime)
                .FirstOrDefaultAsync();
            var Order = _Mapper.Map<OrderVM>(OrderDB);

            return Order;
        }
    }
}
