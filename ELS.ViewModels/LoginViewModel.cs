using System.ComponentModel.DataAnnotations;

using static Els.Constants.EntityValidationsConstants;
namespace ELS.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [MaxLength(FirmIdMaxLength)]
        [MinLength(FirmIdMinLength)]
        public string FirmId { get; set; }
        public string Password { get; set; }

        public string? ReturnUrl { get; set; }

        public bool RemeberMe { get; set; }

    }
}
