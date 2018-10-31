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
