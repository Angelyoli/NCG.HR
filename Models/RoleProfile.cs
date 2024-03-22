using Microsoft.AspNetCore.Identity;

namespace NCG.HR.Models
{
    /// <summary>
    /// 系统角色
    /// </summary>
    public class RoleProfile : UserActivity
    {
        public int TaskId { get; set; }
        public SystemProfile Task { get; set; }
        public string RoleId { get; set; }
        public IdentityRole Role { get; set; }
    }
}
