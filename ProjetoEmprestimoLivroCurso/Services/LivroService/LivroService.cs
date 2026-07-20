using AutoMapper;
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
        private IMapper _mapper;

        public LivroService(AppDbContext context, IWebHostEnvironment sistema, IMapper mapper)
        {
            _context = context;
            _caminhoServidor = Path.Combine(sistema.WebRootPath, "imagem");
            _mapper = mapper;


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

        public async Task<LivroModel> Cadastrar(LivroCriacaoDto dto, IFormFile foto)
        {
            string nomeArquivo = Guid.NewGuid().ToString() + Path.GetExtension(foto.FileName);
            string caminho = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imagem", nomeArquivo);

            using (var stream = new FileStream(caminho, FileMode.Create))
            {
                await foto.CopyToAsync(stream);
            }

            var livro = _mapper.Map<LivroModel>(dto);
            livro.Capa = "/imagem/" + nomeArquivo; // caminho público

            _context.Add(livro);
            await _context.SaveChangesAsync();

            return livro;
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
