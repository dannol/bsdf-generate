using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ResortTools.SelfReg.Models;

namespace ResortTools.SelfReg.Interfaces
{
    public interface IContactService
    {
        SearchResult<ContactModel> GetByOrderId(string OrderId);
        SearchResult<ContactModel> GetByPersinalInfo(ContactModel Contact);
        SearchResult<ContactModel> GetByAccountId(int AccountId);
        SearchResult<ContactModel> GetGroupByAccountId(int AccountId);
        UpdateResult<ContactModel> AddContact(ContactModel Contact);
        UpdateResult<ContactModel> AddGroupMember(ContactModel Member, int AccountId);

    }
}
