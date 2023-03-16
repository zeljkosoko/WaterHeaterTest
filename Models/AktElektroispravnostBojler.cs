using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WaterHeaterTest.Models
{
    public class AktElektroispravnostBojler
    {
        public int Id { get; set; }
        public int IdTestResults { get; set; }

        [StringLength(450)]
        public string IdKorisnikKontrolisao { get; set; }
        public DateTime DatumKontrolisanja { get; set; }
    }
}
