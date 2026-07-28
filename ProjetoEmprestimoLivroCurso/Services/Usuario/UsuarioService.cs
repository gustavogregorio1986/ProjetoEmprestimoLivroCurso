using Microsoft.EntityFrameworkCore;
using ProjetoEmprestimoLivroCurso.Data;
using ProjetoEmprestimoLivroCurso.Dto.Usuario;
using ProjetoEmprestimoLivroCurso.Models;
using ProjetoEmprestimoLivroCurso.Services.Auteticacao;

namespace ProjetoEmprestimoLivroCurso.Services.Usuario
{
    public class UsuarioService : IUsuarioInterface
    {
        private readonly AppDbContext _context;
        private readonly IAtenticacaoInterface _atenticacaoInterface;

        public UsuarioService(AppDbContext context, IAtenticacaoInterface atenticacaoInterface)
        {
            _context = context;
            _atenticacaoInterface = atenticacaoInterface;
        }

        public async Task<List<UsuarioModel>> BuscarUsuarios(int? id)
        {
            try
            {
                var registros = new List<UsuarioModel>();

                if (id != null)
                {
                    registros = await _context.Usuarios
                        .Where(cliente => cliente.Perfil == 0)
                        .Include(e => e.Endereco)
                        .ToListAsync();
                }
                else
                {
                    registros = await _context.Usuarios
                        .Where(cliente => cliente.Perfil != 0)
                        .Include(e => e.Endereco)
                        .ToListAsync();
                }

                return registros;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<UsuarioCriacaoDto> Cadastrar(UsuarioCriacaoDto usuarioCriacaoDto)
        {
            try
            {
                _atenticacaoInterface.CriarSenhaHash(usuarioCriacaoDto.Senha, out byte[] senhaHash, out byte[] senhaSalt);

                var usuario = new UsuarioModel 
                { 
                    NomeCompleto = usuarioCriacaoDto.NomeCompleto,
                    Email = usuarioCriacaoDto.Usuario,
                    Perfil = usuarioCriacaoDto.Perfil,
                    Turno = usuarioCriacaoDto.Turno,
                    SenhaHash = senhaHash,
                    SenhaSalt = senhaSalt
                };

                var endereco = new EnderecoModel
                {
                    Logradouro = usuarioCriacaoDto.Logradouro,
                    Numero = usuarioCriacaoDto.Numero,
                    Bairro = usuarioCriacaoDto.Bairro,
                    Estado = usuarioCriacaoDto.Bairro,
                    Complemento = usuarioCriacaoDto.Complemento,
                    CEP = usuarioCriacaoDto.CEP,
                    Usuario = usuario
                };

                usuario.Endereco = endereco;

                _context.Add(usuario);
                await _context.SaveChangesAsync();

                return usuarioCriacaoDto;


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> VerificaSeExisteUsuarioEEmail(UsuarioCriacaoDto usuarioCriacaoDto)
        {
            try
            {
                var mesmoUsuario = await _context.Usuarios
                    .FirstOrDefaultAsync(usuarioBanco =>
                        usuarioBanco.Email == usuarioCriacaoDto.Email ||
                        usuarioBanco.Usuario == usuarioCriacaoDto.Usuario);

                // Retorna true se já existe usuário/email
                return mesmoUsuario != null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
