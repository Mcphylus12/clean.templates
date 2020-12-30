using Business;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DB
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDataServices(this IServiceCollection services)
        {
            services.AddDbContext<ExampleContext>(options =>
            {
                options.UseSqlite("Data Source=example.db"); // TODO Change to sql database
            });
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
        }
    }
}
