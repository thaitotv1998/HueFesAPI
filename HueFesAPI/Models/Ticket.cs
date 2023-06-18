using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HueFesAPI.Data
{
    public class Ticket
    {
        [Key]
        public int Id { get; set; }
        public DateTime DateCreated { get; set; }
        public string Code { get; set; }
        public bool Status { get; set; }
        public int TicketTypeId { get; set; }
        [ForeignKey("TicketTypeId")]
        public TicketType TicketType { get; set; }

    }
}
