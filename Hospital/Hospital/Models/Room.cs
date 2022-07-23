using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Models
{
    public class Room
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Otağın nömrəsini daxil edin.")]
        public int Number { get; set; }
        
        [Required(ErrorMessage = "Otağın tutumunu qeyd edin.")]
        public string Capacity { get; set; }
        
        public bool IsEmpty { get; set; }
        
        public bool IsDeactive { get; set; }
        [Required(ErrorMessage = "Hansı departamentə aid oluğunu daxil edin.")]
        public Department Department { get; set; }
        public int DepartmentId { get; set; }
        public List<RoomSchedule> RoomSchedules { get; set; }
    }
}
