using ResortTools.SelfReg.ViewModels;
using ResortTools.SelfReg.Models;

namespace ResortTools.SelfReg.Interfaces
{
    public interface IRegistrationService
    {
        SearchResult<ChildRegistration> GetByContactId(int ContactId, int TerminalClientCode);
        //UpdateResult<ChildRegistration> AddRegistration(ChildRegistration Registration);
        //UpdateResult<ChildRegistration> UpdateRegistration(ChildRegistration Registration);

    }
}
