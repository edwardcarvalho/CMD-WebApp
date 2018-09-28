using CMD.Model.Models;
using CMD.Service.BaseRepository;

namespace CMD.Service.Repositories.FuncionarioRepository
{
    public interface IFuncionarioService : IRepository<Funcionario>
    {
        Funcionario GetFuncionarioByCpf(string cpf);
    }
}