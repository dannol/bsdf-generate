using System.Collections.Generic;
using ResortTools.SelfReg.Interfaces;

namespace ResortTools.SelfReg.Models
{
    public class UpdateResult<T> : IResult<T>
    {
        public string Status { get; set; }
        public List<string> Messages { get; set; }

        public int RecordId { get; set; }

        public UpdateResult()
        {
            Messages = new List<string>();
        }
    }
}
