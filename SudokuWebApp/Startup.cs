using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using SudokuWebApp.Services.Generation;
using SudokuWebApp.Services.Validation;

namespace SudokuWebApp
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
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Serialize;
                    options.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.Objects;
                });

            //register Services
            services.AddTransient<IGenerationFieldService, GenerationFieldService>();
            services.AddSingleton<IEmptyFieldFactory, EmptyFieldFactory>();
            services.AddTransient<IValidateService, ValidateService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseDefaultFiles(); // serves up our wwwroot files
            app.UseStaticFiles(); // allows serving of static files
            app.UseMvc(); // sets MVC routes for webapi
        }
    }
}
