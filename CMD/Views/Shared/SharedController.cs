using CMD.Model.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMD.Views.Shared
{
    public class SharedController : Controller
    {
        // GET: Shared
        public ActionResult Index()
        {
            return View();
        }

        //public JsonResult GetUserInfo()
        //{
        //    var user = new Usuarios().Obter(Convert.ToInt64(User.Identity.Name));
        //    return Json(new Usuarios { Login = user.Login }, JsonRequestBehavior.AllowGet);
        //}
    }
}