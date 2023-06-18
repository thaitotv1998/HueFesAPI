using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HueFesAPI.Data
{
    public class Event
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public int TypeInOff { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int EventTypeId { get; set; }
        [ForeignKey("EventTypeId")]
        public EventType EventType { get; set; }
        public int TicketTypeId { get; set; }
        [ForeignKey("TicketTypeId")]
        public TicketType TicketType { get; set; }
        public int LocationId { get; set; }
        [ForeignKey("LocationId")]
        public Location Location { get; set; }
    }
}
