using System;
using System.Collections.Generic;

namespace Examen.DB
{
    public partial class Region
    {
        public Region()
        {
            Territories = new HashSet<Territories>();
        }

        public int RegionID { get; set; }
        public string RegionDescription { get; set; }

        public ICollection<Territories> Territories { get; set; }
    }
}
