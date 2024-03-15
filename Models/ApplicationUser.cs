﻿using Microsoft.AspNetCore.Identity;

namespace NCG.HR.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? NationalId { get; set; }
        public string? CreateById { get; set; }
        public DateTime? CreateOn { get; set; }
        public string? FullName => $"{FirstName} {MiddleName} {LastName}";
        public DateTime? LoginDate { get; set; }
        public string? ModifiedById { get; set; }
        public DateTime? PasswordChangedOn { get; set; }
        public string? RoleId { get; set; }
        public IdentityRole? Role { get; set; }
    }
}
