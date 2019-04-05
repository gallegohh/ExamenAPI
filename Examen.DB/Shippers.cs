using System;
using System.Collections.Generic;

namespace Examen.DB
{
    public partial class Shippers
    {
        public Shippers()
        {
            Orders = new HashSet<Orders>();
        }

        public int ShipperID { get; set; }
        public string CompanyName { get; set; }
        public string Phone { get; set; }

        public ICollection<Orders> Orders { get; set; }
    }
}
