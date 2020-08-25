using HasastPiyasa.DataAccess.Abstract;
using HasastPiyasa.DataAccess.Concrete;
using HasatPiyasa.Business.Abstract;
using HasatPiyasa.Business.Concrete;
using HasatPiyasa.Entity.Entity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace HasatPiyasa_Web_UI
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
            // Add framework services.
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromHours(1);
                options.IOTimeout = TimeSpan.FromSeconds(20);
                options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
                options.Cookie.IsEssential = true;

            });
            services.AddDistributedMemoryCache();
            services.AddDbContext<HasatPiyasaContext>();
            services.AddScoped<IBolgeDal, EfBolgeDal>();
            services.AddScoped<IClaimDal, EfClaimDal>();
            services.AddScoped<IDataInputDal, EfDataInputDal>();
            services.AddScoped<IDataInputService, DataInputManager>();
            services.AddScoped<IEmteaDal, EfEmteaDal>();
            services.AddScoped<ICityDal, EfCityManager>();
            services.AddScoped<IEmteaGroupDal, EfEmteaGroupDal>();
            services.AddScoped<IEmteaTypeDal, EfEmteaTypeDal>();
            services.AddScoped<IEmteaTypeGroupDal, EfEmteaTypeGroupDal>();
            services.AddScoped<ISubeDal, EfSubeDal>();
            services.AddScoped<ITuikDal, EfTuikDal>();
            services.AddScoped<IUserClaimDal, EfUserClaimDal>();
            services.AddScoped<IUserDal, EfUserDal>();
            services.AddScoped<ISubeCityDal, EfSubeCityDal>();
            services.AddScoped<IFormDataInputDal, EfFormDataInputDal>();
            services.AddScoped<IAuthService, AuthManager>();
            services.AddScoped<IUserService, UserManager>();
            services.AddScoped<ICityService, CityManager>();
            services.AddScoped<ISubeCityService, SubeCityManager>();
            services.AddScoped<IBolgeService, BolgeManager>();
            services.AddScoped<IEmteaService, EmteaManager>();
            services.AddScoped<IEmteaTypeService, EmteaTypeManager>();
            services.AddScoped<IEmteaGroupService, EmteaGrupManager>();
            services.AddScoped<IEmteaTypeGroupService, EmteaTypeGroupManager>();
            services.AddScoped<ITuikService, TuikManager>();
            services.AddScoped<ISubeService, SubeManager>();
            services.AddScoped<IDataInputService, DataInputManager>();
            services.AddScoped<IFormDataInputService, FormDataInputManager>();
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddHttpContextAccessor();

            services
                .AddControllersWithViews().AddRazorRuntimeCompilation()
                .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);
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
            }

            app.UseSession();

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
