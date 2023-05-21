using System.ComponentModel.DataAnnotations;

namespace New_Zealand.webApi.Models.DTO
{
    public class AddRegionRequestDto
    {
        [Required]
        [MaxLength(3, ErrorMessage ="Code has to be a minimum of 3 characters")]
        [MinLength(3,ErrorMessage ="Code has to be a minimum of 3 characters")]
        public string? Code { get; set; }
        [Required]
        [MaxLength(3, ErrorMessage ="Code has to be a minimum of 3 characters")]
        public string? Name { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
