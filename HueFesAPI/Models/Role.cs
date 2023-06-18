using System.ComponentModel.DataAnnotations;

namespace HueFesAPI.Data
{
    public class Role
    {
        [Key]
        public Guid RoleId { get; set; }
        public string? RoleName { get; set; }
    }
}
