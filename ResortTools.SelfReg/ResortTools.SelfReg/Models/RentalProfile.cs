using System;
using System.Collections.Generic;
using UnityAPI.Models;

namespace ResortTools.SelfReg.Models
{
    public class RentalProfile
    {
        public int ContactId { get; set; }
        public string Ability { get; set; }
        public string Age { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
        public string ShoeSize { get; set; }
    }
}
