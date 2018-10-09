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
        public SearchResult<ContactViewModel> GetByAccountId(int AccountId)
        {
            SearchResult<ContactViewModel> result = new SearchResult<ContactViewModel>();

            List<ContactViewModel> contacts = new List<ContactViewModel>();
            ContactViewModel Bob = new ContactViewModel
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

        public SearchResult<ContactViewModel> GetByCardNumber(string CardNumber)
        {
            SearchResult<ContactViewModel> result = new SearchResult<ContactViewModel>();

            List<ContactViewModel> contacts = new List<ContactViewModel>();
            ContactViewModel Bob = new ContactViewModel
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

        public SearchResult<ContactViewModel> GetByOrderId(string OrderId)
        {
            SearchResult<ContactViewModel> result = new SearchResult<ContactViewModel>();

            List<ContactViewModel> contacts = new List<ContactViewModel>();
            ContactViewModel Bob = new ContactViewModel
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
            ContactViewModel Steve = new ContactViewModel
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
            ContactViewModel William = new ContactViewModel
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

        public SearchResult<ContactViewModel> GetByPersinalInfo(Contact Contact)
        {
            SearchResult<ContactViewModel> result = new SearchResult<ContactViewModel>();

            List<ContactViewModel> contacts = new List<ContactViewModel>();
            ContactViewModel Bob = new ContactViewModel
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

            ContactViewModel Steve = new ContactViewModel
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

            ContactViewModel William = new ContactViewModel
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

        public SearchResult<ContactViewModel> GetGroupByAccountId(int AccountId)
        {
            SearchResult<ContactViewModel> result = new SearchResult<ContactViewModel>();

            List<ContactViewModel> contacts = new List<ContactViewModel>();
            ContactViewModel Wife = new ContactViewModel
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
            ContactViewModel Steve = new ContactViewModel
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
            ContactViewModel William = new ContactViewModel
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

        public UpdateResult<ContactViewModel> AddContact(Contact Contact)
        {
            //TODO: Fake data
            ContactViewModel UpdatedContact = new ContactViewModel();
            UpdatedContact.AccountId = 99;
            UpdatedContact.DateOfBirth = Contact.DateOfBirth;
            UpdatedContact.Email = Contact.Email;
            UpdatedContact.FirstName = Contact.FirstName;
            UpdatedContact.LastName = Contact.LastName;
            UpdatedContact.PhotoUrl = Contact.PhotoUrl;

            UpdateResult<ContactViewModel> result = new UpdateResult<ContactViewModel>
            {
                Status = "OK",
                UpdatedRecord = UpdatedContact
            };

            return result;
        }

        public UpdateResult<ContactViewModel> UpdateContact(Contact Contact)
        {
            //TODO: Fake data
            ContactViewModel UpdatedContact = new ContactViewModel();
            UpdatedContact.AccountId = 99;
            UpdatedContact.DateOfBirth = Contact.DateOfBirth;
            UpdatedContact.Email = Contact.Email;
            UpdatedContact.FirstName = Contact.FirstName;
            UpdatedContact.LastName = Contact.LastName;
            UpdatedContact.PhotoUrl = Contact.PhotoUrl;

            UpdateResult<ContactViewModel> result = new UpdateResult<ContactViewModel>
            {
                Status = "OK",
                UpdatedRecord = UpdatedContact
            };

            return result;
        }
    }
}
