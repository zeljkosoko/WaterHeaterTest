using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WaterHeaterTest.Data
{
    public class LoginUser: IdentityUser
    {
        public string Ime { get; set; }
        public string Prezime { get; set; }
    }
}
