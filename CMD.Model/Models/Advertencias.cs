using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace CMD.Model.Models
{
    public class Advertencias
    {
        [Key]
        public long AdvertenciaId { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }
    }
}