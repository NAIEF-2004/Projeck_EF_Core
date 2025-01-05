using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Class_Domain
{

    public class Pharmacist
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ContactInfo ContactInfo { get; set; }
        public ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();
        public bool IsDeleted { get; set; } = false; 
    }
}
