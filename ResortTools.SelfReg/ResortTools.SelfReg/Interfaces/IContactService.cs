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
        SearchResult<ContactModelView> GetByOrderId(string OrderId);
        SearchResult<ContactModelView> GetByPersinalInfo(ContactModelView Contact);
        SearchResult<ContactModelView> GetByAccountId(int AccountId);
        SearchResult<ContactModelView> GetGroupByAccountId(int AccountId);
        UpdateResult<ContactModelView> AddContact(ContactModelView Contact);
        UpdateResult<ContactModelView> AddGroupMember(ContactModelView Member, int AccountId);
        UpdateResult<ContactModelView> UpdateContact(ContactModelView Contact);

    }
}
