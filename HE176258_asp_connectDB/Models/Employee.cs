using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HE176258_asp_connectDB.Models
{
    public partial class Employee
    {

        public int Id { get; set; }
        public string? Image { get; set; }

        [Required(ErrorMessage = "Employee name is required")]
        [Display(Name = "Employee name")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Year of birth is required")]
        public int Dob { get; set; }
    }
}
