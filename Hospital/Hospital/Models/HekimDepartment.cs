using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Models
{
    public class HekimDepartment
    {
        public int Id { get; set; }
        public Hekim Hekim { get; set; }
        public int HekimId { get; set; }
        public Department Department { get; set; }
        public int DepartmentId { get; set; }
    }
}
