using BloodDonation.Models;
using BloodDonation.Models.Enums;

using System.ComponentModel.DataAnnotations;

namespace BloodDonation.DTOs.AccountDTOs
{
    public class RegisterRecipientDTO
    {
        [Required, StringLength(100)]
        public string Name { get; set; }

        [Required, StringLength(100)]
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

        [Required]
        public int Age { get; set; }

        [Required]
        public Gender Gender { get; set; }

        [Required]
        public TypeOfBlood Blood { get; set; }

        //public string? Image { get; set; }


        public string? GenderText => Gender.ToString();
        public string? BloodTypeText => Blood.ToString();
    }
}
