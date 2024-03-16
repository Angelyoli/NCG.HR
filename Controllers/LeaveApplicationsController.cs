using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NCG.HR.Data;
using NCG.HR.Models;

namespace NCG.HR.Controllers
{
    [Authorize]
    public class LeaveApplicationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LeaveApplicationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: LeaveApplications
        public async Task<IActionResult> Index()
        {
            var leaveStatus = _context.SystemCodeDetails.Include(r => r.SystemCode).Where(r => r.SystemCode.Code == "LeaveApprovalStatus" && r.Code == "AwaitingApproval").FirstOrDefault();

            var applicationDbContext = _context.LeaveApplications
                .Include(l => l.Duration)
                .Include(l => l.Employee)
                .Include(l => l.LeaveType)
                .Include(l => l.SystemStatus)
                .Where(r=>r.SystemStatusId==leaveStatus.Id);
            return View(await applicationDbContext.ToListAsync());
        }
 
        // GET: LeaveApplications/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveApplication = await _context.LeaveApplications
                .Include(l => l.Duration)
                .Include(l => l.Employee)
                .Include(l => l.LeaveType)
                .Include(l => l.SystemStatus)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (leaveApplication == null)
            {
                return NotFound();
            }

            return View(leaveApplication);
        }

        #region Create

        // GET: LeaveApplications/Create
        public IActionResult Create()
        {
            ViewData["DurationId"] = new SelectList(_context.SystemCodeDetails.Include(r => r.SystemCode).Where(r => r.SystemCode.Code == "LeaveDuration"), "Id", "Code");
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "FullName");
            ViewData["LeaveTypeId"] = new SelectList(_context.LeaveTypes, "Id", "Name");
            return View();
        }

        // POST: LeaveApplications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LeaveApplication leaveApplication)
        {

            var pendingStatus = _context.SystemCodeDetails.Include(r => r.SystemCode).Where(r => r.SystemCode.Code == "LeaveApprovalStatus" && r.Code == "AwaitingApproval").FirstOrDefault();
            if (ModelState.IsValid)
            {
                leaveApplication.EndDate = leaveApplication.StartDate.AddDays(leaveApplication.NoOfDays);
                leaveApplication.SystemStatusId = pendingStatus.Id;
                _context.Add(leaveApplication);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewData["DurationId"] = new SelectList(_context.SystemCodeDetails.Include(r => r.SystemCode).Where(r => r.SystemCode.Code == "LeaveDuration"), "Id", "Code", leaveApplication.DurationId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "FullName", leaveApplication.EmployeeId);
            ViewData["LeaveTypeId"] = new SelectList(_context.LeaveTypes, "Id", "Name", leaveApplication.LeaveTypeId);
            return View(leaveApplication);
        }
        #endregion Create

        #region Edit

        // GET: LeaveApplications/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveApplication = await _context.LeaveApplications.FindAsync(id);
            if (leaveApplication == null)
            {
                return NotFound();
            }
            ViewData["DurationId"] = new SelectList(_context.SystemCodeDetails.Include(r => r.SystemCode).Where(r => r.SystemCode.Code == "LeaveDuration"), "Id", "Code", leaveApplication.DurationId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "FullName", leaveApplication.EmployeeId);
            ViewData["LeaveTypeId"] = new SelectList(_context.LeaveTypes, "Id", "Name", leaveApplication.LeaveTypeId);
            return View(leaveApplication);
        }

        // POST: LeaveApplications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, LeaveApplication leaveApplication)
        {
            if (id != leaveApplication.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var pendingStatus = _context.SystemCodeDetails.Include(r => r.SystemCode).Where(r => r.SystemCode.Code == "LeaveApprovalStatus" && r.Code == "Pending").FirstOrDefault();
                try
                {
                    leaveApplication.SystemStatusId = pendingStatus.Id;
                    _context.Update(leaveApplication);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LeaveApplicationExists(leaveApplication.Id))
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
            ViewData["DurationId"] = new SelectList(_context.SystemCodeDetails.Include(r => r.SystemCode).Where(r => r.SystemCode.Code == "LeaveDuration"), "Id", "Code", leaveApplication.DurationId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "FullName", leaveApplication.EmployeeId);
            ViewData["LeaveTypeId"] = new SelectList(_context.LeaveTypes, "Id", "Name", leaveApplication.LeaveTypeId);
            return View(leaveApplication);
        }

        #endregion Edit

        #region Delete
        // GET: LeaveApplications/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveApplication = await _context.LeaveApplications
                .Include(l => l.Duration)
                .Include(l => l.Employee)
                .Include(l => l.LeaveType)
                .Include(l => l.SystemStatus)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (leaveApplication == null)
            {
                return NotFound();
            }

            return View(leaveApplication);
        }

        // POST: LeaveApplications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var leaveApplication = await _context.LeaveApplications.FindAsync(id);
            if (leaveApplication != null)
            {
                _context.LeaveApplications.Remove(leaveApplication);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LeaveApplicationExists(int id)
        {
            return _context.LeaveApplications.Any(e => e.Id == id);
        }
        #endregion Delete

        #region 批准请假
        // GET: LeaveApplications/ApproveLeave/6
        [HttpGet]
        public async Task<IActionResult> ApproveLeave(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveApplication = await _context.LeaveApplications
                .Include(l => l.Duration)
                .Include(l => l.Employee)
                .Include(l => l.LeaveType)
                .Include(l => l.SystemStatus)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (leaveApplication == null)
            {
                return NotFound();
            }

            return View(leaveApplication);
        }

        [HttpPost]
        public async Task<IActionResult> ApproveLeave(LeaveApplication leaveApplication)
        {
            var approvedStatus = _context.SystemCodeDetails
                .Include(r => r.SystemCode)
                .Where(r => r.SystemCode.Code == "LeaveApprovalStatus" && r.Code == "Approved").FirstOrDefault();
            var record = await _context.LeaveApplications
                .Include(r => r.Duration)
                .Include(r => r.Employee)
                .Include(r => r.LeaveType)
                .Include(r => r.SystemStatus)
                .FirstOrDefaultAsync(r => r.Id == leaveApplication.Id);

            if (null == record)
                return NotFound();

            record.ApprovedOn = DateTime.Now;
            record.SystemStatusId = approvedStatus.Id;
            record.ApprovalNotes = leaveApplication.ApprovalNotes;
            _context.Update(record);
            await _context.SaveChangesAsync();

            ViewData["DurationId"] = new SelectList(_context.SystemCodeDetails.Include(r => r.SystemCode).Where(r => r.SystemCode.Code == "LeaveDuration"), "Id", "Code", leaveApplication.DurationId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "FullName", leaveApplication.EmployeeId);
            ViewData["LeaveTypeId"] = new SelectList(_context.LeaveTypes, "Id", "Name", leaveApplication.LeaveTypeId);
            return View(record);
        }

        #endregion  批准请假

        #region 驳回请假
        // GET: LeaveApplications/RejectLeave/7
        [HttpGet]
        public async Task<IActionResult> RejectLeave(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveApplication = await _context.LeaveApplications
                .Include(l => l.Duration)
                .Include(l => l.Employee)
                .Include(l => l.LeaveType)
                .Include(l => l.SystemStatus)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (leaveApplication == null)
            {
                return NotFound();
            }

            return View(leaveApplication);
        }

        [HttpPost]
        public async Task<IActionResult> RejectLeave(LeaveApplication leaveApplication)
        {
            var approvedStatus = _context.SystemCodeDetails
                .Include(r => r.SystemCode)
                .Where(r => r.SystemCode.Code == "LeaveApprovalStatus" && r.Code == "Rejected").FirstOrDefault();
            var record = await _context.LeaveApplications
                .Include(r => r.Duration)
                .Include(r => r.Employee)
                .Include(r => r.LeaveType)
                .Include(r => r.SystemStatus)
                .FirstOrDefaultAsync(r => r.Id == leaveApplication.Id);

            if (null == record)
                return NotFound();

            record.RejectedOn = DateTime.Now;
            record.SystemStatusId = approvedStatus.Id;
            record.ApprovalNotes = leaveApplication.ApprovalNotes;

            _context.Update(record);
            await _context.SaveChangesAsync();

            ViewData["DurationId"] = new SelectList(_context.SystemCodeDetails.Include(r => r.SystemCode).Where(r => r.SystemCode.Code == "LeaveDuration"), "Id", "Code", leaveApplication.DurationId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "FullName", leaveApplication.EmployeeId);
            ViewData["LeaveTypeId"] = new SelectList(_context.LeaveTypes, "Id", "Name", leaveApplication.LeaveTypeId);
            return View(record);
        }

        #endregion 驳回请假

        #region 已审批请假
        // GET: LeaveApplications/ApprovedLeaveApplications/8
        [HttpGet]
        public async Task<IActionResult> ApprovedLeaveApplications()
        {
            var approvedStatus = _context.SystemCodeDetails
                .Include(r => r.SystemCode)
                .Where(r => r.SystemCode.Code == "LeaveApprovalStatus" && r.Code == "Approved").FirstOrDefault();

            var leaveApplications = _context.LeaveApplications
                .Include(l => l.Duration)
                .Include(l => l.Employee)
                .Include(l => l.LeaveType)
                .Include(l => l.SystemStatus)
                .Where(r => r.SystemStatusId == approvedStatus!.Id);

            return View(await leaveApplications.ToListAsync());
        }
        #endregion 已审批请假

        #region 已审批请假
        // GET: LeaveApplications/RejectedLeaveApplications/9
        [HttpGet]
        public async Task<IActionResult> RejectedLeaveApplications()
        {
            var approvedStatus = _context.SystemCodeDetails
                .Include(r => r.SystemCode)
                .Where(r => r.SystemCode.Code == "LeaveApprovalStatus" && r.Code == "Rejected").FirstOrDefault();

            var leaveApplications = _context.LeaveApplications
                .Include(l => l.Duration)
                .Include(l => l.Employee)
                .Include(l => l.LeaveType)
                .Include(l => l.SystemStatus)
                .Where(r => r.SystemStatusId == approvedStatus!.Id);

            return View(await leaveApplications.ToListAsync());
        }
        #endregion 已审批请假
    }
}
