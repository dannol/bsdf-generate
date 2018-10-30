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
    public class RentalService : IRentalService
    {
        private readonly IContactProvider _contactProvider;
        private readonly ICustomerProvider _customerProvider;

        public RentalService(IContactProvider contactProvider, ICustomerProvider customerProvider)
        {
            _contactProvider = contactProvider;
            _customerProvider = customerProvider;
        }

        public SearchResult<RentalProfileViewModel> GetByContact(Contact Renter, int TerminalId)
        {

            var profileSearchResult = _customerProvider.GetRentalProfile(Renter.ContactId);

            profileSearchResult.Wait();

            SearchResult<RentalProfileViewModel> searchResults = new SearchResult<RentalProfileViewModel>();

            if (profileSearchResult.Result != null)
            {

                RentalProfileViewModel rpvm = new RentalProfileViewModel
                {
                    //For some reason, the Contact ID is not included in the results
                    ContactId = Int32.Parse(Renter.ContactId),
                    RenterFirstName = Renter.FirstName,
                    RenterLastName = Renter.LastName,
                    Ability = profileSearchResult.Result.SelectedAbility,
                    Age = profileSearchResult.Result.SelectedAge,
                    Height = profileSearchResult.Result.SelectedHeight,
                    Weight = profileSearchResult.Result.SelectedWeight,
                    ShoeSize = profileSearchResult.Result.ShoeSize
                };

                searchResults.Results.Add(rpvm);

            }

            return searchResults;
        }

        // * AddContact
        // * This function adds a new contact
        public UpdateResult<RentalProfile> AddRentalProfile(RentalProfile profile, int TerminalID)
        {
            //TODO: Request Doesn't take terminal ID
            //CreateContactRequest createContactRequest = new CreateContactRequest
            //{
            //    Contact = new UnityModels.Contact
            //    {
            //        Active = true,
            //        IsCustomerPrimary = true,
            //        FirstName = Contact.FirstName,
            //        LastName = Contact.LastName,
            //        DateOfBirth = Contact.DateOfBirth.ToString(),
            //        Email = Contact.Email,
            //        Phone = Contact.Phone,
            //        StreetAddress = Contact.Address1,
            //        StreetAddress2 = Contact.Address2,
            //        City = Contact.City,
            //        StateProvince = Contact.State,
            //        ZipPostalCode = Contact.PostalCode                 
            //    }
            //};

            //var addResult = _customerProvider.CreateContact(createContactRequest);

            //addResult.Wait();

            //ContactViewModel cvm = new ContactViewModel();
            //string resultStatus = "";

            //if (addResult.Status == System.Threading.Tasks.TaskStatus.RanToCompletion)
            //{
            //    cvm.ContactId = addResult.Result.ContactId.GetRecId(String.Empty, UnityModels.InfoSourceType.Master);
            //    cvm.DateOfBirth = Contact.DateOfBirth;
            //    cvm.Email = Contact.Email;
            //    cvm.FirstName = Contact.FirstName;
            //    cvm.LastName = Contact.LastName;
            //    cvm.PhotoUrl = "";
            //    resultStatus = "OK";
            //}
            //else
            //{
            //    resultStatus = addResult.Status.ToString();
            //}


            RentalProfileViewModel rpvm = new RentalProfileViewModel();

            UpdateResult<RentalProfile> results = new UpdateResult<RentalProfile>
            {
                Status = "OK",
                UpdatedRecord = rpvm
            };

            return results;
        }

 
        public UpdateResult<RentalProfile> UpdateRentalProfile(RentalProfile profile, int TerminalID)
        {
            //Create an identifier object based on the ID of the contact
            UnityModels.Identifier contactId = new UnityModels.Identifier(UnityModels.InfoSourceType.Master, profile.ContactId.ToString());

            //Create a Unity API Contact for updating
            //UnityModels.Contact contact = new UnityModels.Contact
            //{
            //    ContactId = contactId,
            //    FirstName = Contact.FirstName,
            //    LastName = Contact.LastName,
            //    Email = Contact.Email,
            //    Phone = Contact.Phone,
            //    DateOfBirth = Contact.DateOfBirth.ToString(),
            //    StreetAddress = Contact.Address1,
            //    StreetAddress2 = Contact.Address2,
            //    City = Contact.City,
            //    StateProvinceId = Contact.State,
            //    ZipPostalCode = Contact.PostalCode,
            //    Medication = Contact.Medication,
            //    DrugAllergy = Contact.DrugAllergy,
            //    FoodAllergy = Contact.FoodAllergy,
            //    SpecialCondition = Contact.SpecialCondition,
            //    PrimaryEmergencyContact = Contact.PrimaryContactName,
            //    PrimaryEmergencyPhone = Contact.PrimaryContactPhone,
            //    AlternateEmergencyContact1 = Contact.AlternateContactName,
            //    AlternateEmergencyPhone1 = Contact.AlternateContactPhone
            //};

            ////TODO: Request Doesn't take terminal ID
            //var updateResult = _customerProvider.SaveContact(contact);
            //updateResult.Wait();

            //RentalProfileViewModel rpvm = new RentalProfileViewModel();
            //string resultStatus = "OK";

            //if (updateResult.Status == System.Threading.Tasks.TaskStatus.RanToCompletion)
            //{
            //    cvm.ContactId = Contact.ContactId;
            //    cvm.DateOfBirth = Contact.DateOfBirth;
            //    cvm.Email = Contact.Email;
            //    cvm.FirstName = Contact.FirstName;
            //    cvm.LastName = Contact.LastName;
            //    cvm.PhotoUrl = Contact.PhotoUrl;
            //    resultStatus = "OK";

            //}
            //else
            //{
            //    resultStatus = updateResult.Status.ToString();
            //}

            RentalProfileViewModel rpvm = new RentalProfileViewModel();
            string resultStatus = "OK";

            UpdateResult<RentalProfile> result = new UpdateResult<RentalProfile>
            {
                Status = resultStatus,
                UpdatedRecord = rpvm
            };

            return result;
        }

        // * Load SearchResults
        // * This function is only used by the Contact Services and loads Unity API Contact search
        // * results into search results used by the Self Registration app.
        //private SearchResult<ContactViewModel> LoadSearchResults(IList<ContactSearchItem> unityContacts)
        //{
        //    SearchResult<ContactViewModel> contactSearchResults = new SearchResult<ContactViewModel>();

        //    foreach (ContactSearchItem csi in unityContacts)
        //    {

        //        string maskedCardNumber = csi.MediaIdentification.ChipId.ToString();

        //        if (!String.IsNullOrEmpty(maskedCardNumber))
        //        {
        //            string cardNumberString = csi.MediaIdentification.ChipId.ToString();
        //            maskedCardNumber = "XXXX-" + cardNumberString.Substring(cardNumberString.Length - 4);
        //        }

        //        ContactViewModel cvm = new ContactViewModel
        //        {
        //            ContactId = csi.ContactId.GetRecId(String.Empty, UnityModels.InfoSourceType.Master),
        //            ParentContactId = csi.PrimaryContactId.Ids[0].InfoRecId,
        //            FirstName = csi.FirstName,
        //            LastName = csi.LastName,
        //            DateOfBirth = csi.BirthDate,
        //            OrderArrivalDate = csi.OrderArrivalDate,
        //            Email = csi.EmailAddress,
        //            Phone = csi.Phone,
        //            Address1 = csi.StreetAddress,
        //            Address2 = csi.StreetAddress2,
        //            City = csi.City,
        //            State = csi.StateProvinceId,
        //            PostalCode = csi.ZipPostalCode,
        //            CardNumber = maskedCardNumber
        //        };

        //        contactSearchResults.Results.Add(cvm);

        //    }

        //    return contactSearchResults;
        //}


    }
}
