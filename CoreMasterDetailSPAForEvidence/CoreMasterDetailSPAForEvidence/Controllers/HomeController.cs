using Microsoft.AspNetCore.Mvc;

namespace CoreMasterDetailSPAForEvidence.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
