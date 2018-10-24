﻿using Microsoft.AspNetCore.Mvc;
using ResortTools.SelfReg.Models;
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

        // Get contacts based on an an account number or order ID
        // GET: api/contact/orderid/123
        [HttpGet("orderid/{orderId}/terminalid/{terminalId}")]
        public ActionResult SearchByOrderId(string orderId, string terminalId)
        {
            int terminalClientCode = int.Parse(terminalId);
            return new JsonResult(_contactService.GetByOrderId(orderId, terminalClientCode));

        }

        // Get a contact by their card Number
        // GET: api/contact/cardNumber/123
        [HttpGet("cardnumber/{cardnumber}/terminalId/{terminalId}")]
        public ActionResult SearchByCardNumber(string cardNumber, string terminalId)
        {
            int terminalClientCode = int.Parse(terminalId);

            return new JsonResult(_contactService.GetByCardNumber(cardNumber, terminalClientCode));
        }

        // Get contacts based on personal information
        // GET: api/contact/firstname/John/lastname/Smith/dob/2010-12-21/terminalid/12345
        [HttpGet("firstname/{firstName}/lastname/{lastName}/dob/{dateOfBirth}/terminalid/{terminalId}")]
        public ActionResult SearchByPersonalInfo(string firstName, string lastName, string dateOfBirth, string terminalId)
        {
            //TODO: Create a date helper
            var dobArray = dateOfBirth.Split('-');
            int terminalClientCode = int.Parse(terminalId);

            Contact model = new Contact();
            model.FirstName = firstName;
            model.LastName = lastName;
            model.DateOfBirth = DateTime.Parse(dateOfBirth);

            return new JsonResult(_contactService.GetByPersonalInfo(model, terminalClientCode));

        }

        // Get all contacts associate to a given account ID
        // GET: api/contact/contactid/123
        [HttpGet("{contactId}/group/terminalid/{terminalId}")]
        public ActionResult GetGroupByContactId(int contactId, int terminalId)
        {
            return new JsonResult(_contactService.GetGroupByContactId(contactId, terminalId));
        }

        // Add a new contact
        // POST: api/contact/add
        [HttpPost("add")]
        public ActionResult Add([FromBody] Contact contact)
        {
            return new JsonResult(_contactService.AddContact(contact));
        }

        // Update a new contact
        // POST: api/contact/update/terminalid/12345
        [HttpPost("update/terminalid/{terminalId}")]
        public ActionResult Update([FromBody] Contact contact, string terminalId)
        {
            return new JsonResult(_contactService.UpdateContact(contact));
        }

        // Add a new group member to an account
        // POST: api/contact/123/addgroupmember
        [HttpPost("{contactId}/addgroupmember")]
        public ActionResult Post([FromBody] Contact member, string contactId)
        {
            member.ParentContactId = contactId;
            return new JsonResult(_contactService.AddGroupMember(member));
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
