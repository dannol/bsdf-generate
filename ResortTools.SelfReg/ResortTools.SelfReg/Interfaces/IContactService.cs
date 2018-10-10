using ResortTools.SelfReg.ViewModels;
using ResortTools.SelfReg.Models;

namespace ResortTools.SelfReg.Interfaces
{
    public interface IContactService
    {
        SearchResult<ContactViewModel> GetByOrderId(string OrderId, int TerminalClientCode);
        SearchResult<ContactViewModel> GetByPersonalInfo(Contact Contact, int TerminalId);
        //SearchResult<ContactViewModel> GetByAccountId(string AccountId);
        SearchResult<ContactViewModel> GetByCardNumber(string CardNumber, int TerminalId);
        SearchResult<ContactViewModel> GetGroupByAccountId(int AccountId, int TerminalId);
        UpdateResult<ContactViewModel> AddContact(Contact Contact);
        UpdateResult<ContactViewModel> UpdateContact(Contact Contact);

    }
}
