using System.ComponentModel.DataAnnotations;

namespace ooohCar.Application.Requests.Identity
{
    public class ForgotPasswordRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}