using HueFesAPI.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HueFesAPI.Models
{
    public class User
    {
        [Key]
        public Guid UserId { get; set; }
        [Required]
        [MaxLength(255)]
        public string? FullName { get; set; }
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string? Email { get; set; }
        [Required]
        [Phone]
        public string? PhoneNumber { get; set; }
        public Guid AccId { get; set; }

        [ForeignKey("AccId")]
        public Account? Account { get; set; }
    }
}
