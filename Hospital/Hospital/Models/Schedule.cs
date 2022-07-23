using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Models
{
    public class Schedule
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Günlər qeyd olunmalıdır.")]
        [NotMapped]
        public List<string> Days { get; set; }
        [Required(ErrorMessage = "Günlər qeyd olunmalıdır.")]
        public string AvailableDays { get; set; }
        [Required(ErrorMessage = "İşə başlama saatını daxil edin.")]
        public DateTime StartTime { get; set; }
        [Required(ErrorMessage = "İşdən çıxış saatını daxil edin.")]
        public DateTime EndTime { get; set; }
        public bool IsDeactive { get; set; }
        public Hekim Hekim { get; set; }
        public int HekimId { get; set; }
        


    }
}
