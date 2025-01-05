using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Class_Domain
{
    public class Prescription
    {
        public int Id { get; set; }
        public string PatientName { get; set; }
        public int Quantity { get; set; }
        public DateTime IssuedDate { get; set; }
        public ICollection<Medicine> Medicines { get; set; } = new List<Medicine>();
        public int PharmacistId { get; set; }
        public Pharmacist Pharmacist { get; set; }
    }

}
