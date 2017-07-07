using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
namespace WebApplication1.Start
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddRazorOptions(options =>
                options.ParseOptions = new CSharpParseOptions(LanguageVersion.CSharp7));

            services.AddTransient<IDb, FakeDb>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            app.UseDeveloperExceptionPage();

            app.UseMvcWithDefaultRoute();

            var db = app.ApplicationServices.GetRequiredService<IDb>();
            var things = FakeThingFactory.Construct(1000);
            foreach (var thing in things)
            {
                db.Add(thing);
            }
        }
    }
}