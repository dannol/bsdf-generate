using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using ResortTools.SelfReg.Interfaces;
using ResortTools.SelfReg.Models;
using ResortTools.SelfReg.ViewModels;
using UnityAPI.ClientLibrary.Interfaces;
using UnityModels = UnityAPI.Models;
using UnityAPI.Models.Requests;

namespace ResortTools.SelfReg.Services
{
    public class ContactServiceMock : IContactService
    {
        private readonly IContactProvider _contactProvider;

        public ContactServiceMock(IContactProvider contactProvider)
        {
            _contactProvider = contactProvider;
        }

        public SearchResult<ContactViewModel> GetByContactId(string ContactId)
        {

            SearchResult<ContactViewModel> result = new SearchResult<ContactViewModel>();

            List<ContactViewModel> contacts = new List<ContactViewModel>();
            ContactViewModel Bob = new ContactViewModel
            {
                //ContactId = ContactId,
                FirstName = "Bob",
                LastName = "Account",
                OrderArrivalDate = null,
                CardNumber = "xxxxxxxx-123",
                DateOfBirth = DateTime.Parse("01/01/1982")
            };
            contacts.Add(Bob);


            result.Results = contacts;

            return result;
        }

        public SearchResult<ContactViewModel> GetByCardNumber(string CardNumber, int TerminalId)
        {
            SearchResult<ContactViewModel> result = new SearchResult<ContactViewModel>();

            List<ContactViewModel> contacts = new List<ContactViewModel>();
            ContactViewModel Bob = new ContactViewModel
            {
                //ContactId = 9999,
                FirstName = "Bob",
                LastName = "Card",
                OrderArrivalDate = null,
                CardNumber = CardNumber,
                DateOfBirth = DateTime.Parse("01/01/1986")
            };
            contacts.Add(Bob);

            result.Results = contacts;

            return result;
        }

        public SearchResult<ContactViewModel> GetByOrderId(string OrderId, int TerminalId)
        {
            SearchResult<ContactViewModel> result = new SearchResult<ContactViewModel>();


            ContactSearchRequest contactSearchRequest = new ContactSearchRequest();

            contactSearchRequest.TerminalClientCode = TerminalId;
            contactSearchRequest.SearchString = OrderId;

            var results = _contactProvider.SearchContacts(contactSearchRequest);

            List<ContactViewModel> contacts = new List<ContactViewModel>();
            ContactViewModel Bob = new ContactViewModel
            {
                //ContactId = 1,
                FirstName = "Bob",
                LastName = "Order",
                OrderArrivalDate = null,
                CardNumber = "xxxxxxxx-123",
                DateOfBirth = DateTime.Parse("01/01/1982")
            };
            contacts.Add(Bob);
            ContactViewModel Steve = new ContactViewModel
            {
                //ContactId = 2,
                FirstName = "Steve",
                LastName = "Order",
                OrderArrivalDate = DateTime.Parse("04/04/2019"),
                CardNumber = null,
                DateOfBirth = DateTime.Parse("01/01/1970")
            };
            contacts.Add(Steve);
            ContactViewModel William = new ContactViewModel
            {
                //ContactId = 3,
                FirstName = "William",
                LastName = "Order",
                OrderArrivalDate = null,
                CardNumber = null,
                DateOfBirth = DateTime.Parse("01/01/1980")
            };
            contacts.Add(William);

            result.Results = contacts;

            return result;
        }

        public SearchResult<ContactViewModel> GetByPersonalInfo(Contact Contact, int TerminalId)
        {

            ContactSearchRequest contactSearchRequest = new ContactSearchRequest();
            contactSearchRequest.FirstName = Contact.FirstName;
            contactSearchRequest.LastName = Contact.LastName;
            contactSearchRequest.BirthDate = Contact.DateOfBirth;
            contactSearchRequest.TerminalClientCode = TerminalId;
            contactSearchRequest.SearchString = "";

            var results = _contactProvider.SearchContacts(contactSearchRequest);

            results.Wait();

            SearchResult<ContactViewModel> result = new SearchResult<ContactViewModel>();

            List<ContactViewModel> contacts = new List<ContactViewModel>();

            ContactViewModel Bob = new ContactViewModel
            {
                ContactId = "10",
                FirstName = Contact.FirstName,
                LastName = Contact.LastName,
                OrderArrivalDate = null,
                CardNumber = "xxxxxxxx-123",
                PhotoUrl = "/images/Bob.jpg",
                DateOfBirth = Contact.DateOfBirth
            };
            result.Results.Add(Bob);

            ContactViewModel Steve = new ContactViewModel
            {
                ContactId = "20",
                FirstName = Contact.FirstName,
                LastName = Contact.LastName,
                OrderArrivalDate = DateTime.Parse("04/04/2019"),
                CardNumber = null,
                PhotoUrl = "/images/Steve.jpg",
                DateOfBirth = DateTime.Parse("01/01/1988")
            };
            result.Results.Add(Steve);

            ContactViewModel William = new ContactViewModel
            {
                ContactId = "30",
                FirstName = Contact.FirstName,
                LastName = Contact.LastName + "son",
                OrderArrivalDate = null,
                CardNumber = null,
                PhotoUrl = "/images/William.jpg",
                DateOfBirth = DateTime.Parse("01/01/1999")
            };
            result.Results.Add(William);

            return result;
        }

        public SearchResult<ContactViewModel> GetGroupByContactId(int ContactId)
        {
            SearchResult<ContactViewModel> result = new SearchResult<ContactViewModel>();

            List<ContactViewModel> contacts = new List<ContactViewModel>();
            ContactViewModel Wife = new ContactViewModel
            {
                //ContactId = 10,
                FirstName = "Wife",
                LastName = "GroupMember",
                OrderArrivalDate = null,
                CardNumber = "xxxxxxxx-123",
                PhotoUrl = "/images/Wife.jpg",
                DateOfBirth = DateTime.Parse("01/01/1980")
            };
            result.Results.Add(Wife);
            ContactViewModel Steve = new ContactViewModel
            {
                //ContactId = 11,
                FirstName = "Child1",
                LastName = "GroupMember",
                OrderArrivalDate = DateTime.Parse("04/04/2019"),
                CardNumber = null,
                PhotoUrl = "/images/Steve.jpg",
                DateOfBirth = DateTime.Parse("01/01/2010")
            };
            result.Results.Add(Steve);
            ContactViewModel William = new ContactViewModel
            {
                //ContactId = 12,
                FirstName = "Child2",
                LastName = "GroupMember",
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
            //UpdatedContact.ContactId = 99;
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

        public UpdateResult<ContactViewModel> AddGroupMember(Contact GroupMember)
        {
            ContactViewModel cvm = new ContactViewModel();

            cvm.ContactId = GroupMember.ContactId;
            cvm.DateOfBirth = GroupMember.DateOfBirth;
            cvm.Email = GroupMember.Email;
            cvm.FirstName = GroupMember.FirstName;
            cvm.LastName = GroupMember.LastName;
            cvm.PhotoUrl = GroupMember.PhotoUrl;

            UpdateResult<ContactViewModel> results = new UpdateResult<ContactViewModel>
            {
                Status = "OK",
                UpdatedRecord = cvm
            };

            return results;
        }

        public UpdateResult<ContactViewModel> UpdateContact(Contact Contact)
        {
            //TODO: Fake data
            ContactViewModel UpdatedContact = new ContactViewModel();
            //UpdatedContact.ContactId = 99;
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
