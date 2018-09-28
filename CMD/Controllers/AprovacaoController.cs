using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Globalization;
using CMD.Model.Models;
using CMD.Model.Enumeradores;
using CMD.Model.Models.EntityViewModel;
using CMD.Service.Repositories.EmailService;
using CMD.Service.Repositories.MedidasRepository;
using CMD.Service.Repositories.UsuariosRepository;

namespace CMD.Controllers
{
    public class AprovacaoController : Controller
    {
        private readonly IUsuariosService _usuariosService;
        private readonly IMedidasService _medidasService;
        private readonly IEmailService _emailService;

        public AprovacaoController(IUsuariosService usuariosService, IMedidasService medidasService, IEmailService emailService)
        {
            _usuariosService = usuariosService;
            _medidasService = medidasService;
            _emailService = emailService;
        }
        // GET: Aprovacao
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = _usuariosService.Obter(
                    c => c.UsuarioId == Convert.ToInt64(User.Identity.Name),
                    a => a.Perfil);

                if (user != null)
                {
                    List<Medidas> medidas = new List<Medidas>();

                    if (user.PerfilId == (int)Enums.Perfil.Administrador)
                    {
                        medidas.AddRange(_medidasService.Consultar(
                            c => c.Ativo,
                            a => a.Advertencia,
                            a => a.Filial,
                            a => a.Funcionario.Operacao.Supervisor,
                            a => a.Motivo,
                            a => a.Status));
                    }
                    else if (user.PerfilId == (int)Enums.Perfil.Coordenador || user.PerfilId == (int)Enums.Perfil.Gerente)
                    {
                        medidas.AddRange(_medidasService.Consultar(
                            c => c.FuncAprovadorId == user.FuncionarioId &&
                            c.StatusId == (int)Enums.StatusMedida.AguardandoAprovacaoCoordenadorGerente &&
                            c.Ativo,
                            a => a.Advertencia,
                            a => a.Filial,
                            a => a.Funcionario.Operacao.Supervisor,
                            a => a.Motivo,
                            a => a.Status));
                    }
                    else if (user.Perfil.PerfilId == (int)Enums.Perfil.RH)
                    {
                        medidas.AddRange(_medidasService.Consultar(
                            c => c.StatusId == (int)Enums.StatusMedida.AguardandoAprovacaoRH &&
                            c.Ativo,
                            a => a.Advertencia,
                            a => a.Filial,
                            a => a.Funcionario.Operacao.Supervisor,
                            a => a.Motivo,
                            a => a.Status));
                    }
                    //if (medidas.Count > 0)
                    ViewBag.Medidas = medidas;

                }

                ViewBag.Profile = user.Perfil;
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }

        }

        public ActionResult Atualizar(AprovacaoViewModel aprovacao)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (aprovacao != null)
                {
                    var medida = _medidasService.Obter(aprovacao.idMedida);
                    var user = _usuariosService.Obter(
                        c => c.UsuarioId == Convert.ToInt64(User.Identity.Name),
                        a => a.Funcionario);

                    if (aprovacao.acao == (int)Enums.Acao.Aprovar)
                    {
                        if (medida.StatusId == (int)Enums.StatusMedida.AguardandoAprovacaoCoordenadorGerente)
                        {
                            medida.Comentario1 = aprovacao.comentario1;
                            medida.StatusId = (int)Enums.StatusMedida.AguardandoAprovacaoRH;
                            _medidasService.Atualizar(medida);
                        }
                        else if (medida.StatusId == (int)Enums.StatusMedida.AguardandoAprovacaoRH)
                        {
                            medida.Comentario2 = aprovacao.comentario2;
                            medida.Alinea = aprovacao.alinea;

                            if (!string.IsNullOrEmpty(aprovacao.dataInicio) && !string.IsNullOrEmpty(aprovacao.dataFinal))
                            {
                                medida.DataInicioSuspensao = DateTime.Parse(aprovacao.dataInicio, new CultureInfo("pt-BR"));
                                medida.DataFinalSuspensao = DateTime.Parse(aprovacao.dataFinal, new CultureInfo("pt-BR"));
                                medida.Suspensao = true;
                            }

                            medida.StatusId = (int)Enums.StatusMedida.DisponivelParaImpressao;
                            _medidasService.Atualizar(medida);
                            _emailService.EnviarEmail(user.Funcionario.Email, user.Funcionario.Nome, medida.MedidaId, true);
                        }

                        return Json(new { success = true, message = "Medida aprovada com sucesso!" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        medida.Comentario1 = aprovacao.comentario1;
                        medida.StatusId = (int)Enums.StatusMedida.Reprovado;
                        _medidasService.Atualizar(medida);
                        _emailService.EnviarEmail(user.Funcionario.Email, user.Funcionario.Nome, medida.MedidaId, false);
                        return Json(new { success = true, message = "Medida reprovada com sucesso!" }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { success = false, message = "Desculpe, tivemos um problema com sua solicitação!", error = "object null value" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
    }
}