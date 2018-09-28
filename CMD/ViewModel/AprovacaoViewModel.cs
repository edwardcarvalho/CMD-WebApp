using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMD.Model.Models.EntityViewModel
{
    public class AprovacaoViewModel
    {
        public long idMedida { get; set; }
        public string comentario1 { get; set; }
        public string comentario2 { get; set; }
        public string alinea { get; set; }
        public int acao { get; set; }
        public string dataInicio { get; set; }
        public string dataFinal { get; set; }
    }
}