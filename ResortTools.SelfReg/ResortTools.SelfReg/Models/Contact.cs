using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResortTools.SelfReg.Models
{
    public class Contact
    {

        public int ParentAccountId { get; set; }
        public int? AccountId { get; set; }
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
