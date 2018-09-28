using CMD.Model.Models;
using CMD.Service.Repositories.UsuariosRepository;
using CMD.Shared.Helpers;
using System;
using System.Web.Mvc;
using System.Web.Security;

namespace CMD.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUsuariosService _usuariosService;

        public AccountController(IUsuariosService usuariosService)
        {
            _usuariosService = usuariosService;
        }

        // GET: Account
        [AllowAnonymous]
        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Login(Usuarios model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                Usuarios user = _usuariosService.GetUserByCredentials(model.Login, Helper.EncryptPassword(model.Senha));
                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(Convert.ToString(user.UsuarioId), false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("CustomError", "Usuário ou senha incorretos.");
                }

            }

            return View(model);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }
    }
}