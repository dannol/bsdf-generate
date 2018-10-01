using System;

namespace ResortTools.SelfReg.ViewModels
{

    public class Contact
    {
        private DateTime? _dateOfBirth;
        private int? _age;

        public int? AccountId { get; set; }
        public String LastName { get; set; }
        public String FirstName { get; set; }
        public String Email { get; set; }
        public String Phone { get; set; }
        public String Hometown { get; set; }
        public DateTime? OrderArrivalDate { get; set; }
        public String CardNumber { get; set; }
        public String PhotoUrl { get; set; }
        public DateTime? DateOfBirth
        {
            get { return _dateOfBirth; }
            set
            {
                _dateOfBirth = value;
                if (_dateOfBirth != null)
                {
                    DateTime today = DateTime.Today;
                    _age = today.Year - _dateOfBirth.Value.Year;
                    // Go back to the year the person was born in case of a leap year
                    if (_dateOfBirth > today.AddYears(-_age.Value)) _age--;
                }
            }
        }
        public int? Age
        {
            get { return _age; }
            set { _age = value; }
        }

    }

}
