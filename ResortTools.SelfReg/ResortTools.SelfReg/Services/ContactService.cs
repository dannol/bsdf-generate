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

        // * GetGroupByContactID
        // * This function retrieves all group members for a given contact ID
        public SearchResult<ContactViewModel> GetGroupByContactId(int ContactId, int TerminalId)
        {

            SearchResult<ContactViewModel> result = new SearchResult<ContactViewModel>();

            //Create an identifier object based on the ID of the contact
            UnityModels.Identifier contactId = new UnityModels.Identifier(UnityModels.InfoSourceType.Master, ContactId.ToString());

            var customerSearchResult = _customerProvider.GetCustomer(contactId, "");

            customerSearchResult.Wait();

            foreach (UnityModels.Contact contact in customerSearchResult.Result.Contacts)
            {
                string hometown = String.IsNullOrEmpty(contact.City) ? "" : contact.City + ", ";
                hometown += String.IsNullOrEmpty(contact.StateProvince) ? "" : contact.StateProvince;
                ContactViewModel cvm = new ContactViewModel
                {
                    ContactId = contact.ContactId.GetRecId(String.Empty, UnityModels.InfoSourceType.Master),
                    FirstName = contact.FirstName,
                    LastName = contact.LastName,
                    Hometown = hometown,
                    PhotoUrl = String.IsNullOrEmpty(contact.PhotoUrl) ? "" : contact.PhotoUrl,
                    //Default DOB to today if not present
                    DateOfBirth = DateTime.Parse(String.IsNullOrEmpty(contact.DateOfBirth) ? DateTime.Today.ToShortDateString() : contact.DateOfBirth)
                };

                result.Results.Add(cvm);
            }

            return result;
        }

        public UpdateResult<ContactViewModel> AddContact(Contact Contact)
        {
            CreateContactRequest createContactRequest = new CreateContactRequest
            {
                Contact = new UnityModels.Contact
                {
                    Active = true,
                    IsCustomerPrimary = true,
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

        public UpdateResult<ContactViewModel> AddGroupMember(Contact GroupMember)
        {
            //Create an identifier object based on the Parent ID of the contact
            UnityModels.Identifier contactId = new UnityModels.Identifier(UnityModels.InfoSourceType.Master, GroupMember.ParentContactId);

            UnityModels.Contact newGroupMember = new UnityModels.Contact { 
                    Active = true,
                    IsCustomerPrimary = false,
                    FirstName = GroupMember.FirstName,
                    LastName = GroupMember.LastName,
                    DateOfBirth = GroupMember.DateOfBirth.ToString(),
                    Email = GroupMember.Email,
                    Phone = GroupMember.Phone,
                    StreetAddress = GroupMember.Address1,
                    StreetAddress2 = GroupMember.Address2,
                    City = GroupMember.City,
                    StateProvince = GroupMember.State,
                    ZipPostalCode = GroupMember.PostalCode
            };

            var addResult = _customerProvider.AddDependentContact(contactId, newGroupMember);

            addResult.Wait();

            ContactViewModel cvm = new ContactViewModel();
            string resultStatus = "";

            if (addResult.Status == System.Threading.Tasks.TaskStatus.RanToCompletion)
            {
                cvm.ContactId = addResult.Result.ContactId.GetRecId(String.Empty, UnityModels.InfoSourceType.Master);
                cvm.DateOfBirth = DateTime.Parse(addResult.Result.DateOfBirth);
                cvm.Email = addResult.Result.Email;
                cvm.FirstName = addResult.Result.FirstName;
                cvm.LastName = addResult.Result.LastName;
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
                Active = true,
                IsCustomerPrimary = true,
                ContactId = contactId,
                FirstName = Contact.FirstName,
                LastName = Contact.LastName,
                Email = Contact.Email,
                Phone = Contact.Phone,
                DateOfBirth = Contact.DateOfBirth.ToString(),
                StreetAddress = Contact.Address1,
                StreetAddress2 = Contact.Address2,
                City = Contact.City,
                StateProvinceId = Contact.State,
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
