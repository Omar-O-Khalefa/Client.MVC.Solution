using Client.DAL.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Client.PL.ViewModels
{
    //public enum Gender
    //{
    //    [EnumMember(Value = "Male")]
    //    Male = 1,
    //    [EnumMember(Value = "Female")]
    //    Female = 2

    //}
    //public enum EmpType
    //{
    //    FullTime = 1
    //        , PartTime = 2
    //}
    public class EmployeeViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name Is Requierd")]
        [MaxLength(50, ErrorMessage = "Max Length Of Name Is 50 Chars")]
        [MinLength(5, ErrorMessage = "Min Length Of Name Is 5 Chars")]
        public string Name { get; set; }

        [Range(22, 30)]
        public int? Age { get; set; }

        public string Adress { get; set; }
        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        [EmailAddress]
        public string Email { get; set; }

       //public Gender Gender { get; set; }

       // public EmpType EmployeeType { get; set; }

        [Display(Name = "Phone Number")]
        [Phone]
        public string PhoneNumber { get; set; }
        [Display(Name = "Hiring Date")]
        public DateTime HiringDate { get; set; }

        public DateTime CreationDate { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; } = false;

        //[ForeignKey("Department")]

        //navigational Porperty => [One]
        public int? DepartmentId { get; set; } // foregin Key Column

        //[InverseProperty(nameof(Models.Department.Employees))]
        public Department Department { get; set; }


    }
}
