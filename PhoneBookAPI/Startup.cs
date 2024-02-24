using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PhoneBookAPI.Domain;
using PhoneBookAPI.Domain.Repositories.Abstract;
using PhoneBookAPI.Domain.Repositories.EF;

namespace PhoneBookAPI
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
            //подключаем Config из appsettings.json для получения connectionString
            Configuration.Bind("PhoneBook", new Config());

            //подключаем сервисы
            services.AddTransient<IPhoneBookRecordRepository, EFPhoneBookRecordsRepository>();
            services.AddTransient<DataManager>();

            //подключаем контекст БД
            services.AddDbContext<AppDBContext>(x => x.UseSqlServer(Config.ConnectionString));
            services.AddControllers();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
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
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(name: "api",
                    pattern: "api/{controller=Home}/{action=GetRecords}/{id?}");
                endpoints.MapControllerRoute(name: "default", 
                    pattern: "{area:exists}/{controller=Home}/{action=GetRecords}/{id?}");
            });
        }
    }
}
