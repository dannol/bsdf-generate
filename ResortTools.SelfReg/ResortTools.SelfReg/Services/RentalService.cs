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

        public SearchResult<RentalProfileViewModel> GetByContact(Contact Renter)
        {

            var profileSearchResult = _customerProvider.GetRentalProfile(Renter.ContactId);

            profileSearchResult.Wait();

            SearchResult<RentalProfileViewModel> searchResults = new SearchResult<RentalProfileViewModel>();

            if (profileSearchResult.Result != null)
            {

                //The following loading of Dictionary items is needed because the possible values for each property
                //are returned from Unity with each profile.
                //TODO: SRK-71 -In the future there should be a separate call to get the possible values and this
                //data should not be a part of each profile call             
                Dictionary<int, string> Weights = new Dictionary<int, string>();
                foreach (UnityModels.RentalProfileRefItem weightRef in profileSearchResult.Result.WeightRefs)
                {
                    Weights.Add(weightRef.ListKey, weightRef.ListDescription);
                }

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
                    ShoeSize = profileSearchResult.Result.ShoeSize,
                    //The following properties are needed because the possible values for each property
                    //are returned from Unity with each profile.
                    //TODO: SRK-71 -In the future there should be a separate call to get the possible values and this
                    //data should not be a part of each profile call    
                    Abilities = profileSearchResult.Result.AbilityRefs,
                    Ages = profileSearchResult.Result.AgeRefs,
                    Heights = profileSearchResult.Result.HeightRefs,
                    Weights = profileSearchResult.Result.WeightRefs
                };

                searchResults.Results.Add(rpvm);

            }

            return searchResults;
        }

        // * AddContact
        // * This function adds a new contact
        public UpdateResult<RentalProfile> AddRentalProfile(RentalProfile profile)
        {
            //TODO: Need to Actually Add a rental profile
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

 
        public UpdateResult<RentalProfile> UpdateRentalProfile(RentalProfile profile)
        {
            //Create an identifier object based on the ID of the contact
            UnityModels.Identifier contactId = new UnityModels.Identifier(UnityModels.InfoSourceType.Master, profile.ContactId.ToString());
          
            //TODO: Unity returns a void for this methid.  How do we know that it was successful?
            _customerProvider.SaveRentalProfile(profile.ContactId.ToString(), profile.ShoeSize, profile.Age, profile.Ability, profile.Height, profile.Weight,"") ;
        
            //TODO: Should return a real status
            string resultStatus = "OK";

            UpdateResult<RentalProfile> result = new UpdateResult<RentalProfile>
            {
                Status = resultStatus,
                UpdatedRecord = null
            };

            return result;
        }

    }
}
