using ResortTools.SelfReg.Models;
using System;
using System.Collections.Generic;
using UnityModels = UnityAPI.Models;


namespace ResortTools.SelfReg.ViewModels
{

    public class RentalProfileViewModel : RentalProfile
    {
        public string RenterFirstName { get; set; }
        public string RenterLastName { get; set; }

        //The Following Lists are needed because the possible values for each property
        //are returned from Unity with each profile.
        //TODO: SRK-71 - In the future there should be a separate call to get the possible values and this
        //data should not be a part of each profile call
        public List<UnityModels.RentalProfileRefItem> Abilities { get; set; }
        public List<UnityModels.RentalProfileRefItem> Ages { get; set; }
        public List<UnityModels.RentalProfileRefItem> Heights { get; set; }
        public List<UnityModels.RentalProfileRefItem> Weights { get; set; }

    }

}
