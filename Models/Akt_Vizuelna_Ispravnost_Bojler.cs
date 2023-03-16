using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WaterHeaterTest.Models
{
    public class Akt_Vizuelna_Ispravnost_Bojler
    {
        public int Id { get; set; }

        [Required]
        public int IdSerijskiBrojBojler { get; set; }

        [Required]
        /// <summary>
        /// 0-neispravan, 1-ispravan
        /// </summary>
        public bool Ispravan { get; set; }

        public int? IdAgrVizuelnaGreskaOpis { get; set; }

        [Required]
        [StringLength(450)]
        public string IdKorisnikKontrolisao { get; set; }
        
        [Required]
        [Column(TypeName = "datetime")]
        public DateTime DatumKontrolisanja { get; set; }
    }
}
