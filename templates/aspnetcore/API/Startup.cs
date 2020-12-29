using API.Validators;
using API.ViewModels;
using Business;
using DB;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNet.OData.Formatter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using System.Linq;

namespace API
{
    public class Startup
    {
        private const string API_NAME = "My Api"; // TODO Change

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                .AddNewtonsoftJson()
                .AddFluentValidation();

            // TODO move these registration to autofac modules or servicecollection extensions
            services.AddDbContext<ExampleContext>(options =>
            {
                options.UseSqlite("Data Source=example.db"); // TODO Change to sql database
            });

            services.AddTransient<IValidator<ExampleViewModel>, ExampleValidator>();
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddMediatR(typeof(ExampleBusinessFunction).Assembly);

            services.AddSwaggerGen();

            services.AddOData();

            AddOdataFormatters(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ExampleContext context)
        {
            context.Database.EnsureCreated(); // TODO move to migrations

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.EnableDependencyInjection();
                endpoints.MapControllers();
                endpoints.Select().Filter().MaxTop(10);
                endpoints.MapODataRoute("odata", "odata", BuildODataModel());
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", API_NAME);
            });
        }

        private IEdmModel BuildODataModel()
        {
            var odataBuilder = new ODataConventionModelBuilder();
            odataBuilder.EntitySet<ExampleBusinessModel>("Example"); // String must match controller name

            return odataBuilder.GetEdmModel();
        }

        private static void AddOdataFormatters(IServiceCollection services)
        {
            // https://github.com/OData/WebApi/issues/2024 Adding cross support for odata and swagger
            services.AddMvcCore(options =>
            {
                foreach (var outputFormatter in options.OutputFormatters.OfType<ODataOutputFormatter>().Where(x => x.SupportedMediaTypes.Count == 0))
                {
                    outputFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/prs.odatatestxx-odata"));
                }

                foreach (var inputFormatter in options.InputFormatters.OfType<ODataInputFormatter>().Where(x => x.SupportedMediaTypes.Count == 0))
                {
                    inputFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/prs.odatatestxx-odata"));
                }
            });
        }
    }
}
