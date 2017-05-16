using Microsoft.AspNetCore.Builder;
using Nancy.Owin;

namespace FluentSecurity.Nancy.Sample
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.UseOwin(x => x.UseNancy(opt => opt.Bootstrapper = new Bootstrapper()));
        }
    }
}