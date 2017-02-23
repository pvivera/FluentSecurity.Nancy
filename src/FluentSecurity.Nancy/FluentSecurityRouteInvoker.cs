using System;
using System.Threading;
using System.Threading.Tasks;
using Nancy;
using Nancy.Responses.Negotiation;
using Nancy.Routing;

namespace FluentSecurity.Nancy
{
    public class FluentSecurityRouteInvoker : IRouteInvoker
    {
        private DefaultRouteInvoker _defaultRouteInvoker;
        public FluentSecurityRouteInvoker(IResponseNegotiator negotiator)
        {
            _defaultRouteInvoker = new DefaultRouteInvoker(negotiator);
        }

        public async Task<Response> Invoke(Route route, CancellationToken cancellationToken,
            DynamicDictionary parameters, NancyContext context)
        {
            return await _defaultRouteInvoker.Invoke(route, cancellationToken, parameters, context);
        }
    }
}
