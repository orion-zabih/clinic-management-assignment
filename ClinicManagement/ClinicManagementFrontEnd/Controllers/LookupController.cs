using Microsoft.AspNetCore.Mvc;

namespace ClinicManagementFrontEnd.Controllers
{
    public class LookupController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
