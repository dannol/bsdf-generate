using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using ResortTools.SelfReg.Interfaces;
using ResortTools.SelfReg.Models;
using ResortTools.SelfReg.ViewModels;
using UnityAPI.ClientLibrary.Interfaces;
using UnityModels = UnityAPI.Models;
using UnityAPI.Models.Responses;
using UnityAPI.Models.Requests;

namespace ResortTools.SelfReg.Services
{
    public class ContactService : IContactService
    {
        private readonly IContactProvider _contactProvider;

        public ContactService(IContactProvider contactProvider)
        {
            _contactProvider = contactProvider;
        }

        //public SearchResult<ContactViewModel> GetByAccountId(string AccountId)
        //{

        //    SearchResult<ContactViewModel> result = new SearchResult<ContactViewModel>();

        //    List<ContactViewModel> contacts = new List<ContactViewModel>();
        //    ContactViewModel Bob = new ContactViewModel
        //    {
        //        //AccountId = AccountId,
        //        FirstName = "Bob",
        //        LastName = "Account",
        //        Hometown = "Colorado Springs, CO",
        //        OrderArrivalDate = null,
        //        CardNumber = "xxxxxxxx-123",
        //        DateOfBirth = DateTime.Parse("01/01/1982")
        //    };
        //    contacts.Add(Bob);


        //    result.Results = contacts;

        //    return result;
        //}

        public SearchResult<ContactViewModel> GetByCardNumber(string CardNumber, int TerminalId)
        {
            ContactSearchRequest contactSearchRequest = new ContactSearchRequest();
            contactSearchRequest.SearchString = CardNumber;
            contactSearchRequest.TerminalClientCode = TerminalId;

            var result = _contactProvider.SearchContacts(contactSearchRequest);

            result.Wait();

            SearchResult<ContactViewModel> searchResults = new SearchResult<ContactViewModel>();

            foreach (ContactSearchItem csi in result.Result.SearchResults)
            {
                string cardNumberString = csi.MediaIdentification.ChipId.ToString();
                string maskedCardNumber = "XXXX-" + cardNumberString.Substring(cardNumberString.Length - 4);

                ContactViewModel cvm = new ContactViewModel
                {
                    AccountId = csi.ContactId.GetRecId(String.Empty, UnityModels.InfoSourceType.Master),
                    ParentAccountId = csi.PrimaryContactId.Ids[0].InfoRecId,
                    FirstName = csi.FirstName,
                    LastName = csi.LastName,
                    DateOfBirth = csi.BirthDate,
                    OrderArrivalDate = csi.OrderArrivalDate,
                    Email = csi.EmailAddress,
                    CardNumber = maskedCardNumber
                };

                searchResults.Results.Add(cvm);

            }

            return searchResults;
        }

        public SearchResult<ContactViewModel> GetByOrderId(string OrderId, int TerminalId)
        {

            ContactSearchRequest contactSearchRequest = new ContactSearchRequest();
            contactSearchRequest.SearchString = OrderId;
            contactSearchRequest.TerminalClientCode = TerminalId;

            var result = _contactProvider.SearchContacts(contactSearchRequest);

            result.Wait();

            SearchResult<ContactViewModel> searchResults = new SearchResult<ContactViewModel>();

            foreach (ContactSearchItem csi in result.Result.SearchResults)
            {
                ContactViewModel cvm = new ContactViewModel
                {
                    AccountId = csi.ContactId.GetRecId(String.Empty, UnityModels.InfoSourceType.Master),
                    ParentAccountId = csi.PrimaryContactId.Ids[0].InfoRecId,
                    FirstName = csi.FirstName,
                    LastName = csi.LastName,
                    DateOfBirth = csi.BirthDate,
                    OrderArrivalDate = csi.OrderArrivalDate,
                    Email = csi.EmailAddress,
                    CardNumber = csi.MediaIdentification.ChipId
                };

                searchResults.Results.Add(cvm);

            }

            return searchResults;
        }

        public SearchResult<ContactViewModel> GetByPersonalInfo(Contact Contact, int TerminalId)
        {

            ContactSearchRequest contactSearchRequest = new ContactSearchRequest();
            contactSearchRequest.FirstName = Contact.FirstName;
            contactSearchRequest.LastName = Contact.LastName;
            contactSearchRequest.BirthDate = Contact.DateOfBirth;
            contactSearchRequest.TerminalClientCode = TerminalId;
            contactSearchRequest.SearchString = "";

            var result = _contactProvider.SearchContacts(contactSearchRequest);

            result.Wait();

            return LoadSearchResults(result.Result.SearchResults);
        }

        public SearchResult<ContactViewModel> GetGroupByAccountId(int AccountId, int TerminalId)
        {

            //TODO: Replace the code in this method actual Unity API call
            SearchResult<ContactViewModel> result = new SearchResult<ContactViewModel>();

            List<ContactViewModel> contacts = new List<ContactViewModel>();
            ContactViewModel Wife = new ContactViewModel
            {
                AccountId = "10",
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
                AccountId = "11",
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
                AccountId = "12",
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
            //UpdatedContact.AccountId = 99;
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
            //UpdatedContact.AccountId = 99;
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

        // * Load SearchResults
        // * This function is only used by the Contact Services and loads Unity API Contact search
        // * results into search results used by the Self Registration app.
        private SearchResult<ContactViewModel> LoadSearchResults(IList<ContactSearchItem> unityContacts)
        {
            SearchResult<ContactViewModel> contactSearchResults = new SearchResult<ContactViewModel>();

            foreach (ContactSearchItem csi in unityContacts)
            {
                string maskedCardNumber = csi.MediaIdentification.ChipId.ToString();

                if (!String.IsNullOrEmpty(maskedCardNumber))
                {
                    string cardNumberString = csi.MediaIdentification.ChipId.ToString();
                    maskedCardNumber = "XXXX-" + cardNumberString.Substring(cardNumberString.Length - 4);
                }

                ContactViewModel cvm = new ContactViewModel
                {
                    AccountId = csi.ContactId.GetRecId(String.Empty, UnityModels.InfoSourceType.Master),
                    ParentAccountId = csi.PrimaryContactId.Ids[0].InfoRecId,
                    FirstName = csi.FirstName,
                    LastName = csi.LastName,
                    DateOfBirth = csi.BirthDate,
                    OrderArrivalDate = csi.OrderArrivalDate,
                    Email = csi.EmailAddress,
                    CardNumber = maskedCardNumber
                };

                contactSearchResults.Results.Add(cvm);

            }

            return contactSearchResults;
        }


    }
}
