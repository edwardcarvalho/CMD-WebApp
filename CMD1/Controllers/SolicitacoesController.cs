using CMD.Model.Enumeradores;
using CMD.Model.Models;
using CMD.Service.Repositories.MedidasRepository;
using CMD.Service.Repositories.UsuariosRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace CMD1.Controllers
{
    public class SolicitacoesController : Controller
    {
        private readonly IMedidasService _medidasService;
        private readonly IUsuariosService _usuarioService;

        public SolicitacoesController(IMedidasService medidasService, IUsuariosService usuarioService)
        {
            _medidasService = medidasService;
            _usuarioService = usuarioService;
        }

        public ActionResult Index(string currentPage, int pageSize = 10)
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = _usuarioService.GetUserByGuid(User.Identity.GetUserId());

                if (user != null)
                {
                    List<Medidas> medidas = new List<Medidas>();

                    medidas.AddRange(_medidasService.Consultar(
                        c => /*c.FuncSolicitanteId == user.FuncionarioId &&*/ c.Ativo,
                        a => a.Filial,
                        a => a.Funcionario.Operacao.Supervisor,
                        a => a.Advertencia,
                        a => a.Motivo).OrderByDescending(c => c.MedidaId));

                    ViewBag.TotalPages = Math.Ceiling(Convert.ToDecimal(medidas.Count) / pageSize);

                    if (medidas.Count > 0)
                    {
                        medidas.ForEach(c => { VerificarBloqueioDeMedida(ref c); });
                    }

                    if (!string.IsNullOrEmpty(currentPage))
                    {
                        Session["Page"] = Convert.ToInt32(currentPage);
                        ViewBag.Medidas = medidas.Skip(pageSize * (Convert.ToInt32(currentPage) - 1)).Take(pageSize).ToList();
                    }
                    else
                    {
                        Session["Page"] = 1;
                        ViewBag.Medidas = medidas.Skip(pageSize * 0).Take(pageSize).ToList();
                    }
                }

                ViewBag.Editable = false;
                ViewBag.Profile = user.Perfil;
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        private void VerificarBloqueioDeMedida(ref Medidas medida)
        {
            var dtModificado = medida.Modificado.Value.AddHours(48);
            var dtHoje = DateTime.Now;

            var result = DateTime.Compare(dtModificado, dtHoje);

            if (result < 0)
            {
                if (medida.StatusId == (int)Enums.StatusMedida.DisponivelParaImpressao)
                {
                    medida.StatusId = (int)Enums.StatusMedida.BloqueadaPerdaPrazoImpressao;
                }
                else if (medida.StatusId == (int)Enums.StatusMedida.AguardandoAprovacaoCoordenadorGerente)
                {
                    medida.StatusId = (int)Enums.StatusMedida.BloqueadaPerdaPrazoAprovacaoGerente;
                }
                else if (medida.StatusId == (int)Enums.StatusMedida.AguardandoAprovacaoRH)
                {
                    medida.StatusId = (int)Enums.StatusMedida.BloqueadaPerdaPrazoAprovacaoRH;

                }

                _medidasService.Atualizar(medida);
            }
        }
    }
}