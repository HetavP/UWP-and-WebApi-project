using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectA_B_UWP.Models
{
    public class Contingent
    {
        public int ID { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public Byte[] RowVersion { get; set; }

        // Navigation property to Athletes
        public ICollection<Athlete> Athletes { get; set; }

    }
}
