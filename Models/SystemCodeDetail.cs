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
        public string Code { get; set; }
        public string Description { get; set; }
        public int? OrderNo { get; set; }
    }
}
