using CollageApp.Validators;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace CollageApp.Models
{
    public class StudentDTO
    {
        [ValidateNever]
        public int Id { get; set; }
        
        [Required(ErrorMessage ="Studnet Name is requried")]
        [StringLength(30)]
        public string StudentName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        //[Range(10,20)]
        //public int Age { get; set; }
        
        [Required]
        public string Address { get; set; }
        [DateCheck]
        public DateTime AddmissionDate { get; set; }



    }
}
