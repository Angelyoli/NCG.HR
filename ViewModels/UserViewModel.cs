using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;

namespace NCG.HR.ViewModels
{
    public class UserViewModel
    {
        public int Id { get; set; }
        [Display(Name = "用户名")]
        public string UserName { get; set; }
        [Display(Name = "电子邮箱")]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "姓")]
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        [Display(Name = "名")]
        public string LastName { get; set; }
        [Display(Name = "联系电话")]
        public string PhoneNumber { get; set; }
        [Display(Name = "联系地址")]
        public string Address { get; set; }
        [Display(Name = "国籍")]
        public string? NationalId { get; set; }
        public string? FullName => $"{FirstName} {MiddleName} {LastName}";
        [Display(Name = "用户角色")]
        public string? RoleId { get; set; }

    }
}
