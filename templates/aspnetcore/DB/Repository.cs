using Ardalis.Specification.EntityFrameworkCore;
using Business;
using System.Linq;
using System.Threading.Tasks;

namespace DB
{
    public class Repository<T> : RepositoryBase<T>, IRepository<T>
        where T : class
    {
        private readonly ExampleContext context;

        public Repository(ExampleContext context)
            : base (context)
        {
            this.context = context;
        }

        public IQueryable<T> All => context.Set<T>();

        public Task DeleteByIdAsync(int id)
        {
            return this.DeleteByIdAsync<int>(id);
        }

        public async Task DeleteByIdAsync<TId>(TId id)
        {
            context.RemoveRange(await context.Set<T>().FindAsync(id)); //This might double query check it out
        }
    }
}
