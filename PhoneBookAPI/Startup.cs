using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
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
            //���������� Config �� appsettings.json ��� ��������� connectionString
            Configuration.Bind("PhoneBookAPI", new Config());

            //���������� �������
            services.AddTransient<IPhoneBookRecordRepositoryAPI, EFPhoneBookRecordsRepositoryAPI>();
            services.AddTransient<IAccountRepositoryAPI, EFAccountRepositoryAPI>();
            services.AddTransient<DataManager>();

            //���������� �������� ��
            services.AddDbContext<AppDBContextAPI>(x => x.UseSqlServer(Config.ConnectionString));

            //����������� Identity �������
            services.AddIdentity<IdentityUser, IdentityRole>(opts =>
            {
                opts.User.RequireUniqueEmail = true;
                opts.Password.RequiredLength = 6;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireLowercase = false;
                opts.Password.RequireUppercase = false;
                opts.Password.RequireDigit = false;
            })
            .AddEntityFrameworkStores<AppDBContextAPI>()
            .AddDefaultTokenProviders();

            services.AddControllers();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddHttpContextAccessor();
            services.AddRouting(options =>
            {
                options.ConstraintMap.Add("PhoneBookRecord", typeof(ProvaRouteConstraint));
                options.ConstraintMap.Add("LoginModel", typeof(ProvaRouteConstraint));
                options.ConstraintMap.Add("RegisterModel", typeof(ProvaRouteConstraint));
            });
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
                    pattern: "{controller=Home}/{action=GetRecords}/{id?}");
            });
        }
    }

    internal class ProvaRouteConstraint : IRouteConstraint
    {
        public bool Match(HttpContext? httpContext, IRouter? route, string routeKey,
                          RouteValueDictionary values, RouteDirection routeDirection)
        {
            return false;
        }
    }
}
