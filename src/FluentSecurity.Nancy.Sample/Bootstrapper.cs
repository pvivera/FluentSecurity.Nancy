using System;
using Nancy;
using Nancy.Bootstrapper;

namespace FluentSecurity.Nancy.Sample
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        protected override Func<ITypeCatalog, NancyInternalConfiguration> InternalConfiguration
        {
            get
            {
                return typeCatalog =>
                {
                    var defaultConfiguration = NancyInternalConfiguration.Default(typeCatalog);
                    defaultConfiguration.RouteInvoker = typeof(FluentSecurityRouteInvoker);
                    return defaultConfiguration;
                };
            }
        }
    }
}