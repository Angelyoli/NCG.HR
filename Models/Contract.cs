using System.ComponentModel.DataAnnotations;

namespace NCG.HR.Models
{
    public class EmployeeContract : UserActivity
    {
        public string ContractNumber { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public DateTime? EmploymentDate { get; set; }
        public int? ContractStatusId { get; set; }
        public SystemCodeDetail ContractStatus { get; set; }
        public DateTime? InactiveDate { get; set; }
        public int? CauseOfInactivityId { get; set; }
        public SystemCodeDetail CauseOfInactivity { get; set; }
        public DateTime? TerminationDate { get; set; }
        public int? ReasonForTerminationId { get; set; }
        public SystemCodeDetail ReasonForTermination { get; set; }
        public int? EmploymentTermsId { get; set; }
        public SystemCodeDetail EmploymentTerms { get; set; }
    }
}