using System.ComponentModel.DataAnnotations;

namespace NCG.HR.Models
{
    public class LeaveApplication : ApprovalActivity
    {
        [Display(Name ="员工姓名")]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        [Display(Name ="请假天数")]
        public int NoOfDays { get; set; }
        [Display(Name ="开始日期")]
        public DateTime StartDate { get; set; }
        [Display(Name ="结束日期")]
        public DateTime EndDate { get; set; }
        [Display(Name ="请假期限")]
        public int DurationId { get; set; }
        public SystemCodeDetail Duration { get; set; }
        [Display(Name = "请假类型")]
        public int LeaveTypeId { get; set; }
        public LeaveType LeaveType { get; set; }
        public string? Attachment { get; set; }
        [Display(Name = "请假备注")]
        public string? Description { get; set; }
        [Display(Name = "请假状态")]
        public int SystemStatusId { get; set; }
        public SystemCodeDetail  SystemStatus { get; set; }
        [Display(Name = "审批备注")]
        public string? ApprovalNotes { get; set; }

    }
}
