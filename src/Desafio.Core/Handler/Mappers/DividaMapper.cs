using AutoMapper;
using Desafio.Core.Handler.Models;
using Desafio.Infrastructure.Data.Models;

namespace Desafio.Core.Handler.Mappers
{
    public class DividaMapper: Profile
    {
        public DividaMapper()
        {
            CreateMap<DividaDto, DividaData>().ReverseMap();
            CreateMap<ParcelaDto, ParcelaData>().ReverseMap();
        }
    }
}