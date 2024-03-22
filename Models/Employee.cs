using System.ComponentModel.DataAnnotations;

namespace NCG.HR.Models
{
    public class Employee : UserActivity
    {
        [Display(Name = "工号")]
        public string EmpNo { get; set; }
        [Display(Name = "姓")]
        public string FirstName { get; set; }
        [Display(Name = "名")]
        public string MiddleName { get; set; }
        [Display(Name = "字")]
        public string LastName { get; set; }
        [Display(Name = "全名")]
        public string FullName => $"{FirstName} {MiddleName} {LastName}";
        [Display(Name = "地址")]
        public string Address { get; set; }
        [Display(Name = "电话")]
        public string PhoneNumber { get; set; }
        [Display(Name = "电邮")]
        public string Email { get; set; }
        [Display(Name = "区")]
        public string Region { get; set; }
        [Display(Name = "邮编")]
        public string PostalCode { get; set; }
        [Display(Name = "手机")]
        public string Phone { get; set; }
        [Display(Name = "传真")]
        public string Fax { get; set; }
        [Display(Name = "生日")]
        public DateTime DateOfBirth { get; set; }
        [Display(Name = "国家")]
        public int? CountryId { get; set; }
        public Country Country { get; set; }
        [Display(Name = "城市")]
        public int? CityId { get; set; }
        public City City { get; set; }
        [Display(Name = "部门")]
        public int? DepartmentId { get; set; }
        public Department Department { get; set; }
        [Display(Name = "职位")]
        public int? DesignationId { get; set; }
        public Designation Designation { get; set; }
        [Display(Name = "性别")]
        public int? GenderId { get; set; }
        public SystemCodeDetail Gender { get; set; }

        [Display(Name = "照片")]
        public string? Photo { get; set; }

        public DateTime? EmploymentDate { get; set; }
        public int? SystemStatusId { get; set; }
        public SystemCodeDetail SystemStatus { get; set; }
        public DateTime? InactiveDate { get; set; }
        public int? CauseOfInactivityId { get; set; }
        public SystemCodeDetail CauseOfInactivity { get; set; }
        public DateTime? TerminationDate { get; set; }
        public int? ReasonForTerminationId { get; set; }
        public SystemCodeDetail ReasonForTermination { get; set; }
        public int? EmploymentTermsId { get; set; }
        public SystemCodeDetail EmploymentTerms { get; set; }

        [Display(Name = "银行")]
        public int? BankId { get; set; }
        public Bank Bank { get; set; }
        [Display(Name = "银行账号")]
        public string? BankAccountNo { get; set; }
        /// <summary>
        /// 国际银行账户
        /// </summary>
        public string? IBAN { get; set; }
        /// <summary>
        /// 银行国际代码
        /// </summary>
        public string? SWIFTCode { get; set; }
        /// <summary>
        /// 国家社保号码
        /// </summary>
        public string? NSSFNO { get; set; }
        public string? KRAPIN { get; set; }
        /// <summary>
        /// 护照号码
        /// </summary>
        [Display(Name ="护照号码")]
        public string? PassportNo { get; set; }

    }
}
