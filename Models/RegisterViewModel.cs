using System;
using System.ComponentModel.DataAnnotations;

namespace apple.Models
{
    public class RegisterViewModel : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Name: ")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Alias: ")]
        public string Alias { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email: ")]
        public string Email { get; set; }

        [Required]
        [MinLength(8)]
        [DataType(DataType.Password)]
        [Display(Name = "Password: ")]
        public string Password { get; set; }

        // If the confirm password field doesnt match the password field this error is generated
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Confirm Password does not match Password.")]
        [Display(Name = "Confirm Password: ")]
        public string Confirm { get; set; }
    }
}