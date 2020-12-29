using Ardalis.Specification;
using System.Linq;
using System.Threading.Tasks;

namespace Business
{
    public interface IRepository<T> : IRepositoryBase<T>
        where T : class
    {
        Task DeleteByIdAsync(int id);
        Task DeleteByIdAsync<TId>(TId id);
        IQueryable<T> All { get; }
    }
}