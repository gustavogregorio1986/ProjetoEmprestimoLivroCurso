using System.Security.Cryptography;

namespace ProjetoEmprestimoLivroCurso.Services.Auteticacao
{
    public class AutreticacaoService : IAtenticacaoInterface
    {
        public void CriarSenhaHash(string senha, out byte[] senhaHash, out byte[] senhaSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                senhaHash = hmac.Key;
                senhaSalt = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(senha));
            }
        }
    }
}
