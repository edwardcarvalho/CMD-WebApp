using CMD.Model.Enumeradores;
using CMD.Shared.Helpers;
using CMD.Model.Models;
using CMD1.Model.Models.EntityViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CMD.Service.Repositories.MedidasRepository;
using CMD.Service.Repositories.AdvertenciaRepository;
using CMD.Service.Repositories.MotivosRepository;
using CMD.Service.Repositories.UsuariosRepository;
using CMD.Service.Repositories.FilialRepository;
using CMD.Service.Repositories.FuncionarioRepository;
using System.Linq.Expressions;
using System.Globalization;
using Microsoft.AspNet.Identity;

namespace CMD1.Controllers
{
    public class MedidasController : Controller
    {
        private readonly IMedidasService _medidasService;
        private readonly IAdvertenciaService _advertenciaService;
        private readonly IMotivosService _motivosService;
        private readonly IUsuariosService _usuarioService;
        private readonly IFilialService _filialService;
        private readonly IFuncionarioService _funcionarioService;

        public MedidasController(IMedidasService medidasService, IAdvertenciaService advertenciaService, IMotivosService motivosService, IUsuariosService usuarioService, IFilialService filialService, IFuncionarioService funcionarioService)
        {
            _medidasService = medidasService;
            _advertenciaService = advertenciaService;
            _motivosService = motivosService;
            _usuarioService = usuarioService;
            _filialService = filialService;
            _funcionarioService = funcionarioService;
        }

        // GET: Medidas
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.Filiais = GetFiliaisSelect();
                ViewBag.Aprovadores = GetAprovadoresSelect();
                ViewBag.TiposAdvertencia = GetTiposAdvertenciaSelect();
                ViewBag.Motivos = GetMotivosSelect();
                ViewBag.TipoOcorrencia = EnumToSelect<Enums.TipoDeOcorrencia>();

                return View();
            }
            else
            {
                return RedirectToAction("Index", "Account");
            }
        }

        public ActionResult Desbloquear()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = _usuarioService.GetUserByGuid(User.Identity.GetUserId());

                List<Medidas> medidas = new List<Medidas>();

                List<SelectListItem> acoes = EnumToSelect<Enums.StatusMedida>().Where(c => Convert.ToInt32(c.Value) == (int)Enums.StatusMedida.AguardandoAprovacaoCoordenadorGerente || Convert.ToInt32(c.Value) == (int)Enums.StatusMedida.AguardandoAprovacaoRH || Convert.ToInt32(c.Value) == (int)Enums.StatusMedida.DisponivelParaImpressao).ToList();
                acoes.ForEach(a => { a.Text = "Devolver para " + Helper.GetEnumDescription((Enums.StatusMedida)Convert.ToInt32(a.Value)); });
                ViewBag.Acoes = acoes;

                if (user.PerfilId == (int)Enums.Perfil.RH || user.PerfilId == (int)Enums.Perfil.Administrador)
                    medidas.AddRange(_medidasService.Consultar(c => c.StatusId == (int)Enums.StatusMedida.BloqueadaPerdaPrazoAprovacaoGerente || c.StatusId == (int)Enums.StatusMedida.BloqueadaPerdaPrazoAprovacaoRH || c.StatusId == (int)Enums.StatusMedida.BloqueadaPerdaPrazoImpressao, c => c.Filial, f => f.Funcionario.Operacao.Supervisor, a => a.Advertencia, m => m.Motivo));

                ViewBag.Medidas = medidas;

                return View();
            }

            return View();
        }

        public ActionResult DesbloquearMedida(long idMedida, long idStatus)
        {
            Medidas medida = _medidasService.Obter(idMedida);
            medida.StatusId = idStatus;
            medida.Modificado = DateTime.Now;
            var result = _medidasService.Atualizar(medida);

            if (result)
                return Json(new { success = true, message="Medida desbloqueada com sucesso!" }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { success = false, message = "Medida desbloqueada com sucesso!" }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Salvar(Medidas medida)
        {
            if (medida != null)
            {
                medida.FuncSolicitanteId = _usuarioService.Obter(User.Identity.GetUserId(), c => c.Funcionario).FuncionarioId;
                medida.DataSolicitacao = DateTime.Now;
                medida.Modificado = DateTime.Now;
                medida.DataOcorrencia = DateTime.Parse(medida.DataOcorrencia.Value.ToString(), new CultureInfo("pt-BR"));
                medida.Descricao = medida.Descricao;
                medida.IdTipoOcorrencia = medida.IdTipoOcorrencia;
                medida.StatusId = (long)Enums.StatusMedida.AguardandoAprovacaoCoordenadorGerente;
                medida.Ativo = true;

                var ret = _medidasService.Adicionar(medida);

                if (ret)
                    return Json(new { success = true, message = "Medida disciplinar salva com sucesso!" }, JsonRequestBehavior.AllowGet);
                else
                    return Json(new { success = false, message = "Erro ao salvar medida disciplinar!" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, message = "Erro ao salvar medida disciplinar!" }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetFuncionarioByCpf(string cpf)
        {
            var funcionario = _funcionarioService.GetFuncionarioByCpf(cpf);
            CadastroViewModel entityViewModel = null;

            if (funcionario != null)
            {
                entityViewModel = new CadastroViewModel
                {
                    IdFuncionario = funcionario.FuncionarioId,
                    Nome = funcionario.Nome,
                    Cargo = new Cargo { CargoId = funcionario.Cargo.CargoId, Descricao = funcionario.Cargo.Descricao },
                    Filial = new Filial { FilialId = funcionario.Filial.FilialId, Nome = funcionario.Filial.Nome },
                    Operacao = new Operacao { Nome = funcionario.Operacao.Nome, OperacaoId = funcionario.Operacao.OperacaoId, Supervisor = new Funcionario { FuncionarioId = funcionario.Operacao.Supervisor.FuncionarioId, Nome = funcionario.Operacao.Supervisor.Nome }, Gerente = new Funcionario { FuncionarioId = funcionario.Operacao.Gerente.FuncionarioId, Nome = funcionario.Operacao.Gerente.Nome } },
                    Rg = funcionario.Rg,
                    Cpf = funcionario.Cpf,
                    Matricula = funcionario.Matricula,
                    Medidas = GetTableHistoricoMedidasFuncionario(funcionario)
                };
            }

            return Json(entityViewModel, JsonRequestBehavior.AllowGet);
        }

        private List<SelectListItem> GetFiliaisSelect()
        {
            var filiais = _filialService.Consultar();
            try
            {
                var list = new List<SelectListItem>();
                filiais.ForEach(a => { list.Add(new SelectListItem { Value = Convert.ToString(a.FilialId), Text = a.Nome }); });
                return list;
            }
            catch (Exception ex)
            {
                return new List<SelectListItem>();
            }
        }

        private List<SelectListItem> GetAprovadoresSelect()
        {
            var usuarios = _usuarioService.Consultar(c => c.PerfilId == (int)Enums.Perfil.Gerente, c => c.Funcionario);
            try
            {
                var list = new List<SelectListItem>();
                usuarios.ForEach(a => { list.Add(new SelectListItem { Value = Convert.ToString(a.Funcionario.FuncionarioId), Text = a.Funcionario.Nome }); });
                return list;
            }
            catch (Exception ex)
            {
                return new List<SelectListItem>();
            }
        }

        private List<SelectListItem> GetTiposAdvertenciaSelect()
        {
            var advertencias = _advertenciaService.Consultar();
            try
            {
                var list = new List<SelectListItem>();
                advertencias.ForEach(a => { list.Add(new SelectListItem { Value = Convert.ToString(a.AdvertenciaId), Text = a.Descricao }); });
                return list;
            }
            catch (Exception ex)
            {
                return new List<SelectListItem>();
            }
        }

        private List<SelectListItem> GetMotivosSelect()
        {
            var motivos = _motivosService.Consultar();
            try
            {
                var list = new List<SelectListItem>();
                motivos.ForEach(m => { list.Add(new SelectListItem { Value = Convert.ToString(m.MotivoId), Text = m.Descricao }); });
                return list;
            }
            catch (Exception ex)
            {
                return new List<SelectListItem>();
            }
        }

        private List<SelectListItem> EnumToSelect<T>()
        {
            var enumType = typeof(T);

            return Enum.GetValues(enumType).Cast<Enum>().Select(v => new SelectListItem
            {
                Text = v.ToString(),
                Value = Convert.ToInt32(v).ToString()
            }).ToList();
        }

        private List<HistoricoMedidaViewModel> GetTableHistoricoMedidasFuncionario(Funcionario funcionario)
        {
            var medidas = funcionario.Medidas.ToList();

            List<HistoricoMedidaViewModel> lista = new List<HistoricoMedidaViewModel>();

            medidas.ForEach(m =>
            {
                var exists = lista.Where(c => c.IdMotivo == m.MotivoId && c.IdAdvertencia == m.AdvertenciaId).ToList().Count > 0;
                if (!exists)
                {
                    lista.Add(
                            new HistoricoMedidaViewModel
                            {
                                IdMedida = m.MedidaId,
                                IdMotivo = m.Motivo.MotivoId,
                                MotivoDescricao = _motivosService.Obter(m.Motivo.MotivoId).Descricao,
                                IdAdvertencia = m.Advertencia.AdvertenciaId,
                                TipoAdvertenciaDescricao = _advertenciaService.Obter(m.Advertencia.AdvertenciaId).Descricao,
                                Quantidade = medidas.Where(c => c.Motivo.MotivoId == m.Motivo.MotivoId && c.Advertencia.AdvertenciaId == m.Advertencia.AdvertenciaId).ToList().Count()
                            }
                        );
                }

            });

            return lista;

        }
    }
}