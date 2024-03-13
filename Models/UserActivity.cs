using NCG.HR.Interface;
using System.ComponentModel.DataAnnotations;

namespace NCG.HR.Models
{
    public abstract class UserActivity : IEntity
    {
        [Key]
        public virtual int Id { get; set; }
        public int? CreatedById { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? ModifyById { get; set; }
        public DateTime ModifyOn { get; set; }
        public string State { get; set; }=string.Empty;
        public string Status { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
    }
}
