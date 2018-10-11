using ResortTools.SelfReg.Models;
using System;


namespace ResortTools.SelfReg.ViewModels
{

    public class ContactViewModel : Contact
    {
        private int? _age;
        public String Hometown { get; set; }

        public int? Age
        {
            get {
                if (DateOfBirth != null)
                {
                    DateTime today = DateTime.Today;
                    _age = today.Year - DateOfBirth.Value.Year;
                    // Go back to the year the person was born in case of a leap year
                    if (DateOfBirth > today.AddYears(-_age.Value)) _age--;
                }

                return _age;

            }
            set
            {
                _age = value;
            }

        }

    }

}
