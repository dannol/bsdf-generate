using Microsoft.AspNetCore.Mvc;
using ResortTools.SelfReg.Models;
using System.Diagnostics;

namespace ResortTools.SelfReg.Controllers
{
    public class SelfRegistrationController : Controller
    {
        SelfRegistrationConfig SelfRegistrationConfig { get; }

        //Constructor
        public SelfRegistrationController(SelfRegistrationConfig selfRegistrationConfig)
        {
            SelfRegistrationConfig = selfRegistrationConfig;
        }

        public IActionResult Index()
        {
            return View();
        }

        // Pass Office
        // The id parm will automatically get the first node in the URL which should be the Terminal ID
        // SelfRegistration/PassOffice/12345
        public IActionResult PassOffice(string id)
        {
            ViewData["Message"] = "Self Registration Tool";

            if (string.IsNullOrEmpty(id))
            {
                id = SelfRegistrationConfig.PassOfficeConfig.DefaultTerminalId;
            }

            SelfRegistrationConfig.TerminalId = id;

            return View(SelfRegistrationConfig);
        }

        // Rental Office
        // The id parm will automatically get the first node in the URL which should be the Terminal ID
        // SelfRegistration/RentalOffice/12345
        public IActionResult RentalOffice(string id)
        {
            ViewData["Message"] = "Self Registration Tool";

            if (string.IsNullOrEmpty(id))
            {
                id = SelfRegistrationConfig.RentalOfficeConfig.DefaultTerminalId;
            }

            SelfRegistrationConfig.TerminalId = id;
            return View(SelfRegistrationConfig);
        }

        // Ski School
        // The id parm will automatically get the first node in the URL which should be the Terminal ID
        // SelfRegistration/SkiSchool/12345
        public IActionResult SkiSchool(string id)
        {
            ViewData["Message"] = "Self Registration Tool";

            if (string.IsNullOrEmpty(id))
            {
                id = SelfRegistrationConfig.SkiSchoolConfig.DefaultTerminalId;
            }
            return View(SelfRegistrationConfig);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
