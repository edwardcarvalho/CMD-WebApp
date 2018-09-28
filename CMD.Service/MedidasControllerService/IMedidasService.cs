using CMD.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CMD.Service.MedidasControllerService
{
    public interface IMedidasService
    {
        bool Salvar(Medidas medida);
        bool Atualizar(Medidas medida);
        bool Excluir(Medidas medida);
        Medidas GetMedidaById(long id, bool complete);
        List<Medidas> Consultar();
        List<Medidas> Consultar(Expression<Func<Medidas, bool>> filtro);
        Funcionario GetFuncionarioById(long id);
        Funcionario GetFuncionarioByCpf(string cpf);
        Motivos GetMotivosById(long id);
        List<Motivos> GetAllMotivos();
        Advertencias GetAdvertenciasById(long id);
        List<Advertencias> GetAllAdvertencias();
        List<Filial> GetAllFiliais();
    }
}
