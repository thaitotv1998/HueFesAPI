using System.ComponentModel.DataAnnotations;

namespace HueFesAPI.Data
{
    public class TicketType
    {
        [Key]
        public int Id { get; set; }
        public string TypeName { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
    }
}
