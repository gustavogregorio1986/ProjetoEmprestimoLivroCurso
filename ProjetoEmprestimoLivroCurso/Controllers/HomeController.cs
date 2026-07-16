using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ProjetoEmprestimoLivroCurso.Controllers
{
    public class HomeController : Controller
    {
        

        public IActionResult Index()
        {
            return View();
        }
    }
}
