using Microsoft.AspNetCore.Mvc;
using ResortTools.SelfReg.Models;
using ResortTools.SelfReg.ViewModels;
using System.Collections.Generic;

namespace ResortTools.SelfReg.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        // GET: api/contact/firstname/John/lastname/Smith
        //[HttpGet("accountid/{accountId}")]
        //public ActionResult SearchByAccountId(int accountId)
        //{
        //    SearchResult<ContactModelView> result = new SearchResult<ContactModelView>();

        //    List<ContactModelView> contacts = new List<ContactModelView>();
        //    ContactModelView Wife = new ContactModelView();
        //    Wife.FirstName = "Wife";
        //    Wife.LastName = "Smith";
        //    Wife.Hometown = "Colorado Springs, CO";
        //    Wife.OrderArrivalDate = null;
        //    Wife.CardNumber = "xxxxxxxx-123";
        //    Wife.PhotoUrl = "/images/Wife.jpg";
        //    contacts.Add(Wife);
        //    ContactModelView Steve = new ContactModelView();
        //    Steve.FirstName = "Child1";
        //    Steve.LastName = "Smith";
        //    Steve.Hometown = "Boise, ID";
        //    Steve.OrderArrivalDate = "April 4, 2019";
        //    Steve.CardNumber = null;
        //    Steve.PhotoUrl = "/images/Steve.jpg";
        //    contacts.Add(Steve);
        //    ContactModelView William = new ContactModelView();
        //    William.FirstName = "Child2";
        //    William.LastName = "Smith";
        //    William.Hometown = null;
        //    William.OrderArrivalDate = null;
        //    William.CardNumber = null;
        //    William.PhotoUrl = "/images/William.jpg";
        //    contacts.Add(William);

        //    result.Results = contacts;

        //    return new JsonResult(result);

        //}

        // POST: api/Group
        [HttpPost("addMember/firstname/{firstName}/lastname/{lastname}")]
        public void Post([FromBody] string firstName, string lastName)
        {
            Contact newContact = new Contact();
            newContact.FirstName = firstName;
            newContact.LastName = lastName;
        }

        // PUT: api/Group/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
