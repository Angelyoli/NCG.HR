using NCG.HR.Models;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace NCG.HR.ViewModels
{
    public class ProfileViewModel
    {
        public ICollection<SystemProfile>   Profiles { get; set; }

        [Display(Name = "系统角色")]
        public string RoleId { get; set; }

        [Display(Name = "系统权限")]
        public int TaskId { get; set; }
    }
}
