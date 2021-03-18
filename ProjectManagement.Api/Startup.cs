using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Shared;
using ProjectManagement.Data;
using ProjectManagement.Entities;

namespace ProjectManagement.Api
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
            services.AddControllers().AddNewtonsoftJson(opt => opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddDbContext<ProjectManagementContext>(
                options =>
                {
                    options.UseInMemoryDatabase("ProjectManagement");
                    options.UseLazyLoadingProxies();
                }, ServiceLifetime.Transient);
            DependencyResolver.Init(this.RegisterDependencies(services).BuildServiceProvider());

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }


        private IServiceCollection RegisterDependencies(IServiceCollection services)
        {
            services.AddTransient<IBaseRepository<User>, BaseRepository<User>>();
            services.AddTransient<IBaseRepository<Project>, BaseRepository<Project>>();
            services.AddTransient<IBaseRepository<Task>, BaseRepository<Task>>();

            return services;
        }
    }
}
