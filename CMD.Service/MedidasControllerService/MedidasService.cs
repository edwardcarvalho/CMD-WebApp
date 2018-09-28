using CMD.Data.EntityContext;
using CMD.Model.Enumeradores;
using CMD.Model.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CMD.Service.MedidasControllerService
{
    public class MedidasService : IMedidasService
    {
        public List<Medidas> Consultar()
        {
            List<Medidas> medidas = new List<Medidas>();
            try
            {
                using (var db = new EfContext())
                {
                    medidas.AddRange(
                        db.Medida
                        .Include(x => x.Advertencia)
                        .Include(x => x.Filial)
                        .Include(x => x.FuncAprovador)
                        .Include(x => x.Funcionario.Operacao.Supervisor)
                        .Include(x => x.Funcionario)
                        .Include(x => x.Motivo)
                        .Include(x => x.Status)
                        .Where(c => c.Ativo).ToList()
                        );
                }

                return medidas;
            }
            catch (Exception ex)
            {
                return medidas;
            }
        }

        public List<Medidas> Consultar(Expression<Func<Medidas, bool>> filtro)
        {
            List<Medidas> medidas = new List<Medidas>();
            try
            {
                using (var db = new EfContext())
                {
                    medidas.AddRange(
                        db.Medida
                        .Include(x => x.Advertencia)
                        .Include(x => x.Filial)
                        .Include(x => x.FuncAprovador)
                        .Include(x => x.Funcionario.Operacao.Supervisor)
                        .Include(x => x.Motivo)
                        .Include(x => x.Status)

                        .Where(filtro)
                        );
                }

                return medidas;
            }
            catch (Exception ex)
            {
                return medidas;
            }
        }

        public Medidas GetMedidaById(long id, bool complete)
        {
            Medidas medida = null;

            using (EfContext db = new EfContext())
            {
                if (complete)
                {
                    medida = db.Medida.Find(id);
                    db.Entry(medida).Reference(c => c.Funcionario).Load();
                    db.Entry(medida).Reference(c => c.Motivo).Load();
                    db.Entry(medida).Reference(c => c.Status).Load();
                    db.Entry(medida).Reference(c => c.Advertencia).Load();
                    db.Entry(medida).Reference(c => c.Filial).Load();
                    db.Entry(medida).Reference(c => c.FuncAprovador).Load();
                    db.Entry(medida).Reference(c => c.FuncSolicitante).Load();
                }
                else
                {
                    medida = db.Medida.Find(id);
                }
            }

            return medida;
        }

        public bool Salvar(Medidas medida)
        {
            bool salvou = false;
            try
            {
                using (var db = new EfContext())
                {
                    medida.DataSolicitacao = DateTime.Now;
                    medida.Modificado = DateTime.Now;
                    medida.DataOcorrencia = DateTime.Parse(medida.DataOcorrencia.Value.ToString(), new CultureInfo("pt-BR"));
                    medida.Descricao = medida.Descricao;
                    medida.IdTipoOcorrencia = medida.IdTipoOcorrencia;
                    medida.StatusId = (long)Enums.StatusMedida.AguardandoAprovacaoCoordenadorGerente;
                    medida.Ativo = true;

                    db.Entry(medida).State = EntityState.Added;

                    salvou = db.SaveChanges() > 0;

                }

                return salvou;
            }
            catch (Exception ex)
            {
                return salvou;
            }
        }

        public bool Atualizar(Medidas medida)
        {
            bool salvou = false;

            try
            {
                using (var db = new EfContext())
                {
                    //var temp = db.Medida.Find(medida.IdMedida);
                    //temp.Modificado = DateTime.Now;
                    //temp.Comentario1 = medida.Comentario1;
                    //temp.Comentario2 = medida.Comentario2;
                    //temp.Alinea = medida.Alinea;
                    //temp.DataInicioSuspensao = medida.DataInicioSuspensao;
                    //temp.DataFinalSuspensao = medida.DataFinalSuspensao;
                    //temp.Suspensao = medida.Suspensao;
                    //temp.Status = db.StatusMedida.Single(c => c.IdStatus == medida.Status.IdStatus);
                    //db.SaveChanges();
                    db.Entry(medida).State = EntityState.Modified;
                    salvou = db.SaveChanges() > 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return salvou;
        }

        public bool Excluir(Medidas medida)
        {
            throw new NotImplementedException();
        }

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

        public Funcionario GetFuncionarioById(long id)
        {
            Funcionario funcionario = null;

            try
            {
                using (var db = new EfContext())
                {
                    funcionario = db.Funcionario
                    .Include("Cargo")
                    .Include("Filial")
                    .Include("Operacao")
                    .Where(c => c.FuncionarioId == id && c.Ativo).First();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
                throw;
            }

            return funcionario;
        }

        public Motivos GetMotivosById(long id)
        {
            try
            {
                using (var ctx = new EfContext())
                {
                    return ctx.Motivo.Where(c => c.MotivoId == id && c.Ativo).First();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Motivos> GetAllMotivos()
        {
            List<Motivos> lista = new List<Motivos>();
            try
            {
                using (var db = new EfContext())
                {
                    lista.AddRange(db.Motivo.Where(c => c.Ativo));
                }

                return lista;

            }
            catch (Exception ex)
            {
                return lista;
            }
        }

        public Advertencias GetAdvertenciasById(long id)
        {
            try
            {
                using (var db = new EfContext())
                {
                    return db.Advertencia.Where(c => c.AdvertenciaId == id && c.Ativo).First();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Advertencias> GetAllAdvertencias()
        {
            List<Advertencias> lista = new List<Advertencias>();
            try
            {
                using (var ctx = new EfContext())
                {
                    lista.AddRange(ctx.Advertencia.Where(c => c.Ativo));
                }

                return lista;

            }
            catch (Exception ex)
            {
                return lista;
            }
        }

        public List<Filial> GetAllFiliais()
        {
            List<Filial> lista = new List<Filial>();
            try
            {
                using (var ctx = new EfContext())
                {
                    lista.AddRange(ctx.Filial.Where(c => c.Ativo));
                }

                return lista;

            }
            catch (Exception ex)
            {
                return lista;
            }
        }

    }
}
