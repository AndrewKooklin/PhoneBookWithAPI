using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PhoneBook.Domain;
using PhoneBook.Domain.Repositories.Abstract;
using PhoneBook.Domain.Repositories.EF;

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
            //���������� Config �� appsettings.json ��� ��������� connectionString
            Configuration.Bind("PhoneBook", new Config());

            //���������� �������
            services.AddTransient<IPhoneBookRecordRepository, EFPhoneBookRecordsRepository>();
            services.AddTransient<DataManager>();

            //���������� �������� ��
            services.AddDbContext<AppDBContext>(x => x.UseSqlServer(Config.ConnectionString));

            //��������� ������������ � �������������
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //���������� ��������� ��������� ������
            app.UseStaticFiles();

            //���������� ������� �������������
            app.UseRouting();

            //������������ ��������(endpoints)
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(name: "default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
