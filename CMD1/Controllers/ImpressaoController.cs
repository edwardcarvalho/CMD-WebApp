using CMD.Model.Enumeradores;
using CMD.Model.Models;
using CMD.Service.Repositories.MedidasRepository;
using System;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace CMD1.Controllers
{
    public class ImpressaoController : Controller
    {
        private readonly IMedidasService _medidasService;

        public ImpressaoController(IMedidasService medidasService)
        {
            _medidasService = medidasService;
        }

        // GET: Impressao
        public ActionResult Index(string idMedida)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (!string.IsNullOrEmpty(idMedida))
                {
                    Medidas medida = _medidasService.Obter(Convert.ToInt64(idMedida));
                    if (medida.StatusId == (int)Enums.StatusMedida.DisponivelParaImpressao)
                    {
                        ViewBag.TipoImpressao = medida.Suspensao;
                        medida.StatusId = (int)Enums.StatusMedida.Impressa;
                        _medidasService.Atualizar(medida);

                        return View(_medidasService.Obter(c => c.MedidaId == Convert.ToInt64(idMedida), c => c.Funcionario));
                    }
                    else
                    {
                        ViewBag.TipoImpressao = null;
                        return View();
                    }
                }
                else
                {
                    ViewBag.TipoImpressao = null;
                    return View();
                }
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
    }
}