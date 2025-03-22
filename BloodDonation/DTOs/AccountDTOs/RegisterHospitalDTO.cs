using System.ComponentModel.DataAnnotations;

namespace BloodDonation.DTOs.AccountDTOs
{
    public class RegisterHospitalDTO
    {
        [Required, StringLength(100)]
        public string Name { get; set; }

        [Required, StringLength(100)]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Username can only contain letters and digits.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [RegularExpression("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid Email .")]
        public string Email { get; set; }

        [Required, MinLength(6)]
        public string Password { get; set; }

        [Required]
        public string Address { get; set; }

        [Required(ErrorMessage = "Mobile number is required")]
        [RegularExpression("^(010|011|012|015)\\d{8}$", ErrorMessage = "Invalid  mobile number. Must be 11 digits starting with 010, 011, 012, or 015.")]
        public string PhoneNumber { get; set; }

        //public string? Image { get; set; }

    }
}
