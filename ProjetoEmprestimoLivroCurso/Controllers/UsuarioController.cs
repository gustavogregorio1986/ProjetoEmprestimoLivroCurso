using Microsoft.AspNetCore.Mvc;
using ProjetoEmprestimoLivroCurso.Dto.Usuario;
using ProjetoEmprestimoLivroCurso.Enum;
using ProjetoEmprestimoLivroCurso.Services.Usuario;

namespace ProjetoEmprestimoLivroCurso.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioInterface _usuarioInterface;

        public UsuarioController(IUsuarioInterface usuarioInterface)
        {
            _usuarioInterface = usuarioInterface;
        }

        public async Task<IActionResult> Index(int? id)
        {
            var usuarios = await _usuarioInterface.BuscarUsuarios(id);
            return View(usuarios);
        }

        [HttpGet]
        public ActionResult Cadastrar(int? id)
        {
            ViewBag.Perfil = PerfilEnum.Administrador;

            if (id != null)
            {
                ViewBag.Perfil = PerfilEnum.Cliente;
            }

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Cadastrar(UsuarioCriacaoDto usuarioCriacaoDto)
        {
            if (ModelState.IsValid)
            {
                // Verifica se já existe usuário ou email
                bool existe = await _usuarioInterface.VerificaSeExisteUsuarioEEmail(usuarioCriacaoDto);
                if (existe)
                {
                    TempData["MensagemErro"] = "Já existe email/usuário cadastrado!";
                    return View(usuarioCriacaoDto);
                }

                // Cadastra usuário
                var usuario = await _usuarioInterface.Cadastrar(usuarioCriacaoDto);
                TempData["MensagemSucesso"] = "Cadastro realizado com sucesso!";

                // Redireciona conforme perfil
                if (usuario.Perfil != PerfilEnum.Cliente)
                {
                    return RedirectToAction("Index", "Funcionario");
                }

                return RedirectToAction("Index", "Cliente", new { Id = "0" });
            }

            // Se o ModelState não for válido, retorna para a view com os dados
            return View(usuarioCriacaoDto);
        }
    }
}
