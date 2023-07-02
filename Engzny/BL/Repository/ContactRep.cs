using AutoMapper;
using Engzny.BL.Helper;
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
    public class ContactRep : IContactRep
    {
        private readonly Context _Context;
        private readonly IToastNotification _ToastNotification;
        private readonly IMapper _Mapper;
        private readonly IMailHelper _mailHelper;
        public ContactRep(Context context, IToastNotification toastNotification, IMapper mapper, IMailHelper mailHelper)
        {
            _Context = context;
            _ToastNotification = toastNotification;
            _Mapper = mapper;
            _mailHelper = mailHelper;
        }




        public async Task Add(ContactVM contact)
        {
            var contactDB = _Mapper.Map<Contact>(contact);
            _Context.Contacts.Add(contactDB);

            await _mailHelper.SendMail("mohamedpasyony41@gmail.com", $"Contact From : {contact.Name}",
                $"Email : {contact.Email} \n\n PhoneNumber :0{contact.Phone} \n\n Message:{contact.Message}");

            await _Context.SaveChangesAsync();
        }


        public async Task Delete(int id)
        {
            var Contact = await _Context.Contacts.FindAsync(id);
            if (Contact != null)
            {
                _Context.Contacts.Remove(Contact);
                await _Context.SaveChangesAsync();
            }
        }


        public async Task<List<ContactVM>>Get()
        {
            var contactsDB = await _Context.Contacts.OrderByDescending(a=>a.ContactDateTime).ToListAsync();
            var Contacts = _Mapper.Map<List<ContactVM>>(contactsDB);

            return Contacts;
        }

        public async Task<ContactVM> GetById(int id)
        {
            var ContactDB =await _Context.Contacts.FirstOrDefaultAsync(a=>a.Id==id);
            var Contact = _Mapper.Map<ContactVM>(ContactDB);
            return Contact;
        }

        public async Task<int> GetRecentRecordId()
        {
            var id = await _Context.Contacts.OrderByDescending(a => a.ContactDateTime).Select(a => a.Id).FirstOrDefaultAsync();
            return id;


        }
    }
}
