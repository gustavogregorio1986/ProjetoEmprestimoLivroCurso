using Microsoft.AspNetCore.Mvc;
using ProjetoEmprestimoLivroCurso.Dto;
using ProjetoEmprestimoLivroCurso.Models;
using ProjetoEmprestimoLivroCurso.Services.LivroService;

namespace ProjetoEmprestimoLivroCurso.Controllers
{
    public class LivroController : Controller
    {
        private readonly ILivroInterface _livroInterface;

        public LivroController(ILivroInterface livroInterface)
        {
            this._livroInterface = livroInterface;
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

        [HttpPost]
        public async Task<ActionResult> Cadastrar(LivroCriacaoDto livroCriacaoDto, IFormFile foto)
        {
            if(foto != null)
            {
                if (ModelState.IsValid)
                {
                    if(_livroInterface.VerificaSeJaExisteCadastro(livroCriacaoDto))
                    {
                        TempData["MensagemErro"] = "Código ISBN já cadastrado";
                        return View(livroCriacaoDto);
                    }

                    var livro = await _livroInterface.Cadastrar(livroCriacaoDto, foto);

                    TempData["MensagemSucesso"] = "Livro Cadastrado com sucesso!";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["MensagemErro"] = "Verifique os dados novamente!";
                    return View(livroCriacaoDto);
                }
            }
            else
            {
                TempData["MensagemErro"] = "Incluir uma imagem de capa!";
                return View(livroCriacaoDto);
            }
        }
    }
}
