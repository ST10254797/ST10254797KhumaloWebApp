using Microsoft.AspNetCore.Mvc;

namespace KhumaloWebApp.Controllers
{
    public class CraftController : Controller
    {

        // Action method for displaying the MyWork page

        // Sample craftwork data
        [Route("/")]
        public IActionResult Index()
        {
            return View("Views/Home/Index.cshtml");
        }

        [Route("/about")]
        public IActionResult About()
        {
            return View("Views/About/About.cshtml");
        }

        [Route("/contact")]
        public IActionResult Contact()
        {
            return View("Views/Contact/Contact.cshtml");
        }

    }
}
