using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WaterHeaterTest.Models
{
    public class Agr_Vizuelna_Greska_Opis
    {
        public int Id { get; set; }

        [Required]
        public int IdVizuelnaGreska { get; set; }
        public string Opis { get; set; }
        public string Komentar { get; set; }
        public string Slika { get; set; }

        [Required]
        [Column(TypeName = "datetime")]
        public DateTime DatumKreiranja { get; set; }
    }
}
