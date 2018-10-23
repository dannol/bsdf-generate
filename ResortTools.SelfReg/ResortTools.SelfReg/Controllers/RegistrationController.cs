using Microsoft.AspNetCore.Mvc;
using ResortTools.SelfReg.Models;
using ResortTools.SelfReg.Interfaces;
using System;

namespace ResortTools.SelfReg.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {

        private readonly IRegistrationService _registrationService;
        public RegistrationController(IRegistrationService registrationService)
        {
            _registrationService = registrationService;
        }


        // Get registration based on a contact ID
        // GET: api/registration//contactid/123/terminalid/1235
        [HttpGet("contactid/{contactId}/terminalid/{terminalId}")]
        public ActionResult GetByContactId(int contactId, int terminalId)
        {
            return new JsonResult(_registrationService.GetByContactId(contactId, terminalId));
        }

       
    }
}
