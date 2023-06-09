using System.ComponentModel.DataAnnotations;

namespace New_Zealand.webApi.Models.DTO
{
    public class RegisterRequestDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string? Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        public string[]? Roles { get; set; }
    }
}
