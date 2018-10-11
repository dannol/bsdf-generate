using System;

namespace ResortTools.SelfReg.Models
{
    public class ContactSearchResult<T> : SearchResult<T>
    {
       Boolean ShowGuestList { get; set; }
    }

}
