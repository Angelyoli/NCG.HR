using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NCG.HR.Data;
using NCG.HR.Models;
using NCG.HR.ViewModels;
using System.Security.Claims;

namespace NCG.HR.Controllers
{
    public class ProfilesController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ProfilesController(ApplicationDbContext context)
        {
            _context = context;

        }
        public async Task<IActionResult> Index()
        {
            var tasks = new ProfileViewModel();
            var roles = await _context.Roles.OrderBy(r => r.Name).ToListAsync();
            ViewBag.Roles = new SelectList(roles, "Id", "Name"); // 角色

            var systemTasks = await _context.SystemProfiles
                .Include("Children.Children.Children")
                .OrderBy(r => r.Order)
                //.Where(r => r.ProfileId == null) // 第一层系统权限
                .ToListAsync();

            ViewBag.Tasks = new SelectList(systemTasks, "Id", "Name"); // 具备哪些权限
            return View(tasks);
        }

        [HttpPost]
        public async Task<IActionResult> AssignRights(ProfileViewModel profile)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var role = new RoleProfile
            {
                TaskId = profile.TaskId,
                RoleId = profile.RoleId,
            };
            
            _context.RoleProfiles.Add(role);
            await _context.SaveChangesAsync(userId);
            return RedirectToAction("Index");
        }
    }
}
