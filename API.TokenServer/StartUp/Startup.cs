using API.TokenServer.IdentityEntities;
using IdentityServer4.Models;
using IdentityServer4.Test;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;

namespace API.TokenServer.StartUp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Controller Level
            services.AddControllers();
            services.AddIdentityServer()
                .AddInMemoryClients(Clients.Get(Configuration))
                .AddInMemoryIdentityResources(IdentityEntities.Resources.GetIdentityResources())
                .AddInMemoryApiResources(IdentityEntities.Resources.GetApiResources(Configuration))
                .AddInMemoryApiScopes(IdentityEntities.Resources.GetApiScopes())
                .AddTestUsers(new List<TestUser>())
                .AddDeveloperSigningCredential();

            // Service Level

            // Data Level
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseIdentityServer();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
