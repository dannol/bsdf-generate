using Microsoft.AspNetCore.Mvc;
using ResortTools.SelfReg.Models;
using ResortTools.SelfReg.Interfaces;
using System;

namespace ResortTools.SelfReg.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalController : ControllerBase
    {

        private readonly IRentalService _rentalService;
        public RentalController(IRentalService rentalService)
        {
            _rentalService = rentalService;
        }


        // POST: api/rental/getcontactprofile/terminalId/12345
        [HttpPost("getcontactprofile/terminalId/{terminalId}")]
        public ActionResult Post([FromBody] Contact renter, string terminalId)
        {
            int terminalClientCode = int.Parse(terminalId);
            return new JsonResult(_rentalService.GetByContact(renter, terminalClientCode));
        }


        // Get contacts based on personal information
        // GET: api/rental/firstname/John/lastname/Smith/dob/2010-12-21/terminalid/12345
        [HttpGet("contactid/{contactId}/firstname/{firstName}/lastname/{lastName}/terminalid/{terminalId}")]
        public ActionResult GetByContactInfo(string contactId, string firstName, string lastName, string terminalId)
        {
            int terminalClientCode = int.Parse(terminalId);
            Contact renter = new Contact
            {
                ContactId = contactId,
                FirstName = firstName,
                LastName = lastName
            };

            return new JsonResult(_rentalService.GetByContact(renter, terminalClientCode));
        }

        // Save a profile
        // POST: api/rental/update
        [HttpPost("saveprofile")]
        public ActionResult Update([FromBody] RentalProfile profile)
        {
            return new JsonResult(_rentalService.UpdateRentalProfile(profile));
        }
    }
}
