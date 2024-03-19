using NCG.HR.Interface;
using System.ComponentModel.DataAnnotations;

namespace NCG.HR.Models
{
    public abstract class UserActivity : IEntity
    {
        [Key]
        public virtual int Id { get; set; }
        [Display(Name="创建人")]
        public string CreatedById { get; set; }
        [Display(Name="创建时间")]
        public DateTime CreatedOn { get; set; }
        [Display(Name="修改人")]
        public string ModifyById { get; set; }
        [Display(Name="修改时间")]
        public DateTime ModifyOn { get; set; }
        [Display(Name="启用状态")]
        public string State { get; set; }=string.Empty;
        [Display(Name="实时状态")]
        public string Status { get; set; } = string.Empty;
        [Display(Name="类型")]
        public string Type { get; set; } = string.Empty;
    }

    public abstract class ApprovalActivity : UserActivity
    {
        

        [Display(Name="批准人")]
        public string ApprovedById { get; set; }
        [Display(Name="批准时间")]
        public DateTime ApprovedOn { get; set; }
        [Display(Name="驳回人")]
        public string RejectedById { get; set; }
        [Display(Name="驳回时间")]
        public DateTime RejectedOn { get; set; }
    }
}
