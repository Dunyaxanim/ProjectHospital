using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Models
{
    public class Income
    {
        public int Id { get; set; }
        public IncomeCategory IncomeCategory { get; set; }
        public int IncomeCategoryId { get; set; }

        [Required(ErrorMessage = "Kimin adına olduğunu qeyd edin.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Dəyərini qeyd edin.")]
        public int Amount { get; set; }
        [Required(ErrorMessage = "Ətraflı məlumat qeyd edin.")]
        public string Discription { get; set; }
        [Required(ErrorMessage = "Tarix qeyd edin.")]
        public DateTime Date { get; set; }
        public bool IsDeactive { get; set; }
    }
}
