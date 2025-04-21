using ConsultaEmpresa.Domain.Features;
using ConsultaEmpresa.Domain.Features.Empresas;
using ConsultaEmpresa.Infra.Context;
using ConsultaEmpresa.Infra.Repository.Base;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsultaEmpresa.Infra.Repository.Empresas
{
    public class EmpresaRepository : BaseRepository<Empresa>, IEmpresaRepository
    {
        private readonly AppDbContext _context;

        public EmpresaRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task CriaAsync(Empresa empresa)
        {
            await _context.Empresas.AddAsync(empresa);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Empresa>> GetByUserIdAsync(int userId)
        {
            return await _context.Empresas
                .Where(e => e.UsuarioId == userId)
                .ToListAsync();
        }

        public Empresa GetByCnpj(string cnpj)
        {
            return _context.Empresas.FirstOrDefault(e => e.Cnpj == cnpj);
        }
    }
}