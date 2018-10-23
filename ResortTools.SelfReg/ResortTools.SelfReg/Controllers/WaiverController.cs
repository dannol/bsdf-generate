using Microsoft.AspNetCore.Mvc;
using ResortTools.SelfReg.Models;
using ResortTools.SelfReg.Interfaces;
using System;

namespace ResortTools.SelfReg.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WaiverController : ControllerBase
    {

        private readonly IWaiverService _waiverService;
        public WaiverController(IWaiverService waiverService)
        {
            _waiverService = waiverService;
        }

        // Get waiver based on an an authorization code
        // GET: api/waiver/authcode/12345/terminalid/54321
        [HttpGet("authCode/{authCode}/terminalId/{terminalId}")]
        public ActionResult GetByAuthCode(string authCode, string terminalId)
        {
            int terminalClientCode = int.Parse(terminalId);
            return new JsonResult(_waiverService.GetByAuthCode(authCode, terminalClientCode));
        }

        // POST: api/waiver/sign
        [HttpPost("sign/terminalId/{terminalId}")]
        public ActionResult Post([FromBody] Waiver updateWaiver, string terminalId)
        {
            int terminalClientCode = int.Parse(terminalId);
            return new JsonResult(_waiverService.AddWaiver(updateWaiver, terminalClientCode));
        }

    }
}
