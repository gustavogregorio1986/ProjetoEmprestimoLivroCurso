using Microsoft.EntityFrameworkCore;
using ProjetoEmprestimoLivroCurso.Data;
using ProjetoEmprestimoLivroCurso.Dto;
using ProjetoEmprestimoLivroCurso.Models;

namespace ProjetoEmprestimoLivroCurso.Services.LivroService
{
    public class LivroService : ILivroInterface
    {
        private readonly AppDbContext _context;
        private string _caminhoServidor;

        public LivroService(AppDbContext context, IWebHostEnvironment sistema)
        {
            _context = context;
            _caminhoServidor = Path.Combine(sistema.WebRootPath, "imagem");

            // garante que a pasta exista
            if (!Directory.Exists(_caminhoServidor))
            {
                Directory.CreateDirectory(_caminhoServidor);
            }
        }

        public async Task<List<LivroModel>> BuscarLivros()
        {
            try
            {
                var livros = await _context.Livros.ToListAsync();
                return livros;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<LivroModel> Cadastrar(LivroCriacaoDto livroCriacaoDto, IFormFile foto)
        {
            try
            {
                var nomeArquivo = Guid.NewGuid().ToString() + Path.GetExtension(foto.FileName);
                var caminhoCompleto = Path.Combine(_caminhoServidor, nomeArquivo);

                using (var stream = new FileStream(caminhoCompleto, FileMode.Create))
                {
                    await foto.CopyToAsync(stream);
                }

                var livro = new LivroModel
                {
                    Titulo = livroCriacaoDto.Titulo,
                    Autor = livroCriacaoDto.Autor,
                    QuatidadeEmEstoque = livroCriacaoDto.QuatidadeEmEstoque,
                    Descricao = livroCriacaoDto.Descricao,
                    AnoPublicacao = livroCriacaoDto.AnoPublicacao,
                    ISBN = livroCriacaoDto.ISBN,
                    Genero = livroCriacaoDto.Genero,
                    // caminho público para exibir no navegador
                    Capa = "/imagem/" + nomeArquivo
                };

                _context.Add(livro);
                await _context.SaveChangesAsync();

                return livro;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool VerificaSeJaExisteCadastro(LivroCriacaoDto livroCriacaoDto)
        {
            try
            {
                var livroBanco = _context.Livros.FirstOrDefault(livro => livro.ISBN == livroCriacaoDto.ISBN);
                return livroBanco != null;

            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
