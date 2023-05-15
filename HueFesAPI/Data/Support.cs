using System.ComponentModel.DataAnnotations;

namespace HueFesAPI.Data
{
    public class Support
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
