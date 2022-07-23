using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Hospital.Models
{
    public class EmailMessage
    {
        [Required(ErrorMessage = "Mail ünvanı daxil edin.")]
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        

    }
}
