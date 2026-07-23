

using AutoMapper;
using ProjetoEmprestimoLivroCurso.Dto;
using ProjetoEmprestimoLivroCurso.Models;

namespace ProjetoEmprestimoLivroCurso.AutoMapper
{
    public class ProfileAutoMapper : Profile
    {
        public ProfileAutoMapper()
        {
            CreateMap<LivroCriacaoDto, LivroModel>();
            CreateMap<LivroModel, LivroEdicaoDto>();

        }
    }
}
