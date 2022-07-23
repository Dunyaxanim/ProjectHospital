using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Models
{
    public class Blood
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [Required(ErrorMessage = "Qan miqdarını daxil edin.")]
        public int Quantity { get; set; }
    }
}
