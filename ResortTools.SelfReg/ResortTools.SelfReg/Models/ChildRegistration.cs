using System;
using System.Collections.Generic;
using UnityAPI.Models;

namespace ResortTools.SelfReg.Models
{
    public class ChildRegistration
    {
        public int ContactId { get; set; }
        public string Medications { get; set; }
        public string FoodAllergies { get; set; }
        public string DrugAllergies { get; set; }
        public string SpecialConditions { get; set; }
        public string PrimaryEmergencyContactPhone { get; set; }
        public string PrimaryEmergencyContactEmail { get; set; }
        public string SecondEmergencyContactPhone { get; set; }
        public string SecondEmergencyContactEmail { get; set; }
    }
}
