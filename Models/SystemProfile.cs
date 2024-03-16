namespace NCG.HR.Models
{
    /// <summary>
    /// 系统权限
    /// </summary>
    public class SystemProfile : UserActivity
    {
        public string Name { get; set; }
        public int? ProfileId { get; set; }
        public SystemProfile Profile { get; set; }
        public ICollection<SystemProfile> Children { get; set; }
        public int? Order { get; set; }
    }
}
