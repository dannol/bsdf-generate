using Microsoft.AspNetCore.Mvc;
using ResortTools.SelfReg.ViewModels;
using ResortTools.SelfReg.Interfaces;
using System.Collections.Generic;

namespace ResortTools.SelfReg.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;
        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        // Get contacts based on an an account number
        // GET: api/contact/orderid/123
        [HttpGet("orderid/{orderId}")]
        public ActionResult SearchByOrderId(string orderId)
        {
            return new JsonResult(_contactService.GetByOrderId(orderId));

        }

        // Get a contact by their account ID
        // GET: api/contact/accountId/123
        [HttpGet("accountid/{accountId}")]
        public ActionResult SearchByAccountId(int accountId)
        {

            return new JsonResult(_contactService.GetByAccountId(accountId));

        }

        // Get contacts based on personal information
        // GET: api/contact/firstname/John/lastname/Smith
        [HttpGet("firstname/{firstName}/lastname/{lastName}")]
        public ActionResult SearchByPersonalInfo(string firstName, string lastName)
        {
            ContactModelView model = new ContactModelView();
            model.FirstName = firstName;
            model.LastName = lastName;

            return new JsonResult(_contactService.GetByPersinalInfo(model));

        }

        // Get all contacts associate to a given account ID
        // GET: api/accountid/123
        [HttpGet("{accountId}/group")]
        public ActionResult GetGroupByAccountId(int accountId)
        {
            return new JsonResult(_contactService.GetGroupByAccountId(accountId));
        }

        // Add a new contact
        // POST: api/contact
        [HttpPost]
        public ActionResult Add([FromBody] ContactModelView contact)
        {
            return new JsonResult(_contactService.AddContact(contact));
        }

        // Update a new contact
        // POST: api/contact/update
        [HttpPost("update")]
        public ActionResult Update([FromBody] ContactModelView contact)
        {
            return new JsonResult(_contactService.AddContact(contact));
        }

        // Add a new group member to an account
        // POST: api/contact/123/addgroupmember
        [HttpPost("{accountId}/addgroupmember")]
        public ActionResult Post([FromBody] ContactModelView member, int accountId)
        {
            return new JsonResult(_contactService.AddGroupMember(member, accountId));
        }

        // PUT: api/Test/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        // DELETE: api/ApiWithActions/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
