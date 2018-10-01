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
        public SearchResult<Contact> GetByAccountId(int AccountId)
        {
            SearchResult<Contact> result = new SearchResult<Contact>();

            List<Contact> contacts = new List<Contact>();
            Contact Bob = new Contact
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

        public SearchResult<Contact> GetByCardNumber(string CardNumber)
        {
            SearchResult<Contact> result = new SearchResult<Contact>();

            List<Contact> contacts = new List<Contact>();
            Contact Bob = new Contact
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

        public SearchResult<Contact> GetByOrderId(string OrderId)
        {
            SearchResult<Contact> result = new SearchResult<Contact>();

            List<Contact> contacts = new List<Contact>();
            Contact Bob = new Contact
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
            Contact Steve = new Contact
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
            Contact William = new Contact
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

        public SearchResult<Contact> GetByPersinalInfo(Contact Contact)
        {
            SearchResult<Contact> result = new SearchResult<Contact>();

            List<Contact> contacts = new List<Contact>();
            Contact Bob = new Contact
            {
                AccountId = 10,
                FirstName = Contact.FirstName,
                LastName = Contact.LastName,
                Hometown = "Colorado Springs, CO",
                OrderArrivalDate = null,
                CardNumber = "xxxxxxxx-123",
                PhotoUrl = "/images/Bob.jpg",
                DateOfBirth = Contact.DateOfBirth
            };
            result.Results.Add(Bob);

            Contact Steve = new Contact
            {
                AccountId = 20,
                FirstName = Contact.FirstName,
                LastName = Contact.LastName,
                Hometown = "Boise, ID",
                OrderArrivalDate = DateTime.Parse("04/04/2019"),
                CardNumber = null,
                PhotoUrl = "/images/Steve.jpg",
                DateOfBirth = DateTime.Parse("01/01/1988")
            };
            result.Results.Add(Steve);

            Contact William = new Contact
            {
                AccountId = 30,
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

        public SearchResult<Contact> GetGroupByAccountId(int AccountId)
        {
            SearchResult<Contact> result = new SearchResult<Contact>();

            List<Contact> contacts = new List<Contact>();
            Contact Wife = new Contact
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
            Contact Steve = new Contact
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
            Contact William = new Contact
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

        public UpdateResult<Contact> AddContact(Contact Contact)
        {
            //TODO: Fake data
            Contact.AccountId = 99;

            UpdateResult<Contact> result = new UpdateResult<Contact>
            {
                Status = "OK",
                UpdatedRecord = Contact
            };

            return result;
        }

        public UpdateResult<Contact> UpdateContact(Contact Contact)
        {

            UpdateResult<Contact> result = new UpdateResult<Contact>
            {
                Status = "OK",
                UpdatedRecord = Contact
            };

            return result;
        }
    }
}
