using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMD.Model.Models.EntityViewModel
{
    public class MedidaViewModel
    {
        public long IdFuncionario { get; set; }
        public long IdFilial { get; set; }
        public long IdAdvertencia { get; set; }
        public long IdMotivo { get; set; }
        public string Data { get; set; }
        public int IdTipoOcorrencia { get; set; }
        public string Telefone { get; set; }
        public string Descricao { get; set; }
        public long IdFuncionarioSolicitante { get; set; }
        public long IdFuncionarioAprovador { get; set; }
    }
}