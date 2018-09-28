using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace CMD.Model.Models
{
    public partial class Filial
    {
        public Filial()
        {
            Funcionarios = new HashSet<Funcionario>();
        }

        [Key]
        public long FilialId { get; set; }
        public string Nome { get; set; }
        public bool Ativo { get; set; }
        public virtual ICollection<Funcionario> Funcionarios { get; set; }

    }
}