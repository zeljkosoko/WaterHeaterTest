using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WaterHeaterTest.Models
{
    public class Agr_AktElektroispravnostBojler_SifElektropult
    {
        public int Id { get; set; }
        public int IdAktElektroispravnostBojler { get; set; }
        public int IdSifElektropult { get; set; }
        public DateTime DatumKreiranja { get; set; }
    }
}
