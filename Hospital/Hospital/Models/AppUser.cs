using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Models
{
    public class AppUser:IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public bool IsDeactive { get; set; }
        //public List<Hekim> Hekims { get; set; }

        public static implicit operator AppUser(string v)
        {
            throw new NotImplementedException();
        }
    }
}
