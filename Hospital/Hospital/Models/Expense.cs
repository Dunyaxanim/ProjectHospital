using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Models
{
    public class Expense
    {
        public int Id { get; set; }
        public ExpenseCategory ExpenseCategory { get; set; }
        public int ExpenseCategoryId { get; set; }
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
