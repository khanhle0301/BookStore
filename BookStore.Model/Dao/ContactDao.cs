using BookStore.Model.Models;
using System;
using System.Linq;

namespace BookStore.Model.Dao
{
    public class ContactDao
    {
        private BookStoreDbContext db = null;

        public ContactDao()
        {
            db = new BookStoreDbContext();
        }

        public ContactDetail GetContact()
        {
            return db.ContactDetails.FirstOrDefault();
        }

        public ContactDetail GetById(string id)
        {
            return db.ContactDetails.SingleOrDefault(x => x.ID == id);
        }

        public bool Update(ContactDetail entity)
        {
            try
            {
                var Contact = db.ContactDetails.SingleOrDefault(x => x.ID == entity.ID);
                Contact.CompanyName = entity.CompanyName;
                Contact.CompanyPhone = entity.CompanyPhone;
                Contact.CompanyEmail = entity.CompanyEmail;
                Contact.CompanyWebsite = entity.CompanyWebsite;
                Contact.CompanyAddress = entity.CompanyAddress;
                Contact.CompanyFax = entity.CompanyFax;
                Contact.Lat = entity.Lat;
                Contact.Lng = entity.Lng;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}