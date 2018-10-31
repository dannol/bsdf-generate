using ResortTools.SelfReg.ViewModels;
using ResortTools.SelfReg.Models;

namespace ResortTools.SelfReg.Interfaces
{
    public interface IRentalService
    {
        SearchResult<RentalProfileViewModel> GetByContact(Contact Renter, int TerminalClientCode);
        UpdateResult<RentalProfile> AddRentalProfile(RentalProfile profile, int TerminalClientCode);
        UpdateResult<RentalProfile> UpdateRentalProfile(RentalProfile profile);
    }
}
