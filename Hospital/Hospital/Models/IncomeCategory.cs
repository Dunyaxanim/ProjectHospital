using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Models
{
    public class IncomeCategory
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Adını qeyd edin.")]
        public string Name { get; set; }
        public bool IsDeactive { get; set; }
        public List<Income> Income { get; set; }

    }
}
