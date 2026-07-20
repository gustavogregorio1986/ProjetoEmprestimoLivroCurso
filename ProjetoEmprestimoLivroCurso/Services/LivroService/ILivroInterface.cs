using ProjetoEmprestimoLivroCurso.Dto;
using ProjetoEmprestimoLivroCurso.Models;

namespace ProjetoEmprestimoLivroCurso.Services.LivroService
{
    public interface ILivroInterface
    {
        Task<List<LivroModel>> BuscarLivros();

        bool VerificaSeJaExisteCadastro(LivroCriacaoDto livroCriacaoDto);

        Task<LivroModel> Cadastrar(LivroCriacaoDto livroCriacaoDto, IFormFile foto);
    }
}
