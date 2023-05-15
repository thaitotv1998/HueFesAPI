using System.ComponentModel.DataAnnotations;

namespace HueFesAPI.Data
{
    public class Role
    {
        [Key]
        public int Id { get; set; }
        public string RoleName { get; set; }
    }
}
