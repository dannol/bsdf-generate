using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ResortTools.SelfReg.ViewModels;
using ResortTools.SelfReg.Models;

namespace ResortTools.SelfReg.Interfaces
{
    public interface IContactService
    {
        SearchResult<ContactViewModel> GetByOrderId(string OrderId);
        SearchResult<ContactViewModel> GetByPersinalInfo(Contact Contact);
        SearchResult<ContactViewModel> GetByAccountId(int AccountId);
        SearchResult<ContactViewModel> GetByCardNumber(string CardNumber);
        SearchResult<ContactViewModel> GetGroupByAccountId(int AccountId);
        UpdateResult<ContactViewModel> AddContact(Contact Contact);
        UpdateResult<ContactViewModel> UpdateContact(Contact Contact);

    }
}
