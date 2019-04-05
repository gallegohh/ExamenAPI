using System;
using System.Collections.Generic;

namespace Examen.DB
{
    public partial class CustomerCustomerDemo
    {
        public string CustomerID { get; set; }
        public string CustomerTypeID { get; set; }

        public Customers Customer { get; set; }
        public CustomerDemographics CustomerType { get; set; }
    }
}
