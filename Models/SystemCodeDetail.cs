using System.ComponentModel.DataAnnotations;

namespace NCG.HR.Models
{
    /// <summary>
    /// 系统配置明细
    /// </summary>
    public class SystemCodeDetail : UserActivity
    {
        public int SystemCodeId { get; set; }
        public SystemCode SystemCode { get; set; }
        [Display(Name = "系统配置编码")]
        public string Code { get; set; }
        [Display(Name = "系统配置编码说明")]

        public string Description { get; set; }
        [Display(Name = "配置组编号")]

        public string? OrderNo { get; set; }
    }
}
