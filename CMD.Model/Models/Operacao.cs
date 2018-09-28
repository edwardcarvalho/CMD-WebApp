using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMD.Model.Models
{
    public partial class Operacao
    {
        
        public Operacao()
        {
            Funcionarios = new HashSet<Funcionario>();
        }

        [Key]
        public long OperacaoId { get; set; }

        public string Nome { get; set; }

        public Nullable<long> SupervisorId { get; set; }
        [InverseProperty("Operacoes")]
        public virtual Funcionario Supervisor { get; set; }

        public Nullable<long> GerenteId { get; set; }
        [InverseProperty("Operacoes1")]
        public virtual Funcionario Gerente { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Funcionario> Funcionarios { get; set; }

    }
}