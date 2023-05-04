using Final_Project.Data;
using Final_Project.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Final_Project.Controllers
{
    [Authorize(Roles ="ADMIN")]
    public class RolesController : Controller
    {
        private readonly ILogger<RolesController> _logger;
        RoleManager<IdentityRole> _roleManager;
        UserManager<IdentityUser> _userManager;
        ApplicationDbContext _context;
        public RolesController(ILogger<RolesController> logger, RoleManager<IdentityRole> roleManager, ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _roleManager = roleManager;
            _userManager = userManager;
            _context = context;
        }
        public IActionResult Index()
        {

            return View(_roleManager.Roles.ToList());
        }
        public IActionResult Create() {
            return View(); 
        }

        public IActionResult Assign()
        {
            ViewData["users"] = new SelectList(_userManager.Users, "Id", "Email");
            ViewData["roles"] = new SelectList(_roleManager.Roles, "Id", "Name");
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> AssignAsync(UserRole ur)
        {
            var user = await _userManager.FindByIdAsync(ur.UserId);
            var role = await _roleManager.FindByIdAsync(ur.RoleId);
            await _userManager.AddToRoleAsync(user, role.Name);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(String name)
        {
            IdentityRole role = new IdentityRole { Name = name };
            await _roleManager.CreateAsync(role);
            return RedirectToAction("Index");
        }
    }
}
