using Hospital.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.ViewModels
{
    public class DashboardVM
    {
        public List<Expense> Expenses { get; set; }
        public List<Income> Incomes { get; set; }
        public List<AppUser> Users { get; set; }
        public List<Patient> Patients { get; set; }
        public List<Hekim> Hekims { get; set; }
    }
}
