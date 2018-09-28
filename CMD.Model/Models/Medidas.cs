using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;

namespace CMD.Model.Models
{
    public partial class Medidas
    {

        [Key]
        public long MedidaId { get; set; }

        public Nullable<long> FuncionarioId { get; set; }
        [InverseProperty("Medidas")]
        public virtual Funcionario Funcionario { get; set; }

        public Nullable<long> FuncSolicitanteId { get; set; }
        [InverseProperty("Medidas1")]
        public virtual Funcionario FuncSolicitante { get; set; }

        public Nullable<long> FuncAprovadorId { get; set; }
        [InverseProperty("Medidas2")]
        public virtual Funcionario FuncAprovador { get; set; }

        public long AdvertenciaId { get; set; }
        public Advertencias Advertencia { get; set; }

        public long MotivoId { get; set; }
        public Motivos Motivo { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? DataOcorrencia { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? DataSolicitacao { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? DataInicioSuspensao { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? DataFinalSuspensao { get; set; }
        public int IdTipoOcorrencia { get; set; }
        public string Telefone { get; set; }
        public string Descricao { get; set; }
        public string Comentario1 { get; set; }
        public string Comentario2 { get; set; }
        public string Alinea { get; set; }

        public long FilialId { get; set; }
        public Filial Filial { get; set; }

        public long StatusId { get; set; }
        public StatusMedida Status { get; set; }

        public bool Suspensao { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? Modificado { get; set; }
        public bool Ativo { get; set; }
    }
}