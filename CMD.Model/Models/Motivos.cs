using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace CMD.Model.Models
{
    public class Motivos
    {
        [Key]
        public long MotivoId { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }
    }
}