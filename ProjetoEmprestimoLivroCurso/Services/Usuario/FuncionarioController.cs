using Microsoft.AspNetCore.Mvc;

namespace ProjetoEmprestimoLivroCurso.Services.Usuario
{
    public class FuncionarioController : Controller
    {
        private readonly IUsuarioInterface _usuarioInterface;

        public FuncionarioController(IUsuarioInterface usuarioInterface)
        {
            _usuarioInterface = usuarioInterface;
        }

        public async Task<ActionResult> Index()
        {
            var funcionarios = await _usuarioInterface.BuscarUsuarios(null);
            return View(funcionarios);
        }
    }
}
