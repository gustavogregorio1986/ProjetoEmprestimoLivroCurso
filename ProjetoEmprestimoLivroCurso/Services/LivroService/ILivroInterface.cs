using ProjetoEmprestimoLivroCurso.Dto.Livro;
using ProjetoEmprestimoLivroCurso.Dto.Usuario;
using ProjetoEmprestimoLivroCurso.Models;

namespace ProjetoEmprestimoLivroCurso.Services.LivroService
{
    public interface ILivroInterface
    {
        Task<List<LivroModel>> BuscarLivros();

        bool VerificaSeJaExisteCadastro(LivroCriacaoDto livroCriacaoDto);

        Task<LivroModel> Cadastrar(LivroCriacaoDto livroCriacaoDto, IFormFile foto);

        Task<LivroModel> BuscarLivroPorId(int id);
        Task<LivroModel> Editar(LivroEdicaoDto livroEdicaoDto, IFormFile foto);

    }
}
