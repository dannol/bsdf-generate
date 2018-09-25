using System.Collections.Generic;
using ResortTools.SelfReg.Interfaces;

namespace ResortTools.SelfReg.Models
{
    public class SearchResult<T> : IResult<T>
    {
        public string Status { get; set; }
        public List<string> Messages { get; set; }

        public List<T> Results { get; set; }

        public SearchResult()
        {
            Messages = new List<string>();
            Results = new List<T>();
        }
    }

}
