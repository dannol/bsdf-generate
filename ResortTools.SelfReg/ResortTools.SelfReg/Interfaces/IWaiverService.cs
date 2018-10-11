using ResortTools.SelfReg.ViewModels;
using ResortTools.SelfReg.Models;

namespace ResortTools.SelfReg.Interfaces
{
    public interface IWaiverService
    {
        SearchResult<WaiverViewModel> GetByAuthCode(string AuthCode, int TerminalClientCode);
    }
}
