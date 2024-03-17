using NCG.HR.Interface;
using System.ComponentModel.DataAnnotations;

namespace NCG.HR.Models
{
    public abstract class UserActivity : IEntity
    {
        [Key]
        public virtual int Id { get; set; }
        public string CreatedById { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifyById { get; set; }
        public DateTime ModifyOn { get; set; }
        public string State { get; set; }=string.Empty;
        public string Status { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
    }

    public abstract class ApprovalActivity : UserActivity
    {
        public string ApprovedById { get; set; }
        public DateTime ApprovedOn { get; set; }
        public string RejectedById { get; set; }
        public DateTime RejectedOn { get; set; }
    }
}
