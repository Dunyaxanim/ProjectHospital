using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Models
{
    public class ExpenseCategory
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Ad daxil edin.")]
        public string Name { get; set; }
        public bool IsDeactive { get; set; }
        public List<Expense> Expenses { get; set; }
    }
}
