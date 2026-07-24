using AutoMapper;
using AutoMapper.Configuration.Annotations;
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

        public async Task<LivroModel> BuscarLivroPorId(int id)
        {
            try
            {
                var livro = await _context.Livros.FirstOrDefaultAsync(l => l.Id == id);

                return livro;

            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
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
            var livro = _mapper.Map<LivroModel>(dto);

            livro.Capa = await GerarCaminhoArquivoAsync(foto);

            _context.Add(livro);
            await _context.SaveChangesAsync();

            return livro;
        }

        public async Task<LivroModel> Editar(LivroEdicaoDto dto, IFormFile foto)
        {
            var livro = await _context.Livros.FirstOrDefaultAsync(l => l.Id == dto.Id);
            if (livro == null)
                return null;

            _mapper.Map(dto, livro);

            if (foto != null)
            {
                // só tenta deletar se houver capa antiga
                if (!string.IsNullOrEmpty(livro.Capa))
                {
                    var caminhoAntigo = Path.Combine(_caminhoServidor, Path.GetFileName(livro.Capa));
                    if (File.Exists(caminhoAntigo))
                        File.Delete(caminhoAntigo);
                }

                // gera nova capa
                livro.Capa = await GerarCaminhoArquivoAsync(foto);
            }

            _context.Update(livro);
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

        private async Task<string> GerarCaminhoArquivoAsync(IFormFile foto)
        {
            if (foto == null || foto.Length == 0)
                return null;

            // Gera nome único
            string nomeArquivo = Guid.NewGuid().ToString() + Path.GetExtension(foto.FileName);

            // Caminho físico no servidor
            string caminhoFisico = Path.Combine(_caminhoServidor, nomeArquivo);

            // Salva o arquivo
            using (var stream = new FileStream(caminhoFisico, FileMode.Create))
            {
                await foto.CopyToAsync(stream);
            }

            // Retorna o caminho público (para ser usado no navegador)
            return "/imagem/" + nomeArquivo;
        }

    }
}
