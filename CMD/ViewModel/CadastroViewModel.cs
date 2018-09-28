using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMD.Model.Models.EntityViewModel
{
    public class CadastroViewModel
    {
        public CadastroViewModel()
        {
            Medidas = new List<HistoricoMedidaViewModel>();
        }

        public long IdFuncionario { get; set; }
        public string Nome { get; set; }
        public Filial Filial { get; set; }
        public string Matricula { get; set; }
        public string Rg { get; set; }
        public string Cpf { get; set; }
        public Operacao Operacao { get; set; }
        public Cargo Cargo { get; set; }
        public List<HistoricoMedidaViewModel> Medidas { get; set; }
    }
}