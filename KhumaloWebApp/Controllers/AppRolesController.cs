using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace KhumaloWebApp.Controllers
{
   
    public class AppRolesController : Controller

    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public AppRolesController(RoleManager<IdentityRole>roleManager)
        {
            _roleManager = roleManager;
        }

        [Authorize(Roles = "Admin")]
        // list all the roles created by the user
        public IActionResult Index()
        {

            var roles = _roleManager.Roles;
            return View(roles);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Create()
        {
             return View(); 
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(IdentityRole model)
        {

            // avoid duplicate role

            if(!_roleManager.RoleExistsAsync(model.Name).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(model.Name)).GetAwaiter().GetResult();
            }

            return RedirectToAction ("Index");
        }
    }
}
