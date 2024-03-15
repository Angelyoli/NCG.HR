using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NCG.HR.Data;
using NCG.HR.ViewModels;

namespace NCG.HR.Controllers
{
    [Authorize]
    public class RolesController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ApplicationDbContext _context;

        public RolesController(RoleManager<IdentityRole> roleManager, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, ApplicationDbContext context)
        {
            this._context = context;
            this._userManager = userManager;
            this._roleManager = roleManager;
            this._signInManager = signInManager;
        }
        public async Task<IActionResult> Index()
        {
            var roles = await _context.Roles.ToListAsync();
            return View(roles);
        }

        #region Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RolesViewModel model)
        {
            var role = new IdentityRole { Name = model.RoleName };

            var result = await _roleManager.CreateAsync(role);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }
        #endregion Create

        #region Edit
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var record = await _roleManager.FindByIdAsync(id);
            var role = new RolesViewModel { RoleId = record.Id, RoleName = record.Name };
            return View(role);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, RolesViewModel model)
        {
            var check = await _roleManager.RoleExistsAsync(model.RoleName);
            if (!check)
            {

                var role = await _roleManager.FindByIdAsync(id);
                role.Name = model.RoleName;

                var result = await _roleManager.UpdateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }
        #endregion Edit
    }
}
