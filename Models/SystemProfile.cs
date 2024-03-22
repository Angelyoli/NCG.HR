using System.ComponentModel.DataAnnotations;

namespace NCG.HR.Models
{
    /// <summary>
    /// 系统权限
    /// </summary>
    public class SystemProfile : UserActivity
    {
        [Display(Name = "权限名称")]
        public string Name { get; set; }
        [Display(Name = "权限组")]

        public int? ProfileId { get; set; }
        public SystemProfile Profile { get; set; }
        public ICollection<SystemProfile> Children { get; set; }
        [Display(Name = "组编号")]

        public int? Order { get; set; }
    }
}
