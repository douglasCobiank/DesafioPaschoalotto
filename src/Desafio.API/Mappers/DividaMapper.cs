using AutoMapper;
using Desafio.API.Models;
using Desafio.Core.Handler.Models;

namespace Desafio.API.Mappers
{
    public class DividaMapper: Profile
    {
        public DividaMapper()
        {
            CreateMap<Divida, DividaDto>().ReverseMap();
            CreateMap<Parcela, ParcelaDto>().ReverseMap();
        }
    }
}