using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Models
{
    public class Department
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Departament adını daxil edin.")]
        public string DepartmentName { get; set; }
        [Required(ErrorMessage = "Ətraflı məlumat qeyd edin.")]
        public string Description { get; set; }
       
        public string Picture { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }
        public bool IsDeactive { get; set; } 
        public List<HekimDepartment> HekimDepartment { get; set; } 
        public List<Room> Rooms { get; set; }
       
    }
}
