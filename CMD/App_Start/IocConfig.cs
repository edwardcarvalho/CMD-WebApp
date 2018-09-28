using SimpleInjector;
using SimpleInjector.Integration.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace CMD.App_Start
{
    public class IocConfig
    {
        public static Container GetConfig()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            var services = Assembly.Load("CMD.Service").GetExportedTypes();

            foreach (var service in services.Where(c => !c.Name.Contains("Repository")))
            {
                if (!service.Name.StartsWith("I"))
                {
                    if (service.GetInterfaces().Where(p => !p.Name.Equals("IDisposable")).ToList().Count > 0)
                    {
                        container.Register(service.GetInterfaces().Where(c => c.Name.Contains(service.Name)).First(), service, Lifestyle.Singleton);
                    }
                }
            }

            return container;
        }
    }
}