using ConsultaEmpresa.Domain.Features;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsultaEmpresa.Infra.Repository.Base
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected DbSet<T> Query { get; set; }
        protected DbContext Context { get; set; }

        public BaseRepository(DbContext context)
        {
            this.Context = context;
            this.Query = Context.Set<T>();
        }

        public async Task CreateAsync(T entity)
        {
            await Context.Set<T>().AddAsync(entity);
            await Context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>?> GetAllAsync()
        {
            return await this.Query.ToListAsync();
        }
    }
}