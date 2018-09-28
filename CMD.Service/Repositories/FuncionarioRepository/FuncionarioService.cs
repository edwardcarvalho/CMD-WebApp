using CMD.Data.EntityContext;
using CMD.Model.Models;
using CMD.Service.BaseRepository;
using System;
using System.Linq;
using System.Data.Entity;

namespace CMD.Service.Repositories.FuncionarioRepository
{
    public class FuncionarioService : Repository<Funcionario>, IFuncionarioService
    {
        public Funcionario GetFuncionarioByCpf(string cpf)
        {
            Funcionario funcionario = null;

            try
            {
                using (var db = new EfContext())
                {
                    var func = db.Funcionario
                    .Include("Cargo")
                    .Include("Filial")
                    .Include("Operacao.Funcionarios")
                    .Include(x => x.Medidas.Select(y => y.Advertencia))
                    .Include(x => x.Medidas.Select(y => y.Motivo))
                    .Include(x => x.Medidas.Select(y => y.Status))
                    .Where(c => c.Cpf == cpf && c.Ativo);

                    if (func.Count() > 0)
                    {
                        funcionario = func.First();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
                throw;
            }

            return funcionario;
        }
    }
}
