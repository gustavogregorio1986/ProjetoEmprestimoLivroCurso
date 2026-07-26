using ProjetoEmprestimoLivroCurso.Enum;
using ProjetoEmprestimoLivroCurso.Models;
using System.ComponentModel.DataAnnotations;

namespace ProjetoEmprestimoLivroCurso.Dto.Usuario
{
    public class UsuarioCriacaoDto
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Digite o nome completo")]
        public string NomeCompleto { get; set; } = string.Empty;
        [Required(ErrorMessage = "Digite o usuario completo")]
        public string Usuario { get; set; } = string.Empty;
        [Required(ErrorMessage = "Digite o email completo")]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "Digite a situação")]
        public bool Situacao { get; set; } = true;
        [Required(ErrorMessage = "Selecione um perfil")]
        public PerfilEnum Perfil { get; set; }
        [Required(ErrorMessage = "selecione um turno")]
        public TurnoEnum Turno { get; set; }
        [Required(ErrorMessage = "Digite a senha"), MinLength(6, ErrorMessage = "A senha deve conter no minimo 6 caracteres")]
        public string Senha { get; set; }
        [Required(ErrorMessage = "Digite a confirmação de senha"), Compare("Senha", ErrorMessage = "As senhas não coincidem!")]
        public string ConfirmacaoSenha { get; set; }

        [Required(ErrorMessage = "Digite o logradouro")]
        public string Logradouro { get; set; } = string.Empty;
        [Required(ErrorMessage = "Digite o numero")]
        public int Numero { get; set; }
        [Required(ErrorMessage = "Digite o bairro")]
        public string Bairro { get; set; }
        [Required(ErrorMessage = "Digite o CEP")]
        public string CEP { get; set; } = string.Empty;
        [Required(ErrorMessage = "Digite o estado")]
        public string Estado { get; set; } = string.Empty;
        public string? Complemento { get; set; } = string.Empty;
    }
}
