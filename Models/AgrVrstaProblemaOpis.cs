﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace WaterHeaterTest.Models
{
    public partial class AgrVrstaProblemaOpis
    {
        public AgrVrstaProblemaOpis()
        {
            AktVodootpornostKotao = new HashSet<AktVodootpornostKotao>();
        }

        public int Id { get; set; }
        public int IdVrstaProblema { get; set; }
        public string Opis { get; set; }
        public string Slika { get; set; }
        public DateTime DatumKreiranja { get; set; }

        public virtual SifVrstaProblema IdVrstaProblemaNavigation { get; set; }
        public virtual ICollection<AktVodootpornostKotao> AktVodootpornostKotao { get; set; }
    }
}