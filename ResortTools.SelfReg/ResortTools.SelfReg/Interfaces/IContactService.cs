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
        SearchResult<Contact> GetByOrderId(string OrderId);
        SearchResult<Contact> GetByPersinalInfo(Contact Contact);
        SearchResult<Contact> GetByAccountId(int AccountId);
        SearchResult<Contact> GetByCardNumber(string CardNumber);
        SearchResult<Contact> GetGroupByAccountId(int AccountId);
        UpdateResult<Contact> AddContact(Contact Contact);
        UpdateResult<Contact> UpdateContact(Contact Contact);

    }
}
