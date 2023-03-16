using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WaterHeaterTest.Models
{
    public class SifElektropult
    {
        public int Id { get; set; }

        [Required]
        public string Sifra { get; set; }

        public DateTime DatumKreiranja { get; set; }
    }
}
