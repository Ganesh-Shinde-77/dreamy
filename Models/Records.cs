using System.ComponentModel.DataAnnotations;

namespace WebApplicationn.Models
{
    public class Records
    {
        public string name { get; set; } = string.Empty;

        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string emailAddress { get; set; } = string.Empty;
        public string message { get; set; } = string.Empty;

    }

}
