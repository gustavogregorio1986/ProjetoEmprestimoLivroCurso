using Microsoft.AspNetCore.Mvc;

namespace ProjetoEmprestimoLivroCurso.Controllers
{
    public class LivroController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
