using System.ComponentModel.DataAnnotations;

namespace HueFesAPI.Data
{
    public class LocationType
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
