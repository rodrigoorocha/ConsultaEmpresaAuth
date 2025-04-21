namespace ConsultaEmpresa.Domain.Features.Usuarios
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string SenhaHash { get; set; }

        public void DefinirSenha(string senha)
        {
            SenhaHash = senha;
        }

        public Usuario(string email, string senhaHash)
        {
            Email = email;
            SenhaHash = senhaHash;
        }
    }
}