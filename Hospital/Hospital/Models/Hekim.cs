using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Models
{
    public class Hekim
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Ad daxil edin.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Soyad daxil edin.")]
        public string SureName { get; set; }
        [Required(ErrorMessage = "Şəkil daxil edin.")]

        public string Picture { get; set; }
        [NotMapped]
        [Required(ErrorMessage = "Şəkil daxil edin.")]
        public IFormFile Photo { get; set; }
        public List<HekimDepartment> HekimDepartment { get; set; }
        public List<Patient> Patients { get; set; }
        public HekimDetail HekimDetail { get; set; }
        public List<Schedule> Schedules { get; set; }
        //public AppUser AppUser { get; set; }
        //public string AppUserId { get; set; }
        public bool IsDeactive { get; set; }


    }
}

