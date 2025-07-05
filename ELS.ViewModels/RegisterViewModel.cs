using System.ComponentModel.DataAnnotations;

using static Els.Constants.EntityValidationsConstants;

namespace ELS.ViewModels
{
    public class RegisterViewModel
    {
      

        [Required]
        [MaxLength(ApplicationUserNameMaxLenght)]
        [MinLength(ApplicationUserNameMinLength)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(ApplicationUserLastNameMaxLength)]
        [MinLength(ApplicationUserLastNameMinLength)]
        public string LastName { get; set; }
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(FirmIdMaxLength)]
        [MinLength(FirmIdMinLength)]
        public string FirmId { get; set; }
        [Required]
        [MaxLength(DepartmentMaxLength)]
        public string Department { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password does not match")]
        public string ConfirmPassword { get; set; }



    }
}
