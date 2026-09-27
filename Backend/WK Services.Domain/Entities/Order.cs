using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WK_Services.Domain.Enums;

namespace WK_Services.Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }

        public int ClientId { get; set; }
        public int ServiceId { get; set; }

        public string OrderNumber { get; set; } = null!;
        public OrderType OrderType { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedAt { get; set; }

        public DateTime? RequestedDeliveryDate { get; set; }

        public Client Client { get; set; } = null!;
        public Service Service { get; set; } = null!;
        public int CreatedByContactId { get; set; }
        public Contact CreatedByContact { get; set; } = null!;
    }
}
