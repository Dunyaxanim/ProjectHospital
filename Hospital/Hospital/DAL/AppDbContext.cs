using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hospital.Models;
using Hospital.Controllers;

namespace Hospital.DAL
{
    public class AppDbContext:IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {

        }
        public DbSet<Hekim> Hekims { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<HekimDetail> HekimDetails { get; set; }
        public DbSet<HekimDepartment> HekimDepartments { get; set; }
        public DbSet<RoomSchedule> RoomSchedules { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<PatientDetail> PatientDetails { get; set; }
        public DbSet<Tool> Tools { get; set; }
        public DbSet<IncomeCategory> IncomeCategories { get; set; }
        public DbSet<ExpenseCategory> ExpenseCategories { get; set; }
        public DbSet<Income> Incomes { get; set; }

        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Blood> Bloods { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Blood>().HasData(new Blood
            {
                Id = 1,
                Name= "I(O)",
                Quantity=0
            }, new Blood
            {
                Id = 2,
                Name = "II(A)",
                Quantity = 0
            }, new Blood
            {
                Id = 3,
                Name = "III(B)",
                Quantity = 0
            },
            new Blood
            {
                Id = 4,
                Name = "IV(C)",
                Quantity = 0
            }
            );
            
        }
    }
    
}