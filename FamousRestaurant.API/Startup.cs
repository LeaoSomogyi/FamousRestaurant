using FamousRestaurant.API.Contracts;
using FamousRestaurant.API.DataContext;
using FamousRestaurant.API.Repositories;
using FamousRestaurant.API.Units;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Reflection;

namespace FamousRestaurant.API
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
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(opt =>
                {                    
                    opt.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                    opt.SerializerSettings.MissingMemberHandling = MissingMemberHandling.Ignore;
                    opt.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                    opt.SerializerSettings.DateFormatString = "yyyy-MM-ddTHH:mm:ss";
                    opt.SerializerSettings.Culture = new System.Globalization.CultureInfo("en-US");
                    opt.SerializerSettings.Formatting = Formatting.None;
                    opt.SerializerSettings.FloatFormatHandling = FloatFormatHandling.DefaultValue;
                    opt.SerializerSettings.FloatParseHandling = FloatParseHandling.Double;
                    opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    opt.SerializerSettings.TypeNameHandling = TypeNameHandling.None;
                });

            services.AddEntityFrameworkSqlServer().AddDbContext<RestaurantContext>(options =>
            {
                options.UseSqlServer(Configuration["ConnectionString"],
                    sqlOptions => sqlOptions.MigrationsAssembly(typeof(Startup)
                    .GetTypeInfo().Assembly.GetName().Name));
            });

            services.AddScoped(typeof(DbContext), typeof(RestaurantContext));
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();                
            }

            app.UseHttpsRedirection();
            app.UseMvc();

            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<RestaurantContext>();
                context.Database.Migrate();
            }
        }
    }
}
