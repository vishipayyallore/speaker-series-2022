using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzServiceBusQueue.GettingStarted.Data
{
    public class Order
    {
        public Guid OrderID { get; set; } = Guid.NewGuid();

        public int Quantity { get; set; }

        public float UnitPrice { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.Now;
    }

}
