using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PetLibrary;
using PetLibrary.Manager;
using PetLibrary.Repository;

namespace PetSort
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
            services.AddMvc();

            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            services.AddOptions();

            AppSettings settings = Configuration.GetSection("AppSettings").Get<AppSettings>();
            PetRepository repository = new PetRepository(settings);
            PetManager manager = new PetManager(repository);
            services.AddTransient<IPetRepository, PetRepository>(r => repository);
            services.AddTransient<IPetManager, PetManager>(m => manager);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Cats}/{id?}");
            });
        }
    }
}
