﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace WaterHeaterTest.Models
{
    public partial class SifKotao
    {
        public SifKotao()
        {
            AgrSifraKotaoSarzaKotao = new HashSet<AgrSifraKotaoSarzaKotao>();
        }

        public int Id { get; set; }
        public string Naziv { get; set; }
        public DateTime DatumKreiranja { get; set; }

        public virtual ICollection<AgrSifraKotaoSarzaKotao> AgrSifraKotaoSarzaKotao { get; set; }
    }
}