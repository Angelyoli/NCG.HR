using System.ComponentModel.DataAnnotations;

namespace NCG.HR.Models
{
    /// <summary>
    /// 系统配置项目
    /// </summary>
    public class SystemCode : UserActivity
    {
        [Display(Name = "系统编码")]
        public string Code { get; set; }
        [Display(Name ="编码描述")]
        public string Description { get;set; }
    }
}
