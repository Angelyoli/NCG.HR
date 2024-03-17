using NCG.HR.Models;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace NCG.HR.ViewModels
{
    public class ProfileViewModel
    {
        [Display(Name = "系统角色")]
        public string RoleId { get; set; }
        public ICollection<SystemProfile>   Profiles { get; set; }
        public ICollection<int> RolesProfilesIds { get; set; }
        public int[] Ids { get; set; }

        [Display(Name = "系统权限")]
        public int TaskId { get; set; }
    }
}
