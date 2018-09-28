using CMD.Data.EntityContext;
using CMD.Model.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace CMD1.App_Start
{
    public class IocConfig
    {
        public static Container GetConfig()
        {
            var container = new Container();

            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();
            container.Register<ApplicationUserManager>(Lifestyle.Scoped);
            container.Register<IUserStore<Usuarios>>(() => new UserStore<Usuarios>(container.GetInstance<EfContext>()), Lifestyle.Scoped);
            container.Register<ApplicationSignInManager>(Lifestyle.Scoped);
            container.Register<IAuthenticationManager>(() => HttpContext.Current.GetOwinContext().Authentication, Lifestyle.Scoped);

            // Register all controllers
            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());

            var services = Assembly.Load("CMD.Service").GetExportedTypes();

            foreach (var service in services.Where(c => !c.Name.Contains("Repository")))
            {
                if (!service.Name.StartsWith("I"))
                {
                    if (service.GetInterfaces().Where(p => !p.Name.Equals("IDisposable")).ToList().Count > 0)
                    {
                        container.Register(service.GetInterfaces().Where(c => c.Name.Contains(service.Name)).First(), service, Lifestyle.Scoped);
                    }
                }
            }

            return container;
        }
    }
}