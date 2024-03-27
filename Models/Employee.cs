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
        public string MiddleName { get; set; } = string.Empty;
        [Display(Name = "字")]
        public string LastName { get; set; }
        [Display(Name = "身份证号")]
        public string IdentityNumber { get; set; }
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
        [Display(Name = "籍贯")]
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
        [Display(Name = "在职状态")]

        public int? ActiveStatusId { get; set; }
        public SystemCodeDetail ActiveStatus { get; set; }

        [Display(Name = "照片")]
        public string? Photo { get; set; }
        [Display(Name = "民族")]
        public int? NationId { get; set; }
        public SystemCodeDetail Nation { get; set; }
        [Display(Name = "政治面貌")]
        public int? PoliticalStatusId { get; set; }
        public SystemCodeDetail PoliticalStatus { get; set; }
        [Display(Name = "入党时间")]
        public DateTime? JoinPartyTime { get; set; }
        [Display(Name = "身份")]
        public int? CadreStatusId { get; set; }
        public SystemCodeDetail CadreStatus { get; set; }
        [Display(Name = "最高学历")]
        public int? HighestEducationId { get; set; }
        public SystemCodeDetail HighestEducation { get; set; }
        [Display(Name = "毕业院校")]
        public int? GraduatedSchoolId { get; set; }
        public SystemCodeDetail GraduatedSchool { get; set; }

        [Display(Name = "毕业专业")]
        public int? GraduatedMajorId { get; set; }
        public SystemCodeDetail GraduatedMajor { get; set; }
        [Display(Name = "毕业时间")]
        public DateTime? GraduatedTime { get; set; }
        [Display(Name = "参加工作时间")]
        public DateTime? StartWorkTime { get; set; }
        [Display(Name = "本单位工作时间")]
        public DateTime? WorkInUnitTime { get; set; }
        [Display(Name = "工龄")]
        public int? WorkedYeas { get; set; }
        [Display(Name = "专业职务")]
        public int? ProfessionalQualificationId { get; set; }
        public SystemCodeDetail ProfessionalQualification { get; set; }
        [Display(Name = "职务取得时间")]
        public DateTime? GetProfessionalTime { get; set; }
        [Display(Name = "聘用职务")]
        public int? RecruitmentPositionId { get; set; }
        public SystemCodeDetail RecruitmentPosition { get; set; }
        [Display(Name = "执业资格")]
        public int? QualificationId { get; set; }
        public SystemCodeDetail Qualification { get; set; }

        [Display(Name = "合同")]
        public string? ContractNumber { get; set; }
        [Display(Name = "专业类别")]
        public int? ProfessionalCategoryId { get; set; }
        public SystemCodeDetail ProfessionalCategory { get; set; }
        [Display(Name = "职称")]
        public int? JobTitleId { get; set; }
        public SystemCodeDetail JobTitle { get; set; }
        [Display(Name = "是否全科")]
        public bool? IsGeneral { get; set; }
        [Display(Name = "领导职务")]

        public int? leadershipPositionId { get; set; }
        public SystemCodeDetail leadershipPosition { get; set; }


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
        [Display(Name = "护照号码")]
        public string? PassportNo { get; set; }

    }

    public class EmployeeImportEntity
    {

        public string EmpNo { get; set; }

        public string FirstName { get; set; }
        public string MiddleName { get; set; } = string.Empty;

        public string LastName { get; set; }

        public string IdentityNumber { get; set; }


        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string Region { get; set; }

        public string PostalCode { get; set; }

        public string Phone { get; set; }

        public string Fax { get; set; }

        public string DateOfBirth { get; set; }

        public string CountryId { get; set; }


        public string CityId { get; set; }
        public string DepartmentId { get; set; }

        public string DesignationId { get; set; }

        public string GenderId { get; set; }

        public string ActiveStatusId { get; set; }

        public string Photo { get; set; }
        public string NationId { get; set; }
        public string PoliticalStatusId { get; set; }
        public string? JoinPartyTime { get; set; }
        [Display(Name = "身份")]
        public string? CadreStatusId { get; set; }
        public string HighestEducationId { get; set; }

        public string GraduatedSchoolId { get; set; }

        public string GraduatedMajorId { get; set; }

        public string? GraduatedTime { get; set; }
        [Display(Name = "参加工作时间")]
        public string? StartWorkTime { get; set; }
        [Display(Name = "本单位工作时间")]
        public string? WorkInUnitTime { get; set; }
        [Display(Name = "工龄")]
        public int? WorkedYeas { get; set; }
        [Display(Name = "专业职务")]
        public string ProfessionalQualificationId { get; set; }

        public string? GetProfessionalTime { get; set; }
        [Display(Name = "聘用职务")]
        public string RecruitmentPositionId { get; set; }

        public string QualificationId { get; set; }


        [Display(Name = "合同")]
        public string? ContractNumber { get; set; }
        [Display(Name = "专业类别")]
        public string ProfessionalCategoryId { get; set; }

        public string JobTitleId { get; set; }

        public bool? IsGeneral { get; set; }


        public string leadershipPositionId { get; set; }
        public SystemCodeDetail leadershipPosition { get; set; }



        public string BankId { get; set; }

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
        [Display(Name = "护照号码")]
        public string? PassportNo { get; set; }
    }
}