using ProjetoEmprestimoLivroCurso.Models;

namespace ProjetoEmprestimoLivroCurso.Services.Usuario
{
    public interface IUsuarioInterface
    {
        Task<List<UsuarioModel>> BuscarUsuarios(int? id);
    }
}
