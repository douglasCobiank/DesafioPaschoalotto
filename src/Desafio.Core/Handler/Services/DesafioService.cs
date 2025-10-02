using AutoMapper;
using Desafio.Core.Handler.Interface;
using Desafio.Core.Handler.Models;
using Desafio.Infrastructure.Data.Models;
using Desafio.Infrastructure.Repositories;

namespace Desafio.Core.Handler.Services
{
    public class DesafioService(IDesafioRepository desafioRepository, IMapper mapper) : IDesafioService
    {
        private readonly IDesafioRepository _desafioRepository = desafioRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<DividaDto> AddDividaAsync(DividaDto dividaDto)
        {
            var dividaData = _mapper.Map<DividaData>(dividaDto);

            dividaData.Parcelas = dividaData.Parcelas
                .Select(p => new ParcelaData
                {
                    Id = p.Id,
                    ValorParcela = p.ValorParcela,
                    DataVencimento = DateTime.SpecifyKind(p.DataVencimento, DateTimeKind.Utc)
                })
                .ToList();

            await _desafioRepository.AddAsync(dividaData);

            return _mapper.Map<DividaDto>(dividaData);
        }

        public async Task<List<DividaDto>> GetDividaAsync()
        {
            var dividaData = await _desafioRepository.GetAllAsync();
            var result = _mapper.Map<List<DividaDto>>(dividaData.ToList());
            ValorAtualizado(result);
            return result;
        }

        public async Task<DividaDto> GetByIdDividaAsync(int idDivida)
        {
            var dividaData = await _desafioRepository.GetByIdAsync(idDivida);

            return _mapper.Map<DividaDto>(dividaData);
        }

        private void ValorAtualizado(List<DividaDto>? Divida)
        {
            foreach (var divida in Divida)
            {
                int diasAtraso = 0;
                decimal valorAtualizado = 0.0M;

                foreach (var parcela in divida.Parcelas)
                {
                    decimal valorParcela = parcela.ValorParcela;
                    diasAtraso = (DateTime.Now - divida.Parcelas[0].DataVencimento).Days;

                    if (diasAtraso > 0)
                    {
                        decimal juros = (divida.PorcentagemJuros / 100 / 30) * diasAtraso * valorParcela;
                        decimal multa = (divida.PorcentagemMulta / 100) * valorParcela;
                        valorParcela += juros + multa;
                    }

                    valorAtualizado += valorParcela;
                }

                divida.ValorAtualizado = valorAtualizado;
                divida.DiasAtraso = diasAtraso;
                divida.ValorOriginal = divida.Parcelas.Select(p => p.ValorParcela).Sum();
            }
        }
    }
}