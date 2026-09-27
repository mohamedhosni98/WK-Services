using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WK_Services.Domain.Entities
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Shipment_Address { get; set; }
        public string Email { get; set; }
        public string Country { get; set; }
        public string City { get; set; }

        public ICollection<Contact> Contacts { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
