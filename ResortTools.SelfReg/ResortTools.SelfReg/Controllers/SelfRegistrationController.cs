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

        public IActionResult RentalOffice()
        {
            ViewData["Message"] = "Self Registration Tool";

            return View(SelfRegistrationConfig);
        }


        public IActionResult PassOffice()
        {
            ViewData["Message"] = "Self Registration Tool";

            return View(SelfRegistrationConfig);
        }

        public IActionResult SkiSchool()
        {
            ViewData["Message"] = "Self Registration Tool";

            return View(SelfRegistrationConfig);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
