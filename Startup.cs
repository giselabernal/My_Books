using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using My_Books.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using My_Books.Data;
using My_Books.Data.Services;
using My_Books.Exceptions;

namespace My_Books
{
    public class Startup
    {
        public string ConnString { get; set; }  
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            ConnString = Configuration.GetConnectionString("DefaultConnStr");
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddControllers()
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.SuppressModelStateInvalidFilter = true;
                    options.SuppressMapClientErrors = true;
                });
            //configure DBContext with SQL
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(ConnString));

            //Configure Services
            services.AddTransient<BooksService>();
            services.AddTransient<AuthorsService>();    
            services.AddTransient<PublishersService>();
            //services.AddLogging()

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v2", new OpenApiInfo { Title = "My_Books_updated", Version = "v2" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v2/swagger.json", "My_Books_updated_v1"));
            }

            app.UseHttpsRedirection();
            //duda? es solo para cuando es un solo endpoint no?
           // app.MapControllerRoute(name: "Books", pattern: "{controller=Home}/{action=Index}/{id?}");

            app.UseRouting();

            app.UseAuthorization();

            //ExceptionHandling
            //app.ConfigureBuilInExceptionHandler();
            app.ConfigureCustomExceptionHandler();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
           // AppDbInitializer.Seed(app);
        }
    }
}
