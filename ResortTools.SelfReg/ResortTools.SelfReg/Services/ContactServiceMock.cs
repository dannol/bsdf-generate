using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ResortTools.SelfReg.Interfaces;
using ResortTools.SelfReg.Models;

namespace ResortTools.SelfReg.Services
{
    public class ContactServiceMock : IContactService
    {
        public SearchResult<ContactModel> GetByAccountId(int AccountId)
        {
            SearchResult<ContactModel> result = new SearchResult<ContactModel>();

            List<ContactModel> contacts = new List<ContactModel>();
            ContactModel Bob = new ContactModel
            {
                AccountId = AccountId,
                FirstName = "New",
                LastName = "Weeks",
                Hometown = "Colorado Springs, CO",
                OrderArrivalDate = null,
                JCardNumber = "xxxxxxxx-123"
            };
            contacts.Add(Bob);
            

            result.Results = contacts;

            return result;
        }

        public SearchResult<ContactModel> GetByOrderId(string OrderId)
        {
            SearchResult<ContactModel> result = new SearchResult<ContactModel>();

            List<ContactModel> contacts = new List<ContactModel>();
            ContactModel Bob = new ContactModel
            {
                AccountId = 1,
                FirstName = "Bob",
                LastName = "Weeks",
                Hometown = "Colorado Springs, CO",
                OrderArrivalDate = null,
                JCardNumber = "xxxxxxxx-123"
            };
            contacts.Add(Bob);
            ContactModel Steve = new ContactModel
            {
                AccountId = 2,
                FirstName = "Steve",
                LastName = "Weeks",
                Hometown = "Boise, ID",
                OrderArrivalDate = "April 4, 2019",
                JCardNumber = null
            };
            contacts.Add(Steve);
            ContactModel William = new ContactModel
            {
                AccountId = 3,
                FirstName = "William",
                LastName = "Weeks",
                Hometown = null,
                OrderArrivalDate = null,
                JCardNumber = null
            };
            contacts.Add(William);

            result.Results = contacts;

            return result;
        }

        public SearchResult<ContactModel> GetByPersinalInfo(ContactModel Contact)
        {
            SearchResult<ContactModel> result = new SearchResult<ContactModel>();

            List<ContactModel> contacts = new List<ContactModel>();
            ContactModel Bob = new ContactModel();
            Bob.FirstName = Contact.FirstName;
            Bob.LastName = Contact.LastName;
            Bob.Hometown = "Colorado Springs, CO";
            Bob.OrderArrivalDate = null;
            Bob.JCardNumber = "xxxxxxxx-123";
            Bob.PhotoUrl = "/images/Bob.jpg";
            result.Results.Add(Bob);

            ContactModel Steve = new ContactModel();
            Steve.FirstName = Contact.FirstName;
            Steve.LastName = Contact.LastName;
            Steve.Hometown = "Boise, ID";
            Steve.OrderArrivalDate = "April 4, 2019";
            Steve.JCardNumber = null;
            Steve.PhotoUrl = "/images/Steve.jpg";
            result.Results.Add(Steve);

            ContactModel William = new ContactModel();
            William.FirstName = Contact.FirstName;
            William.LastName = Contact.LastName + "son";
            William.Hometown = null;
            William.OrderArrivalDate = null;
            William.JCardNumber = null;
            William.PhotoUrl = "/images/William.jpg";
            result.Results.Add(William);

            return result;
        }

        public SearchResult<ContactModel> GetGroupByAccountId(int AccountId)
        {
            SearchResult<ContactModel> result = new SearchResult<ContactModel>();

            List<ContactModel> contacts = new List<ContactModel>();
            ContactModel Wife = new ContactModel();
            Wife.FirstName = "Wife";
            Wife.LastName = "Smith";
            Wife.Hometown = "Colorado Springs, CO";
            Wife.OrderArrivalDate = null;
            Wife.JCardNumber = "xxxxxxxx-123";
            Wife.PhotoUrl = "/images/Wife.jpg";
            result.Results.Add(Wife);
            ContactModel Steve = new ContactModel();
            Steve.FirstName = "Child1";
            Steve.LastName = "Smith";
            Steve.Hometown = "Boise, ID";
            Steve.OrderArrivalDate = "April 4, 2019";
            Steve.JCardNumber = null;
            Steve.PhotoUrl = "/images/Steve.jpg";
            result.Results.Add(Steve);
            ContactModel William = new ContactModel();
            William.FirstName = "Child2";
            William.LastName = "Smith";
            William.Hometown = null;
            William.OrderArrivalDate = null;
            William.JCardNumber = null;
            William.PhotoUrl = "/images/William.jpg";
            result.Results.Add(William);

            return result;
        }

        public UpdateResult<ContactModel> AddContact(ContactModel Contact)
        {
            UpdateResult<ContactModel> result = new UpdateResult<ContactModel>
            {
                Status = "OK",
                RecordId = 123
            };

            return result;
        }

        public UpdateResult<ContactModel> AddGroupMember(ContactModel Member, int AccountId)
        {
            UpdateResult<ContactModel> result = new UpdateResult<ContactModel>
            {
                Status = "OK",
                RecordId = 123
            };

            return result;
        }
    }
}
