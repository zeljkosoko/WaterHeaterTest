using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

namespace WaterHeaterTest.Models
{
    public class Sif_Serijski_Broj_Bojler
    {
        public int Id { get; set; }
        
        [Required]
        public string Naziv { get; set; }

        [Required]
        [Column(TypeName = "datetime")]
        public DateTime DatumKreiranja { get; set; }
    }
}
