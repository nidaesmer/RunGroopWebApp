using System.ComponentModel.DataAnnotations;

namespace RunGroopWebApp.ViewModel
{
    public class LoginViewModel
    {
        [Display(Name ="Email Address")] //ui de nası gorunecegi placeholder gibi denebilir
        [Required(ErrorMessage ="Email address is required")]
        public string EmailAddress { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
