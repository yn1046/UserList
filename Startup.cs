using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.DependencyInjection;
using UserList.Services;

namespace UserList
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<UserService>();
            services.AddRouting();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, UserService userService)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true
                });
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseRouter(router =>
            {
                router.MapGet("/api/pageCount", async (request, response, routeData) =>
                {                        
                    await response.WriteAsync(userService.GetPageCount().ToString());
                });

                router.MapGet("/api/users", async (request, response, routeData) =>
                {
                    var id = 0;
                    if (request.Query.Keys.Contains("id"))
                        id = int.Parse(request.Query["id"]);
                        
                    await response.WriteAsync(userService.GetUsers(id));
                });

                router.MapPost("/api/users", async (request, response, routeData) =>
                {
                    await response.WriteAsync(await userService.AddUser(request.Body));
                });
            });

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Page doesn't exist.");
            });
        }
    }
}
