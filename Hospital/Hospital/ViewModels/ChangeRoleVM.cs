using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.ViewModels
{
    public class ChangeRoleVM
    {
        public string OldRole { get; set; }
        public string NewRole { get; set; }

        internal static Task<List<IdentityRole>> ToListAsync()
        {
            throw new NotImplementedException();
        }
    }
}
