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
    public class RegistrationServiceMock : IRegistrationService
    {
        private readonly IContactProvider _contactProvider;

        public RegistrationServiceMock(IContactProvider contactProvider)
        {
            _contactProvider = contactProvider;
        }

        public SearchResult<ChildRegistration> GetByContactId(int ContactId, int TerminalClientCode)
        {

            SearchResult<ChildRegistration> result = new SearchResult<ChildRegistration>();

            List<ChildRegistration> registrations = new List<ChildRegistration>();
            ChildRegistration reg = new ChildRegistration
            {
                //ContactId = ContactId,
                ContactId = ContactId,
                Medications = "Pills",
                FoodAllergies = "nuts",
                DrugAllergies = "Big Pills",
                SpecialConditions = "arachnaphobia",               
            };
            registrations.Add(reg);


            result.Results = registrations;

            return result;
        }
        
    }
}
