using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PhoneBookWEB.Domain;
using PhoneBookWEB.Domain.Entities;
using PhoneBookWEB.Domain.Repositories.Abstract;
using PhoneBookWEB.Domain.Repositories.API;

namespace PhoneBookWEB
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
            Configuration.Bind("PhoneBook", new Config());

            services.AddDbContext<AppDBContext>(x => x.UseSqlServer(Config.ConnectionString));

            services.AddControllersWithViews();
            services.AddTransient<IAccountRepository, APIAccountRepository>();
            services.AddTransient<IPhoneBookRecordRepository, APIPhoneBookRecordsRepository>();
            services.AddTransient<DataManager>();
            services.AddTransient<UserRoles>();

            //настраиваем Identity систему
            services.AddIdentity<IdentityUser, IdentityRole>(opts =>
            {
                opts.User.RequireUniqueEmail = true;
                opts.Password.RequiredLength = 6;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireLowercase = false;
                opts.Password.RequireUppercase = false;
                opts.Password.RequireDigit = false;
            })
            .AddEntityFrameworkStores<AppDBContext>()
            .AddDefaultUI()
            .AddDefaultTokenProviders();

            //настраиваем authentication Cookie
            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Name = "MyPhoneBookAuth";
                options.Cookie.HttpOnly = true;
                options.LoginPath = "/Views/Login";
                options.AccessDeniedPath = "/account/accessdenied";
                options.SlidingExpiration = true;
            });

            //поддержка контроллеров и представлений
            services.AddControllersWithViews()
            //выставляем совместимость с aspnet.core 3.1
            .SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_3_0)
            .AddSessionStateTempDataProvider();
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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
