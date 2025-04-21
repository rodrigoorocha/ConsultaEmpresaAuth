namespace ConsultaEmpresa.Domain.Features.Usuarios
{
    public interface IUsuarioRepository
    {
        public Task CriaAsync(Usuario usuario);

        public Task<bool> ExistePorEmailAsync(string email);

        public Task<bool> ExistePorIdAsync(int id);

        public Task<int> ExisteLoginAsync(string email, string senha);
    }
}