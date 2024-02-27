using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PhoneBook.Domain;
using PhoneBook.Domain.Repositories.Abstract;
using PhoneBook.Domain.Repositories.API;

namespace PhoneBook
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            //подключаем Config из appsettings.json для получения connectionString
            Configuration.Bind("PhoneBook", new Config());

            //подключаем сервисы
            services.AddTransient<IPhoneBookRecordRepository, APIPhoneBookRecordsRepository>();
            services.AddTransient<DataManager>();

            //подключаем контекст БД
            services.AddDbContext<AppDBContext>(x => x.UseSqlServer(Config.ConnectionString));

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
                options.LoginPath = "/account/login";
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

            //подключаем поддержку статичных файлов
            app.UseStaticFiles();

            //подключаем систему маршрутизации
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            

            //регистрируем маршруты(endpoints)
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(name: "default",
                    pattern:"{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
