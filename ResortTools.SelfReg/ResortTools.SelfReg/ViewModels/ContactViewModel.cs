using ResortTools.SelfReg.Models;
using System;


namespace ResortTools.SelfReg.ViewModels
{

    public class ContactViewModel : Contact
    {
        private int? _age;
        private string _displayOrderArrivalDate;
        private string _hometown;

        public String Hometown
        {
            get
            {
                //Build a friendly hometown string for display
                if (!String.IsNullOrEmpty(City))
                {
                    return City + ",  " + State;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                _hometown = value;
            }
        }
        public String DisplayOrderArrivalDate
        {
            get
            {
                //Build a friendly Order Arrival date format for display
                //TODO: Need to utilize a localized format string
                if (OrderArrivalDate != null && OrderArrivalDate.Value != DateTime.MinValue)
                {
                    return OrderArrivalDate.Value.ToString("MM/dd/yyyy");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                _displayOrderArrivalDate = value;
            }
        }

        public int? Age
        {
            get {
                //Set age based on DOB
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
