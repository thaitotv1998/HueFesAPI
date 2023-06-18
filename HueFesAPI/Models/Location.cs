using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HueFesAPI.Data
{
    public class Location
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string ImagePath { get; set; }
        public int LocationTypeId { get; set; }
        [ForeignKey("LocationTypeId")]
        public LocationType LocationType { get; set; }
    }
}
