using System.Collections.Generic;

namespace ResortTools.SelfReg.Interfaces
{
    public interface IResult<T>
    {
        string Status { get; set; }
        List<string> Messages { get; set; }
    }
}
