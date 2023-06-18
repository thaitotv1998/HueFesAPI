using System.ComponentModel.DataAnnotations;

namespace HueFesAPI.Data
{
    public class EventType
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
