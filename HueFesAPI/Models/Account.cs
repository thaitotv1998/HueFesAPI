using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HueFesAPI.Data
{
    public class Account
    {
        [Key]
        public Guid AccId { get; set; }
        [Required]
        public string? Username { get; set; }
        [Required]
        public string? Password { get; set; }
        public Guid RoleId { get; set; }

        [ForeignKey("RoleId")]
        public Role? Role { get; set; }

        public string RefreshToken { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; }
        public DateTime ExpiresDate { get; set; }
    }
}
