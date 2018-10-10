using System;
using UnityAPI.Models;

namespace ResortTools.SelfReg.Models
{
    public class Contact
    {

        public string ParentAccountId { get; set; }
        public string AccountId { get; set; }
        public String LastName { get; set; }
        public String FirstName { get; set; }
        public String Email { get; set; }
        public String Phone { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime? OrderArrivalDate { get; set; }
        public String CardNumber { get; set; }
        public String PhotoUrl { get; set; }

    }
}
