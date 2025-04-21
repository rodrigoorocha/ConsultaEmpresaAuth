using System.Security.Cryptography;
using System.Text;

namespace ConsultaEmpresa.Infra;

public class SenhaEncriptador
{
    public virtual string Encriptar(string senha)
    {
        var chaveAdicional = "qualquerstringaqui";

        var novaSenha = $"{senha}{chaveAdicional}";

        var bytes = Encoding.UTF8.GetBytes(novaSenha);

        var hashBytes = SHA512.HashData(bytes);

        return StringBytes(hashBytes);
    }

    private static string StringBytes(byte[] bytes)
    {
        var sb = new StringBuilder();
        foreach (byte b in bytes)
        {
            var hex = b.ToString("x2");
            sb.Append(b);
        }

        return sb.ToString();
    }
}