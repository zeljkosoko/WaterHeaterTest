﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace WaterHeaterTest.Models
{
    public partial class AgrSifraKotaoSarzaKotao
    {
        public AgrSifraKotaoSarzaKotao()
        {
            SifSerijskiBrojKotao = new HashSet<SifSerijskiBrojKotao>();
        }

        public int Id { get; set; }
        public int IdKotao { get; set; }
        public int IdSarza { get; set; }
        public DateTime DatumKreiranja { get; set; }

        public virtual SifKotao IdKotaoNavigation { get; set; }
        public virtual SifSarzaKotao IdSarzaNavigation { get; set; }
        public virtual ICollection<SifSerijskiBrojKotao> SifSerijskiBrojKotao { get; set; }
    }
}