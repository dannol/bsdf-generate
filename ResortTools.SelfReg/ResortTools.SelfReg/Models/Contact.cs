using System;
using UnityAPI.Models;

namespace ResortTools.SelfReg.Models
{
    public class Contact
    {

        public string ParentContactId { get; set; }
        public string ContactId { get; set; }
        public String LastName { get; set; }
        public String FirstName { get; set; }
        public String Email { get; set; }
        public String Phone { get; set; }
        public String Address1 { get; set; }
        public String Address2 { get; set; }
        public String City { get; set; }
        public String State { get; set; }
        public String PostalCode { get; set; }

        public DateTime? DateOfBirth { get; set; }
        public DateTime? OrderArrivalDate { get; set; }
        public String CardNumber { get; set; }
        public String PhotoUrl { get; set; }
        
        // Registration Information
        public string AlternateContactName { get; set; }
        public string AlternateContactPhone { get; set; }
        public string DrugAllergy { get; set; }
        public string FoodAllergy { get; set; }
        public string Medication { get; set; }
        public string PrimaryContactName { get; set; }
        public string PrimaryContactPhone { get; set; }
        public string SpecialCondition { get; set; }


    }
}
