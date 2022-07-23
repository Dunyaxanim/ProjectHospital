 using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Models
{
    public class Patient
    {
        public int Id { get; set; }
        //[Required(ErrorMessage = "Adını qeyd edin.")]
        public string FirstName { get; set; }
        //[Required(ErrorMessage = "Soyadını qeyd edin.")]
        public string LastName { get; set; }
        public PatientDetail PatientDetail { get; set; }
        public Hekim Hekim { get; set; }
        public int HekimId { get; set; }
        public bool IsDeactive { get; set; }
    }
}
