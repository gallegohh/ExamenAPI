using System;
using System.Collections.Generic;

namespace Examen.DB
{
    public partial class EmployeeTerritories
    {
        public int EmployeeID { get; set; }
        public string TerritoryID { get; set; }

        public Employees Employee { get; set; }
        public Territories Territory { get; set; }
    }
}
