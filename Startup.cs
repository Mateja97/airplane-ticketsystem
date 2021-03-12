using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using airplane_ticketsystem.Hubs;
using Microsoft.AspNetCore.SignalR;
using System.Net;
namespace airplane_ticketsystem
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
            services.AddControllersWithViews();
            //MySql
            services.AddTransient<MySqlDatabase>(_ => 
            new MySqlDatabase("server=localhost; database=airplane_tickets; uid=mateja; pwd=Mateja1997!;"));
            
            services.AddSignalR(options => 
            { 
                options.EnableDetailedErrors = true; 
                
            }); 
            ServicePointManager.ServerCertificateValidationCallback +=
                  (sender, certificate, chain, sslPolicyErrors) => true;
              
        }
            
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                

                endpoints.MapHub<FlightsHub>("/flightsHub");
                
            });
            app.Use(async (context, next) =>
            {
                var hubContext = context.RequestServices
                            .GetRequiredService<IHubContext<FlightsHub>>();
                            if (next != null)
                            {
                                await next.Invoke();
                                }
            });
            
            
        }
    }
}
