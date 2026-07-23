using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjetoEmprestimoLivroCurso.Dto;
using ProjetoEmprestimoLivroCurso.Models;
using ProjetoEmprestimoLivroCurso.Services.LivroService;

namespace ProjetoEmprestimoLivroCurso.Controllers
{
    public class LivroController : Controller
    {
        private readonly ILivroInterface _livroInterface;
        private readonly IMapper _mapper;

        public LivroController(ILivroInterface livroInterface, IMapper mapper)
        {
            _livroInterface = livroInterface;
            _mapper = mapper;
        }

        public async Task<ActionResult<List<LivroModel>>> Index()
        {
            var livros = await _livroInterface.BuscarLivros();
            return View(livros);
        }

        [HttpGet]
        public ActionResult Cadastrar()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> Detalhes(int id)
        {
            var livro = await _livroInterface.BuscarLivroPorId(id);
            return View(livro);
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            var livro = await _livroInterface.BuscarLivroPorId(id);

            if (livro == null)
                return RedirectToAction("Index");

            var livroEdicaoDto = _mapper.Map<LivroEdicaoDto>(livro);
            livroEdicaoDto.Capa = livro.Capa;
            return View(livroEdicaoDto); // precisa passar o DTO aqui
        }


        [HttpPost]
        public async Task<ActionResult> Cadastrar(LivroCriacaoDto livroCriacaoDto, IFormFile foto)
        {
            if (foto == null)
            {
                TempData["MensagemErro"] = "Incluir uma imagem de capa!";
                return View(livroCriacaoDto);
            }

            if (!ModelState.IsValid)
            {
                TempData["MensagemErro"] = "Verifique os dados novamente!";
                return View(livroCriacaoDto);
            }

            if (_livroInterface.VerificaSeJaExisteCadastro(livroCriacaoDto))
            {
                TempData["MensagemErro"] = "Código ISBN já cadastrado";
                return View(livroCriacaoDto);
            }

            // chama o service para salvar o livro e a imagem
            var livro = await _livroInterface.Cadastrar(livroCriacaoDto, foto);

            TempData["MensagemSucesso"] = "Livro cadastrado com sucesso!";
            return RedirectToAction("Index");
        }
    }
}
