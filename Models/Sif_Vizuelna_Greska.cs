using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WaterHeaterTest.Models
{
    public class Sif_Vizuelna_Greska
    {
        public int Id { get; set; }

        [Required]
        public string Naziv { get; set; }

        [Required]
        [Display(Description = "1-Beznačajna, 2-Uslovno-značajna, 3-Značajna")]
        /// <summary>
        /// 1-Beznačajna, 2-Uslovno-značajna, 3-Značajna
        /// </summary>
        public int Tip { get; set; }

        [Required]
        [Column(TypeName = "datetime")]
        public DateTime DatumKreiranja { get; set; }

        [Required]
        [StringLength(450)]
        public string IdKorisnikKreirao { get; set; }
    }
}
