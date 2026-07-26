using System.ComponentModel.DataAnnotations;

namespace ProjetoEmprestimoLivroCurso.Dto.Livro
{
    public class LivroCriacaoDto
    {
        [Required(ErrorMessage = "Insira o titulo!")]
        public string Titulo { get; set; } = string.Empty;
        [Required(ErrorMessage = "Insira a Decrição!")]
        public string Descricao { get; set; } = string.Empty;
        [Required(ErrorMessage = "Insira o ISBN!")]
        public string ISBN { get; set; } = string.Empty;
        [Required(ErrorMessage ="Insira o Augtor")]
        public string Autor { get; set; } = string.Empty;
        [Required(ErrorMessage = "Insira o Genero!")]
        public string Genero { get; set; } = string.Empty;
        [Required(ErrorMessage = "Insira o ano de publicação!")]
        public int AnoPublicacao { get; set; }
        [Required(ErrorMessage = "Insira a Quantidade em Estoque!")]
        public int QuatidadeEmEstoque { get; set; }
        public IFormFile Foto { get; set; }
    }
}
