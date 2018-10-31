using ResortTools.SelfReg.ViewModels;
using ResortTools.SelfReg.Models;

namespace ResortTools.SelfReg.Interfaces
{
    public interface IRentalService
    {
        SearchResult<RentalProfileViewModel> GetByContact(Contact Rentere);
        UpdateResult<RentalProfile> AddRentalProfile(RentalProfile profile);
        UpdateResult<RentalProfile> UpdateRentalProfile(RentalProfile profile);
    }
}
