 using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Models
{
    public class Tool
    {
        public int Id { get; set; }
       
        [Required(ErrorMessage = "Ad daxil edin.")]

        public string Name { get; set; }
        [Required(ErrorMessage = "Say daxil edin.")]
        public int Count { get; set; }
        [Required(ErrorMessage = "Dəyər daxil edin.")]
        public int Price { get; set; }
       
        public bool IsDeactive { get; set; }
    }
}
