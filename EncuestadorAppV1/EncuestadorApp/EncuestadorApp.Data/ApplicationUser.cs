using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using EncuestadorApp.Data;

namespace EncuestadorApp.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
    }
}
