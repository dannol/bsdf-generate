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
        private readonly ICustomerProvider _customerProvider;

        public ContactService(IContactProvider contactProvider, ICustomerProvider customerProvider)
        {
            _contactProvider = contactProvider;
            _customerProvider = customerProvider;
        }

        //public SearchResult<ContactViewModel> GetByContactId(string ContactId)
        //{

        //    SearchResult<ContactViewModel> result = new SearchResult<ContactViewModel>();

        //    List<ContactViewModel> contacts = new List<ContactViewModel>();
        //    ContactViewModel Bob = new ContactViewModel
        //    {
        //        //ContactId = ContactId,
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
            ContactSearchRequest contactSearchRequest = new ContactSearchRequest
            {
                SearchString = CardNumber,
                TerminalClientCode = TerminalId
            };

            var result = _contactProvider.SearchContacts(contactSearchRequest);

            result.Wait();

            SearchResult<ContactViewModel> searchResults = new SearchResult<ContactViewModel>();

            foreach (ContactSearchItem csi in result.Result.SearchResults)
            {
                string cardNumberString = csi.MediaIdentification.ChipId.ToString();
                string maskedCardNumber = "XXXX-" + cardNumberString.Substring(cardNumberString.Length - 4);

                ContactViewModel cvm = new ContactViewModel
                {
                    ContactId = csi.ContactId.GetRecId(String.Empty, UnityModels.InfoSourceType.Master),
                    ParentContactId = csi.PrimaryContactId.Ids[0].InfoRecId,
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

            ContactSearchRequest contactSearchRequest = new ContactSearchRequest
            {
                SearchString = OrderId,
                TerminalClientCode = TerminalId
            };

            var result = _contactProvider.SearchContacts(contactSearchRequest);

            result.Wait();

            SearchResult<ContactViewModel> searchResults = new SearchResult<ContactViewModel>();

            foreach (ContactSearchItem csi in result.Result.SearchResults)
            {
                ContactViewModel cvm = new ContactViewModel
                {
                    ContactId = csi.ContactId.GetRecId(String.Empty, UnityModels.InfoSourceType.Master),
                    ParentContactId = csi.PrimaryContactId.Ids[0].InfoRecId,
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

            ContactSearchRequest contactSearchRequest = new ContactSearchRequest
            {
                FirstName = Contact.FirstName,
                LastName = Contact.LastName,
                BirthDate = Contact.DateOfBirth,
                TerminalClientCode = TerminalId,
                SearchString = ""
            };

            var result = _contactProvider.SearchContacts(contactSearchRequest);

            result.Wait();

            return LoadSearchResults(result.Result.SearchResults);
        }

        public SearchResult<ContactViewModel> GetGroupByContactId(int ContactId, int TerminalId)
        {

            //TODO: Replace the code in this method actual Unity API call
            SearchResult<ContactViewModel> result = new SearchResult<ContactViewModel>();

            List<ContactViewModel> contacts = new List<ContactViewModel>();
            ContactViewModel Wife = new ContactViewModel
            {
                ContactId = "10",
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
                ContactId = "11",
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
                ContactId = "12",
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
            CreateContactRequest createContactRequest = new CreateContactRequest
            {
                Contact = new UnityModels.Contact
                {  
                    FirstName = Contact.FirstName,
                    LastName = Contact.LastName,
                    DateOfBirth = Contact.DateOfBirth.ToString(),
                    Email = Contact.Email,
                    Phone = Contact.Phone,
                    StreetAddress = Contact.Address1,
                    StreetAddress2 = Contact.Address2,
                    City = Contact.City,
                    StateProvince = Contact.State,
                    ZipPostalCode = Contact.PostalCode                  
                }
            };

            var addResult = _customerProvider.CreateContact(createContactRequest);

            addResult.Wait();

            ContactViewModel cvm = new ContactViewModel();
            string resultStatus = "";
          
            if (addResult.Status == System.Threading.Tasks.TaskStatus.RanToCompletion)
            {
                cvm.ContactId = addResult.Result.ContactId.GetRecId(String.Empty, UnityModels.InfoSourceType.Master);
                cvm.DateOfBirth = Contact.DateOfBirth;
                cvm.Email = Contact.Email;
                cvm.FirstName = Contact.FirstName;
                cvm.LastName = Contact.LastName;
                cvm.PhotoUrl = "";
                resultStatus = "OK";
            }
            else
            {
                resultStatus = addResult.Status.ToString();
            }
            
            UpdateResult<ContactViewModel> results = new UpdateResult<ContactViewModel>
            {
                Status = resultStatus,
                UpdatedRecord = cvm
            };

            return results;
        }

        public UpdateResult<ContactViewModel> UpdateContact(Contact Contact)
        {
            //Create an identifier object based on the ID of the contact
            UnityModels.Identifier contactId = new UnityModels.Identifier(UnityModels.InfoSourceType.Master, Contact.ContactId);

            //Create a Unity API Contact for updating
            UnityModels.Contact contact = new UnityModels.Contact
            {
                ContactId = contactId,
                Email = Contact.Email,
                Phone = Contact.Phone,
                DateOfBirth = Contact.DateOfBirth.ToString(),
                StreetAddress = Contact.Address1,
                StreetAddress2 = Contact.Address2,
                City = Contact.City,
                StateProvince = Contact.State,
                ZipPostalCode = Contact.PostalCode
            };


            var updateResult =  _customerProvider.SaveContact(contact);
            updateResult.Wait();

            ContactViewModel cvm = new ContactViewModel();
            string resultStatus = "";

            if (updateResult.Status == System.Threading.Tasks.TaskStatus.RanToCompletion)
            {
                cvm.ContactId = Contact.ContactId;
                cvm.DateOfBirth = Contact.DateOfBirth;
                cvm.Email = Contact.Email;
                cvm.FirstName = Contact.FirstName;
                cvm.LastName = Contact.LastName;
                cvm.PhotoUrl = Contact.PhotoUrl;
                resultStatus = "OK";

            }
            else
            {
                resultStatus = updateResult.Status.ToString();
            }

            UpdateResult<ContactViewModel> result = new UpdateResult<ContactViewModel>
            {
                Status = resultStatus,
                UpdatedRecord = cvm
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
                //TODO: We should be able to get all of the contact data in one call
                //We need to get additional information for each contact from the customer service
                var customerSearchResult = _customerProvider.GetCustomer(csi.ContactId, "");

                customerSearchResult.Wait();

                string maskedCardNumber = csi.MediaIdentification.ChipId.ToString();

                if (!String.IsNullOrEmpty(maskedCardNumber))
                {
                    string cardNumberString = csi.MediaIdentification.ChipId.ToString();
                    maskedCardNumber = "XXXX-" + cardNumberString.Substring(cardNumberString.Length - 4);
                }

                ContactViewModel cvm = new ContactViewModel
                {
                    ContactId = csi.ContactId.GetRecId(String.Empty, UnityModels.InfoSourceType.Master),
                    ParentContactId = csi.PrimaryContactId.Ids[0].InfoRecId,
                    FirstName = csi.FirstName,
                    LastName = csi.LastName,
                    DateOfBirth = DateTime.Parse(customerSearchResult.Result.Contacts[0].DateOfBirth),
                    OrderArrivalDate = csi.OrderArrivalDate,
                    Email = customerSearchResult.Result.Contacts[0].Email,
                    Phone = customerSearchResult.Result.Contacts[0].Phone,
                    Address1 = customerSearchResult.Result.Contacts[0].StreetAddress,
                    Address2 = customerSearchResult.Result.Contacts[0].StreetAddress2,
                    City = customerSearchResult.Result.Contacts[0].City,
                    State = customerSearchResult.Result.Contacts[0].StateProvince,
                    PostalCode = customerSearchResult.Result.Contacts[0].ZipPostalCode,
                    CardNumber = maskedCardNumber
                };

                contactSearchResults.Results.Add(cvm);

            }

            return contactSearchResults;
        }


    }
}
