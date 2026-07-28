namespace ProjetoEmprestimoLivroCurso.Services.Auteticacao
{
    public interface IAtenticacaoInterface
    {
        public void CriarSenhaHash(string senha, out byte[] senhaHash, out byte[] senhaSalt);
    }
}
