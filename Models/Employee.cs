namespace NCG.HR.Models
{
    public class Employee : UserActivity
    {
   
        public string EmpNo { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {MiddleName} {LastName}";
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public int? CountryId { get; set; }
        public Country Country { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int? DepartmentId { get; set; }
        public Department Department { get; set; }
        public int? DesignationId { get; set; }
        public Designation Designation { get; set; }
        public int? GenderId { get; set; }
        public SystemCodeDetail Gender { get; set; }


    }
}
