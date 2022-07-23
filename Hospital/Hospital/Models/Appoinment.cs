using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Models
{
    public class Appoinment
    {
        public int Id { get; set; }
        public List<Department> Departments { get; set; }
        public List<Hekim> Hekims { get; set; }
        //public DateTime AppointmentDate { get; set; }
        public string SerialNo { get; set; }
        public string Problem { get; set; }
        public bool IsDeactive { get; set; }

    }
}
