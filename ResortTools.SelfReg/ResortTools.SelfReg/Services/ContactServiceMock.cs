using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ResortTools.SelfReg.Interfaces;
using ResortTools.SelfReg.Models;
using ResortTools.SelfReg.ViewModels;

namespace ResortTools.SelfReg.Services
{
    public class ContactServiceMock : IContactService
    {
        public SearchResult<ContactModelView> GetByAccountId(int AccountId)
        {
            SearchResult<ContactModelView> result = new SearchResult<ContactModelView>();

            List<ContactModelView> contacts = new List<ContactModelView>();
            ContactModelView Bob = new ContactModelView
            {
                AccountId = AccountId,
                FirstName = "Bob",
                LastName = "Account",
                Hometown = "Colorado Springs, CO",
                OrderArrivalDate = null,
                CardNumber = "xxxxxxxx-123",
                DateOfBirth = DateTime.Parse("01/01/1982")
            };
            contacts.Add(Bob);


            result.Results = contacts;

            return result;
        }

        public SearchResult<ContactModelView> GetByCardNumber(string CardNumber)
        {
            SearchResult<ContactModelView> result = new SearchResult<ContactModelView>();

            List<ContactModelView> contacts = new List<ContactModelView>();
            ContactModelView Bob = new ContactModelView
            {
                AccountId = 9999,
                FirstName = "Bob",
                LastName = "Card",
                Hometown = "Colorado Springs, CO",
                OrderArrivalDate = null,
                CardNumber = CardNumber,
                DateOfBirth = DateTime.Parse("01/01/1986")
            };
            contacts.Add(Bob);

            result.Results = contacts;

            return result;
        }

        public SearchResult<ContactModelView> GetByOrderId(string OrderId)
        {
            SearchResult<ContactModelView> result = new SearchResult<ContactModelView>();

            List<ContactModelView> contacts = new List<ContactModelView>();
            ContactModelView Bob = new ContactModelView
            {
                AccountId = 1,
                FirstName = "Bob",
                LastName = "Order",
                Hometown = "Colorado Springs, CO",
                OrderArrivalDate = null,
                CardNumber = "xxxxxxxx-123",
                DateOfBirth = DateTime.Parse("01/01/1982")
            };
            contacts.Add(Bob);
            ContactModelView Steve = new ContactModelView
            {
                AccountId = 2,
                FirstName = "Steve",
                LastName = "Order",
                Hometown = "Boise, ID",
                OrderArrivalDate = DateTime.Parse("04/04/2019"),
                CardNumber = null,
                DateOfBirth = DateTime.Parse("01/01/1970")
            };
            contacts.Add(Steve);
            ContactModelView William = new ContactModelView
            {
                AccountId = 3,
                FirstName = "William",
                LastName = "Order",
                Hometown = null,
                OrderArrivalDate = null,
                CardNumber = null,
                DateOfBirth = DateTime.Parse("01/01/1980")
            };
            contacts.Add(William);

            result.Results = contacts;

            return result;
        }

        public SearchResult<ContactModelView> GetByPersinalInfo(ContactModelView Contact)
        {
            SearchResult<ContactModelView> result = new SearchResult<ContactModelView>();

            List<ContactModelView> contacts = new List<ContactModelView>();
            ContactModelView Bob = new ContactModelView
            {
                FirstName = Contact.FirstName,
                LastName = Contact.LastName,
                Hometown = "Colorado Springs, CO",
                OrderArrivalDate = null,
                CardNumber = "xxxxxxxx-123",
                PhotoUrl = "/images/Bob.jpg",
                DateOfBirth = Contact.DateOfBirth
            };
            result.Results.Add(Bob);

            ContactModelView Steve = new ContactModelView
            {
                FirstName = Contact.FirstName,
                LastName = Contact.LastName,
                Hometown = "Boise, ID",
                OrderArrivalDate = DateTime.Parse("04/04/2019"),
                CardNumber = null,
                PhotoUrl = "/images/Steve.jpg",
                DateOfBirth = DateTime.Parse("01/01/1988")
            };
            result.Results.Add(Steve);

            ContactModelView William = new ContactModelView
            {
                FirstName = Contact.FirstName,
                LastName = Contact.LastName + "son",
                Hometown = null,
                OrderArrivalDate = null,
                CardNumber = null,
                PhotoUrl = "/images/William.jpg",
                DateOfBirth = DateTime.Parse("01/01/1999")
            };
            result.Results.Add(William);

            return result;
        }

        public SearchResult<ContactModelView> GetGroupByAccountId(int AccountId)
        {
            SearchResult<ContactModelView> result = new SearchResult<ContactModelView>();

            List<ContactModelView> contacts = new List<ContactModelView>();
            ContactModelView Wife = new ContactModelView
            {
                AccountId = 10,
                FirstName = "Wife",
                LastName = "GroupMember",
                Hometown = "Colorado Springs, CO",
                OrderArrivalDate = null,
                CardNumber = "xxxxxxxx-123",
                PhotoUrl = "/images/Wife.jpg",
                DateOfBirth = DateTime.Parse("01/01/1980")
            };
            result.Results.Add(Wife);
            ContactModelView Steve = new ContactModelView
            {
                AccountId = 11,
                FirstName = "Child1",
                LastName = "GroupMember",
                Hometown = "Boise, ID",
                OrderArrivalDate = DateTime.Parse("04/04/2019"),
                CardNumber = null,
                PhotoUrl = "/images/Steve.jpg",
                DateOfBirth = DateTime.Parse("01/01/2010")
            };
            result.Results.Add(Steve);
            ContactModelView William = new ContactModelView
            {
                AccountId = 12,
                FirstName = "Child2",
                LastName = "GroupMember",
                Hometown = null,
                OrderArrivalDate = null,
                CardNumber = null,
                PhotoUrl = "/images/William.jpg",
                DateOfBirth = DateTime.Parse("01/01/2012")
            };
            result.Results.Add(William);

            return result;
        }

        public UpdateResult<ContactModelView> AddContact(ContactModelView Contact)
        {
            //TODO: Fake data
            Contact.AccountId = 99;

            UpdateResult<ContactModelView> result = new UpdateResult<ContactModelView>
            {
                Status = "OK",
                UpdatedRecord = Contact
            };

            return result;
        }

        public UpdateResult<ContactModelView> UpdateContact(ContactModelView Contact)
        {

            UpdateResult<ContactModelView> result = new UpdateResult<ContactModelView>
            {
                Status = "OK",
                UpdatedRecord = Contact
            };

            return result;
        }
    }
}
