using System.ComponentModel.DataAnnotations;

namespace NCG.HR.Models
{
    /// <summary>
    /// 系统配置项目
    /// </summary>
    public class SystemCode : UserActivity
    {     
        public string Code { get; set; }
        public string Description { get;set; }
    }
}
