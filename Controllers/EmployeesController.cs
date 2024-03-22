using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NCG.HR.Data;
using NCG.HR.Models;
using System.Security.Claims;

namespace NCG.HR.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private string GetUserId => User.FindFirstValue(ClaimTypes.NameIdentifier);
        private readonly IConfiguration _configuration;

        public EmployeesController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // GET: Employees
        public async Task<IActionResult> Index()
        {
            return View(await _context.Employees.ToListAsync());
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            FillViewData(null);
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Employee employee, IFormFile employeephoto)
        {
            if (employee != null)
                if (employeephoto.Length > 0)
                {
                    var fileName = "EmployeePhoto_" + DateTime.Now.ToString("yyyymmddhhmmss") + "_" + employeephoto.FileName;
                    var path = _configuration["FileSettings:UploadFolder"];
                    var filePath = Path.Combine(path, fileName);
                    var stream = new FileStream(filePath, FileMode.Create);
                    await employeephoto.CopyToAsync(stream);
                    employee.Photo = fileName;
                }

            employee.CreatedById = null;
            employee.CreatedOn = DateTime.Now;
            employee.ModifyById = null;
            employee.ModifyOn = DateTime.Now;
            if (ModelState.IsValid)
            {
                _context.Add(employee);
                await _context.SaveChangesAsync(GetUserId);
                return RedirectToAction(nameof(Index));
            }

            FillViewData(employee);
            return View(employee);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            FillViewData(employee);
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Employee employee)
        {
            if (id != employee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee);
                    await _context.SaveChangesAsync(GetUserId);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            FillViewData(employee);
            return View(employee);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            FillViewData(employee);
            return View(employee);
        }



        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
            }

            await _context.SaveChangesAsync(GetUserId);
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.Id == id);
        }
        private void FillViewData(Employee employee)
        {
            if (employee == null)
            {
                ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Name");
                ViewData["BankId"] = new SelectList(_context.Banks, "Id", "Name");
                ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Name");
                ViewData["DesignationId"] = new SelectList(_context.Designations, "Id", "Name");
                ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name");
                ViewData["GenderId"] = new SelectList(_context.SystemCodeDetails.Include(r => r.SystemCode).Where(r => r.SystemCode.Code.Contains("XB")), "Id", "Description");
            }
            else
            {
                ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Name", employee.CountryId);
                ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Name", employee.CityId);
                ViewData["BankId"] = new SelectList(_context.Banks, "Id", "Name", employee.BankId);
                ViewData["DesignationId"] = new SelectList(_context.Designations, "Id", "Name", employee.DesignationId);
                ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name", employee.DepartmentId);
                ViewData["GenderId"] = new SelectList(_context.SystemCodeDetails.Include(r => r.SystemCode).Where(r => r.SystemCode.Code.Contains("XB")), "Id", "Description", employee.GenderId);
            }
        }
    }
}
