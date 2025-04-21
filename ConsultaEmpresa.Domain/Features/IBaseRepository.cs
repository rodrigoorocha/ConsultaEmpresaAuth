using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsultaEmpresa.Domain.Features
{
    public interface IBaseRepository<T> where T : class
    {
        Task CreateAsync(T entity);

        Task<IEnumerable<T>?> GetAllAsync();
    }
}