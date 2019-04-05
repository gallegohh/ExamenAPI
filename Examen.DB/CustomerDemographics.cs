using System;
using System.Collections.Generic;

namespace Examen.DB
{
    public partial class CustomerDemographics
    {
        public CustomerDemographics()
        {
            CustomerCustomerDemo = new HashSet<CustomerCustomerDemo>();
        }

        public string CustomerTypeID { get; set; }
        public string CustomerDesc { get; set; }

        public ICollection<CustomerCustomerDemo> CustomerCustomerDemo { get; set; }
    }
}
