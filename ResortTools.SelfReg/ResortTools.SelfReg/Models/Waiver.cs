using System;
using System.Collections.Generic;
using UnityAPI.Models;

namespace ResortTools.SelfReg.Models
{
    public class Waiver
    {
        public Contact Signer { get; set; }
        public List<Contact> Minors { get; set; }
        public string WaiverText { get; set; }
        public string SignatureString { get; set; }
        public string SignatureBase64String { get; set; }
    }
}
