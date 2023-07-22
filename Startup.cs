using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Westwind.AspNetCore.LiveReload;
using BudgetingApp.Models.RepositoryModels;
using BudgetingApp.Auth;
using System;

namespace BudgetingApp
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public IConfiguration Configuration {get; set;}
        public Startup(IConfiguration config)
        {
            Configuration = config;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            // budgeting app DbContext
            services.AddDbContext<BudgetingContext>(opts =>
                opts.UseNpgsql(Configuration["ConnectionStrings:BudgetingAppConnection"]));

            // Identity DbContext
            services.AddDbContext<IdentityContext> (opts =>
                opts.UseNpgsql(Configuration["ConnectionStrings:IdentityConnection"]));
            
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<IdentityContext>();

            services.Configure<IdentityOptions>(opts =>{
                opts.Password.RequireDigit = true;
                opts.Password.RequireLowercase = true;
                opts.Password.RequireNonAlphanumeric = true;
                opts.Password.RequireUppercase = true;
                opts.Password.RequiredLength = 6;
                opts.User.RequireUniqueEmail = true;
                opts.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
            });
            

            services.AddLiveReload(config => {});
            services.AddMvc().AddRazorRuntimeCompilation();
            services.AddRazorPages().AddRazorRuntimeCompilation();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, BudgetingContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error/Error500");
                app.UseStatusCodePagesWithRedirects("/Error/Error{0}");
                app.UseHsts();
            }
    
            app.UseLiveReload();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });

            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                scope.ServiceProvider.GetService<BudgetingContext>().Database.Migrate();
                scope.ServiceProvider.GetService<IdentityContext>().Database.Migrate();
            }
            IdentitySeedData.CreateAdminAccount(app.ApplicationServices, Configuration);
            BudgetingSeedData.SeedBudgetingDatabase(context, app.ApplicationServices);
        }
    }
}
