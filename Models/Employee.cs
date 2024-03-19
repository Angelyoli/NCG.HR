using System.ComponentModel.DataAnnotations;

namespace NCG.HR.Models
{
    public class Employee : UserActivity
    {   
        [Display(Name="工号")]
        public string EmpNo { get; set; }
        [Display(Name="姓")]
        public string FirstName { get; set; }
        [Display(Name="名")]
        public string MiddleName { get; set; }
        [Display(Name="字")]
        public string LastName { get; set; }
        [Display(Name="全名")]
        public string FullName => $"{FirstName} {MiddleName} {LastName}";
        [Display(Name="地址")]
        public string Address { get; set; }
        [Display(Name="电话")]
        public string PhoneNumber { get; set; }
        [Display(Name="电邮")]
        public string Email { get; set; }
        [Display(Name="区")]
        public string Region { get; set; }
        [Display(Name="邮编")]
        public string PostalCode { get; set; }
        [Display(Name="手机")]
        public string Phone { get; set; }
        [Display(Name="传真")]
        public string Fax { get; set; }
        [Display(Name ="生日")]
        public DateTime DateOfBirth { get; set; }
        [Display(Name="国家")]
        public int? CountryId { get; set; }
        public Country Country { get; set; }
        [Display(Name="城市")]
        public int? CityId { get; set; }    
        public City City { get; set; }
        [Display(Name="部门")]
        public int? DepartmentId { get; set; }
        public Department Department { get; set; }
        [Display(Name="职位")]
        public int? DesignationId { get; set; }
        public Designation Designation { get; set; }
        [Display(Name="性别")]
        public int? GenderId { get; set; }
        public SystemCodeDetail Gender { get; set; }

        [Display(Name ="照片")]
        public string? Photo { get; set; }

    }
}
