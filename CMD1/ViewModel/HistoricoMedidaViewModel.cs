using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMD1.Model.Models.EntityViewModel
{
    public class HistoricoMedidaViewModel
    {
        public long IdMedida { get; set; }
        public long IdAdvertencia { get; set; }
        public string TipoAdvertenciaDescricao { get; set; }
        public long IdMotivo { get; set; }
        public string MotivoDescricao { get; set; }
        public int Quantidade { get; set; }
    }
}