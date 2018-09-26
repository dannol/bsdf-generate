using System;

namespace ResortTools.SelfReg.ViewModels
{

    public class ContactModelView
    {
        public int AccountId { get; set; }
        public String LastName { get; set; }
        public String FirstName { get; set; }
        public String Hometown { get; set; }
        public DateTime? OrderArrivalDate { get; set; }
        public String CardNumber { get; set; }
        public String PhotoUrl { get; set; }
        public DateTime? DateOfBirth { get; set; }

    }
    
}
