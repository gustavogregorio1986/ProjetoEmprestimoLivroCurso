using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjetoEmprestimoLivroCurso.Dto.Livro;
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

        // LISTAGEM DE LIVROS
        public async Task<ActionResult<List<LivroModel>>> Index()
        {
            var livros = await _livroInterface.BuscarLivros();
            return View(livros);
        }

        // FORMULÁRIO DE CADASTRO
        [HttpGet]
        public ActionResult Cadastrar()
        {
            return View();
        }

        // DETALHES DO LIVRO
        [HttpGet]
        public async Task<ActionResult> Detalhes(int id)
        {
            var livro = await _livroInterface.BuscarLivroPorId(id);
            return View(livro);
        }

        // FORMULÁRIO DE EDIÇÃO
        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            var livro = await _livroInterface.BuscarLivroPorId(id);

            if (livro == null)
                return RedirectToAction("Index");

            var livroEdicaoDto = _mapper.Map<LivroEdicaoDto>(livro);
            livroEdicaoDto.Capa = livro.Capa;
            return View(livroEdicaoDto);
        }

        // CADASTRAR LIVRO
        [HttpPost]
        public async Task<ActionResult> Cadastrar(LivroCriacaoDto livroCriacaoDto, IFormFile foto)
        {
            if (foto == null)
            {
                TempData["MensagemErro"] = "Inclua uma imagem de capa!";
                return View(livroCriacaoDto);
            }

            if (!ModelState.IsValid)
            {
                TempData["MensagemErro"] = "Verifique os dados novamente!";
                return View(livroCriacaoDto);
            }

            if (_livroInterface.VerificaSeJaExisteCadastro(livroCriacaoDto))
            {
                TempData["MensagemErro"] = "Código ISBN já cadastrado!";
                return View(livroCriacaoDto);
            }

            var livro = await _livroInterface.Cadastrar(livroCriacaoDto, foto);

            TempData["MensagemSucesso"] = "Livro cadastrado com sucesso!";
            return RedirectToAction("Index");
        }

        // EDITAR LIVRO
        [HttpPost]
        public async Task<ActionResult> Editar(LivroEdicaoDto livroEdicaoDto, IFormFile? foto)
        {
            if (!ModelState.IsValid)
            {
                TempData["MensagemErro"] = "Verifique os dados preenchidos!";
                return View(livroEdicaoDto);
            }

            var livro = await _livroInterface.Editar(livroEdicaoDto, foto);
            if (livro == null)
            {
                TempData["MensagemErro"] = "Livro não encontrado!";
                return RedirectToAction("Index");
            }

            TempData["MensagemSucesso"] = "Livro atualizado com sucesso!";
            return RedirectToAction("Index");
        }
    }
}
