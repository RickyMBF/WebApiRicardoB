using AutoMapper;
using WebApiRicardoB.DTOs;
using WebApiRicardoB.Entities;

namespace WebApiRicardoB.Utilidades
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<CarroDTO, Carro>();
            CreateMap<Carro, GetCarroDTO>();
        }
    }
}
