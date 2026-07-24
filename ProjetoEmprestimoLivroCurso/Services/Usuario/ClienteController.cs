using Microsoft.AspNetCore.Mvc;

namespace ProjetoEmprestimoLivroCurso.Services.Usuario
{
    public class ClienteController : Controller
    {
        private readonly IUsuarioInterface _usuarioInterface;

        public ClienteController(IUsuarioInterface usuarioInterface)
        {
            _usuarioInterface = usuarioInterface;
        }

        public async Task<ActionResult> Index(int? id)
        {
            var clientes = await _usuarioInterface.BuscarUsuarios(id);
            return View(clientes);
        }
    }
}
