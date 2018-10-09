using Microsoft.AspNetCore.Mvc;
using ResortTools.SelfReg.Models;
using ResortTools.SelfReg.ViewModels;
using ResortTools.SelfReg.Interfaces;
using System;

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


        // Get a contact by their card Number
        // GET: api/contact/cardNumber/123
        [HttpGet("cardnumber/{cardnumber}")]
        public ActionResult SearchByCardNumber(string cardNumber)
        {
            return new JsonResult(_contactService.GetByCardNumber(cardNumber));
        }

        // Get contacts based on personal information
        // GET: api/contact/firstname/John/lastname/Smith/dob/2010-12-21
        [HttpGet("firstname/{firstName}/lastname/{lastName}/dob/{dateOfBirth}")]
        public ActionResult SearchByPersonalInfo(string firstName, string lastName, string dateOfBirth)
        {
            //TODO: Create a date helper
            var dobArray = dateOfBirth.Split('-'); 

            Contact model = new Contact();
            model.FirstName = firstName;
            model.LastName = lastName;
            model.DateOfBirth = DateTime.Parse(dateOfBirth);

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
        // POST: api/contact/add
        [HttpPost("add")]
        public ActionResult Add([FromBody] Contact contact)
        {
            return new JsonResult(_contactService.AddContact(contact));
        }

        // Update a new contact
        // POST: api/contact/update
        [HttpPost("update")]
        public ActionResult Update([FromBody] Contact contact)
        {
            return new JsonResult(_contactService.UpdateContact(contact));
        }

        // Add a new group member to an account
        // POST: api/contact/123/addgroupmember
        [HttpPost("{accountId}/addgroupmember")]
        public ActionResult Post([FromBody] Contact member)
        {
            return new JsonResult(_contactService.AddContact(member));
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
